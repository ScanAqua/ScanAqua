using UnityEngine;
using WebSocketSharp;
using Defective.JSON;

public class SystemScript : MonoBehaviour
{
    public string IP = "127.0.0.1";
    public string port = "9999";
    WebSocket socket;
    public bool isReceived = false;
    string data;                                // ���������� ���� ���� �����͸� ������ ����
    public GameObject testObject;               // ������ �迭�� ���� ����
    public int theme = -1;                      // �׸� �⺻�� -1 -> �����ϸ� 0, 1, 2 �� �ϳ�

    void Awake()
    {
        socket = new WebSocket($"ws://{IP}:{port}");
        socket.Connect();
        socket.OnMessage += (sender, e) =>
        {
            data = e.Data;  
            isReceived = true;
        };
    }

    void Update()
    {
        /* �̹��� ���� �׽�Ʈ�� ĸ�� �̹��� ������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            byte[] decodedImage = ScreenCapture.CaptureScreenshotAsTexture().EncodeToPNG();
            JSONObject sendData = new JSONObject();
            sendData.AddField("type", "image");
            sendData.AddField("creature", theme == 0? Random.Range(0, 10) : Random.Range(0, 5));
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
                int creature = receivedData.GetField("creature").intValue;      // ���� ��ȣ ����
                byte[] imageData = System.Convert.FromBase64String(receivedData.GetField("image").stringValue);         // �̹��� ������ ���� (base64string -> byte[])
                Debug.Log($"Theme: {theme}, creature: {creature}");

                Texture2D receivedImage = new Texture2D(2, 2);                  // �ؽ��� ����
                bool isLoaded = receivedImage.LoadImage(imageData);             // byte[] -> �ؽ��� ����
                GameObject test = Instantiate(testObject, new Vector3(Random.Range(-40f, 40f), Random.Range(0, 50f), 0), Quaternion.identity);  // ������ ����
                test.GetComponent<MeshRenderer>().material.mainTexture = receivedImage;                                                         // �ؽ��ĸ� ������ ����
            }
            isReceived = false;
            data = null;                                                        // ���� ���� ������ �ʱ�ȭ
        }
    }

    private void OnApplicationQuit()
    {
        socket.Close();
    }
}
