using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public GameObject Dove; public GameObject Flamingo; public GameObject Ostrich; public GameObject Sparrow; public GameObject Vulture;
    private GameObject newBird;
    public string fileName = "";
    private bool fileDetected = false;  // ���� ������ ��Ÿ���� �÷��� ����

    void Start()
    {
        string path = Application.dataPath + "/Resources/Textures";
        // PNG ���� ������ FileSystemWatcher
        FileSystemWatcher pngWatcher = new FileSystemWatcher();
        pngWatcher.Path = path;
        pngWatcher.Filter = "*.png";
        pngWatcher.Created += OnFileCreated;
        pngWatcher.EnableRaisingEvents = true;

        // JPG ���� ������ FileSystemWatcher
        FileSystemWatcher jpgWatcher = new FileSystemWatcher();
        jpgWatcher.Path = path;
        jpgWatcher.Filter = "*.jpg";
        jpgWatcher.Created += OnFileCreated;
        jpgWatcher.EnableRaisingEvents = true;
    }

    private void OnFileCreated(object sender, FileSystemEventArgs e)
    {
        // .meta �����̸� ����
        if (fileName.EndsWith(".png"))
        {
            return; // .meta ������ �����ϰ� ����
        }
        Debug.Log("���ο� ������ �����Ǿ����ϴ�: " + e.Name);
        fileName = Path.GetFileNameWithoutExtension(e.Name);  // ���� �̸��� ����
        fileDetected = true;  // ���� ���� �÷��׸� true�� ����
    }

    void Update()
    {
        // ������ �����Ǿ��ٸ� ���� �����忡�� �۾� ó��
        if (fileDetected)
        {
            SpawnBird(fileName);

            Debug.Log("���ο� ���� �����Ǿ����ϴ�.");

            // ���� ���� �÷��� ����
            fileDetected = false;
        }
    }

    private IEnumerator LoadTexture(string fileName)
    {
        string fullPath = Path.Combine(Application.dataPath, "Resources/Textures", fileName + ".png");

        // ������ �����ϴ��� Ȯ��
        if (File.Exists(fullPath))
        {
            // ������ ����Ʈ �迭�� �б�
            byte[] fileData = File.ReadAllBytes(fullPath);

            // �ؽ�ó�� �����ϰ� ����Ʈ �����͸� �ε�
            Texture2D newTexture = new Texture2D(2, 2);
            if (newTexture.LoadImage(fileData))
            {
                Debug.Log("�ؽ�ó�� �ε�Ǿ����ϴ�: " + fileName);

                // SkinnedMeshRenderer�� �ؽ�ó ����
                newBird.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.mainTexture = newTexture;
            }
            else
            {
                Debug.LogError("�ؽ�ó �ε忡 �����߽��ϴ�: " + fileName);
            }
        }
        else
        {
            Debug.LogError("������ ã�� �� �����ϴ�: " + fullPath);
        }

        yield return null;
    }
    private void SpawnBird(string fileName)
    {
        GameObject birdPrefab = null;
        Vector3 spawnPosition;

        // ���� �̸��� ���� ������ ����
        switch (Regex.Replace(fileName, @"\d", "").ToLower()) // �ҹ��ڷ� ��ȯ�Ͽ� ��
        {
            case "dove":
                birdPrefab = Dove;
                spawnPosition = new Vector3(Random.Range(-25, 25), Random.Range(20, 3), Random.Range(50, 20));
                break;
            case "flamingo":
                birdPrefab = Flamingo;
                spawnPosition = new Vector3(Random.Range(-25, 25), -5, Random.Range(30, 10));
                break;
            case "ostrich":
                birdPrefab = Ostrich;
                spawnPosition = new Vector3(Random.Range(-25, 25), -5, Random.Range(30, 10));
                break;
            case "sparrow":
                birdPrefab = Sparrow;
                spawnPosition = new Vector3(Random.Range(-25, 25), -5, Random.Range(30, 10));
                break;
            case "vulture":
                birdPrefab = Vulture;
                spawnPosition = new Vector3(Random.Range(-25, 25), Random.Range(20, 3), Random.Range(50, 20));
                break;
            default:
                Debug.LogError("�ش��ϴ� �������� �����ϴ�: " + fileName);
                return;
        }

        // �������� ������ ��쿡�� ����
        if (birdPrefab != null)
        {

            newBird = Instantiate(birdPrefab, spawnPosition, Quaternion.Euler(0, 90, 0));
            StartCoroutine(LoadTexture(fileName));

        }
    }
}
