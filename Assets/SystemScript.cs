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
    string data;                                // 웹소켓으로 전송 받은 데이터를 저장할 변수
    public Texture2D testImage;                 // 테스트 이미지
    public int theme = 0;                      // 테마 기본값 -1 -> 선택하면 0, 1, 2 중 하나

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

        /* 이미지 전송 테스트용 캡쳐 이미지 보내기
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
            JSONObject receivedData = new JSONObject(data);                     // JSONObject로 변환
            string type = receivedData.GetField("type").stringValue;            // type 추출

            /**************************
            start(request) -> theme(response): 시작할 때 테마 보내주기
            image: 이미지 받을 때
            **************************/

            if (type == "start")            // 시작할 때 테마 보내주기
            {
                JSONObject sendData = new JSONObject();                         // JSONObject 생성
                sendData.AddField("type", "theme");                             // 타입 추가
                sendData.AddField("theme", theme);                              // 테마 번호 추가
                socket.Send(sendData.ToString());
            }
            else if (type == "image")       // 이미지 받을 때
            {
                int creature = receivedData.GetField("animal").intValue;      // 동물 번호 추출
                byte[] imageData = System.Convert.FromBase64String(receivedData.GetField("image").stringValue);         // 이미지 데이터 추출 (base64string -> byte[])
                Debug.Log($"Theme: {theme}, creature: {creature}");

                Texture2D receivedImage = new Texture2D(2, 2);                  // 텍스쳐 생성
                bool isLoaded = receivedImage.LoadImage(imageData);             // byte[] -> 텍스쳐 적용

                if (isLoaded)
                {
                    GameObject[] group = theme == 0 ? fishes : theme == 1 ? birds : dinos;
                    GameObject animal = group[creature];
                    Vector3 position = Vector3.zero;
                    GameObject obj = Instantiate(animal);
                    obj.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = receivedImage;
                    // 텍스쳐를 재질로 적용
                }
            }
            isReceived = false;
            data = null;                                                        // 전송 받은 데이터 초기화
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
