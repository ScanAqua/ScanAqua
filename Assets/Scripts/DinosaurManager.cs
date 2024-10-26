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
    private bool fileDetected = false;  // 파일 감지를 나타내는 플래그 변수

    void Start()
    {
        string path = Application.dataPath + "/Resources/Textures";
        // PNG 파일 감지용 FileSystemWatcher
        FileSystemWatcher pngWatcher = new FileSystemWatcher();
        pngWatcher.Path = path;
        pngWatcher.Filter = "*.png";
        pngWatcher.Created += OnFileCreated;
        pngWatcher.EnableRaisingEvents = true;

        // JPG 파일 감지용 FileSystemWatcher
        FileSystemWatcher jpgWatcher = new FileSystemWatcher();
        jpgWatcher.Path = path;
        jpgWatcher.Filter = "*.jpg";
        jpgWatcher.Created += OnFileCreated;
        jpgWatcher.EnableRaisingEvents = true;
    }

    private void OnFileCreated(object sender, FileSystemEventArgs e)
    {
        // .meta 파일이면 무시
        if (fileName.EndsWith(".png"))
        {
            return; // .meta 파일을 무시하고 종료
        }
        Debug.Log("새로운 파일이 감지되었습니다: " + e.Name);
        fileName = Path.GetFileNameWithoutExtension(e.Name);  // 파일 이름을 저장
        fileDetected = true;  // 파일 감지 플래그를 true로 설정
    }

    void Update()
    {
        // 파일이 감지되었다면 메인 스레드에서 작업 처리
        if (fileDetected)
        {
            SpawnDinosaur(fileName);

            Debug.Log("새로운 공룡 생성되었습니다.");

            // 파일 감지 플래그 리셋
            fileDetected = false;
        }
    }

    private IEnumerator LoadTexture(string fileName)
    {
        string fullPath = Path.Combine(Application.dataPath, "Resources/Textures", fileName + ".png");

        // 파일이 존재하는지 확인
        if (File.Exists(fullPath))
        {
            // 파일을 바이트 배열로 읽기
            byte[] fileData = File.ReadAllBytes(fullPath);

            // 텍스처를 생성하고 바이트 데이터를 로드
            Texture2D newTexture = new Texture2D(2, 2);
            if (newTexture.LoadImage(fileData))
            {
                Debug.Log("텍스처가 로드되었습니다: " + fileName);

                // SkinnedMeshRenderer에 텍스처 적용
                newDinosaur.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.mainTexture = newTexture;
            }
            else
            {
                Debug.LogError("텍스처 로드에 실패했습니다: " + fileName);
            }
        }
        else
        {
            Debug.LogError("파일을 찾을 수 없습니다: " + fullPath);
        }

        yield return null;
    }
    private void SpawnDinosaur(string fileName)
    {
        GameObject DinosaurPrefab = null;
        Vector3 spawnPosition;

        // 파일 이름에 따라 프리팹 선택
        switch (Regex.Replace(fileName, @"\d", "").ToLower()) // 소문자로 변환하여 비교
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
                Debug.LogError("해당하는 프리팹이 없습니다: " + fileName);
                return;
        }

        // 프리팹이 설정된 경우에만 생성
        if (DinosaurPrefab != null)
        {

            newDinosaur = Instantiate(DinosaurPrefab, spawnPosition, Quaternion.Euler(0, 90, 0));
            StartCoroutine(LoadTexture(fileName));

        }
    }
}
