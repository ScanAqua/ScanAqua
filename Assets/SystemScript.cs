using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using Defective.JSON;
using System.IO;
using System.Linq;

public class SystemScript : MonoBehaviour
{
    public string IP = "127.0.0.1";
    public string port = "9999";
    WebSocket socket;
    public bool isReceived = false;
    string data;                                // ���������� ���� ���� �����͸� ������ ����
    public Texture2D testImage;                 // �׽�Ʈ �̹���
    public int theme = 0;                      // �׸� �⺻�� -1 -> �����ϸ� 0, 1, 2 �� �ϳ�

    public GameObject options;
    public int optionCount = 0;

    public MapManager maps;
    int mapIndex = 0;
    int[] themeCount = { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };
    public bool nextSceneTrigger = false;

    public GameObject[] fishes = new GameObject[10];
    public GameObject[] birds = new GameObject[5];
    public GameObject[] dinos = new GameObject[5];

    public Text socketState;

    void Awake()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "socket.txt")))
        {
            string[] datas = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "socket.txt"));
            IP = datas[0];
            port = datas[1];
        }

        socket = new WebSocket($"ws://{IP}:{port}");
        socket.Connect();
        socket.OnMessage += (sender, e) =>
        {
            data = e.Data;  
            isReceived = true;
        };
    }

    void Start()
    {
        maps.SetSea();
    }

    void Update()
    {
        if (options.activeSelf)
        {
            if (socket == null)
            {
                socketState.text = "X";
                socketState.color = Color.red;
            }
            else if (socket.ReadyState == WebSocketState.Open)
            {
                socketState.text = "O";
                socketState.color = Color.blue;
            }
            else
            {
                socketState.text = "X";
                socketState.color = Color.red;
            }
        }

        /* �̹��� ���� �׽�Ʈ�� ĸ�� �̹��� ������
        if (Input.GetKeyDown(KeyCode.Space) && theme == 0)
        {
            //byte[] decodedImage = ScreenCapture.CaptureScreenshotAsTexture().EncodeToPNG();
            byte[] decodedImage = testImage.EncodeToPNG();
            JSONObject sendData = new JSONObject();
            sendData.AddField("type", "image");
            sendData.AddField("creature", 0);
            sendData.AddField("image", System.Convert.ToBase64String(decodedImage));
            socket.Send(sendData.ToString());
        }
        */

        if (isReceived)
        {
            JSONObject receivedData = new JSONObject(data);                     // JSONObject�� ��ȯ
            string type = receivedData.GetField("type").stringValue;            // type ����

            /**************************
            start(request) -> theme(response): ������ �� �׸� �����ֱ�
            image: �̹��� ���� ��
            **************************/

            if (type == "start")            // ������ �� �׸� �����ֱ�
            {
                JSONObject sendData = new JSONObject();                         // JSONObject ����
                sendData.AddField("type", "theme");                             // Ÿ�� �߰�
                sendData.AddField("theme", theme);                              // �׸� ��ȣ �߰�
                socket.Send(sendData.ToString());
            }
            else if (type == "image")       // �̹��� ���� ��
            {
                int creature = receivedData.GetField("animal").intValue;      // ���� ��ȣ ����
                byte[] imageData = System.Convert.FromBase64String(receivedData.GetField("image").stringValue);         // �̹��� ������ ���� (base64string -> byte[])
                Debug.Log($"Theme: {theme}, creature: {creature}");

                Texture2D receivedImage = new Texture2D(2, 2);                  // �ؽ��� ����
                bool isLoaded = receivedImage.LoadImage(imageData);             // byte[] -> �ؽ��� ����

                if (isLoaded)
                {
                    GameObject[] group = theme == 0 ? fishes : theme == 1 ? birds : dinos;
                    GameObject animal = group[creature];
                    Vector3 position = Vector3.zero;
                    GameObject obj = Instantiate(animal);
                    obj.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = receivedImage;
                    // �ؽ��ĸ� ������ ����
                }
            }
            isReceived = false;
            data = null;                                                        // ���� ���� ������ �ʱ�ȭ
        }

        if (nextSceneTrigger)
        {
            mapIndex ++;

            theme = (mapIndex % 15) / 5;
            if (theme == 0) maps.SetSea();
            else if (theme == 1) maps.SetSky();
            else if (theme == 2) maps.SetGround();
            else Debug.Log("Error");

            if (mapIndex % 15 == 0)
            {
                GameObject[] animals = GameObject.FindGameObjectsWithTag("fish").Concat(GameObject.FindGameObjectsWithTag("bird")).Concat(GameObject.FindGameObjectsWithTag("dino")).ToArray();
                foreach (GameObject animal in animals)
                {
                    Destroy(animal);
                }
            }

            nextSceneTrigger = false;
        }
    }

    public void Reconnect()
    {
        if (socket != null && (socket.ReadyState == WebSocketState.Open || socket.ReadyState == WebSocketState.Connecting)) socket.Close();

        IP = GameObject.Find("Input").transform.GetChild(0).GetComponent<InputField>().text;
        port = GameObject.Find("Input").transform.GetChild(1).GetComponent<InputField>().text;

        File.WriteAllText(Path.Combine(Application.persistentDataPath, "socket.txt"), $"{IP}\n{port}");

        socket = new WebSocket($"ws://{IP}:{port}");
        socket.Connect();
        socket.OnMessage += (sender, e) =>
        {
            data = e.Data;
            isReceived = true;
        };
    }

    public void HiddenMenu()
    {
        if (optionCount >= 6)
        {
            options.SetActive(!options.activeSelf ? true : false);
            optionCount = 0;
        } else optionCount++;
    }

    private void OnApplicationQuit()
    {
        socket.Close();
    }
}
