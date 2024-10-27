using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class DinosaurManager : MonoBehaviour
{
    public GameObject Pterodactyl; public GameObject Spinosaurus; public GameObject T_Rex; public GameObject Triceratops; public GameObject Velociraptor;
    private GameObject newDinosaur;
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
            SpawnDinosaur(fileName);

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
                newDinosaur.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.mainTexture = newTexture;
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
    private void SpawnDinosaur(string fileName)
    {
        GameObject DinosaurPrefab = null;
        Vector3 spawnPosition;

        // ���� �̸��� ���� ������ ����
        switch (Regex.Replace(fileName, @"\d", "").ToLower()) // �ҹ��ڷ� ��ȯ�Ͽ� ��
        {
            case "pterodactyl":
                DinosaurPrefab = Pterodactyl;
                spawnPosition = new Vector3(Random.Range(-25, 25), Random.Range(20, 3), Random.Range(50, 20));
                break;
            case "spinosaurus":
                DinosaurPrefab = Spinosaurus;
                spawnPosition = new Vector3(Random.Range(-25, 25), -2, Random.Range(30, 10));
                break;
            case "t_rex":
                DinosaurPrefab = T_Rex;
                spawnPosition = new Vector3(Random.Range(-25, 25), -2, Random.Range(30, 10));
                break;
            case "triceratops":
                DinosaurPrefab = Triceratops;
                spawnPosition = new Vector3(Random.Range(-25, 25), -2, Random.Range(30, 10));
                break;
            case "velociraptor":
                DinosaurPrefab = Velociraptor;
                spawnPosition = new Vector3(Random.Range(-25, 25), -2, Random.Range(30, 10));
                break;
            default:
                Debug.LogError("�ش��ϴ� �������� �����ϴ�: " + fileName);
                return;
        }

        // �������� ������ ��쿡�� ����
        if (DinosaurPrefab != null)
        {

            newDinosaur = Instantiate(DinosaurPrefab, spawnPosition, Quaternion.Euler(0, 90, 0));
            StartCoroutine(LoadTexture(fileName));

        }
    }
}
