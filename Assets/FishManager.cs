using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FishManager : MonoBehaviour
{
    public GameObject fishPrefab;
    private GameObject newFish;
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
        if (fileName.EndsWith(".jpg"))
        {
            return; // .meta ������ �����ϰ� ����
        }
        Debug.Log("���ο� ������ �����Ǿ����ϴ�: " + e.Name);
        fileName = e.Name;  // ���� �̸��� ����
        fileDetected = true;  // ���� ���� �÷��׸� true�� ����
    }

    void Update()
    {
        // ������ �����Ǿ��ٸ� ���� �����忡�� �۾� ó��
        if (fileDetected)
        {
            // ����� ����
            Vector3 spawnPosition = new Vector3(Random.Range(-15, 15), Random.Range(-9, 9), 0);
            newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.Euler(0, 90, 0));
            Debug.Log("���ο� ����Ⱑ �����Ǿ����ϴ�.");

            // �ڷ�ƾ���� �ؽ�ó �ε�
            StartCoroutine(LoadTexture(fileName));

            // ���� ���� �÷��� ����
            fileDetected = false;
        }
    }

    private IEnumerator LoadTexture(string fileName)
    {
        string fullPath = Path.Combine(Application.dataPath, "Resources/Textures", fileName);

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

                // ������� SkinnedMeshRenderer�� �ؽ�ó ����
                newFish.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.mainTexture = newTexture;
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
}