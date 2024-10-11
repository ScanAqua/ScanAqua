using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FishManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject BassletFish; public GameObject BlueYellowFish; public GameObject ButterflyFish; public GameObject Clownfish; public GameObject Gray_mullet; public GameObject Konosiruspunctatus; public GameObject StripedFish; public GameObject Sweetfish; public GameObject TriggerFish; public GameObject YellowAngelFish;
    private GameObject newFish;
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
            // 물고기 생성
            //Vector3 spawnPosition = new Vector3(Random.Range(-15, 15), Random.Range(-9, 9), 0);
            //newFish = Instantiate(BassletFish, spawnPosition, Quaternion.Euler(0, 90, 0));

            Debug.Log("새로운 물고기가 생성되었습니다.");

            // 코루틴으로 텍스처 로드
            StartCoroutine(LoadTexture(fileName));

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

                // 물고기의 SkinnedMeshRenderer에 텍스처 적용
                newFish.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.mainTexture = newTexture;
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
    private void SpawnFish(string fileName)
    {
        GameObject fishPrefab = null;

        // 파일 이름에 따라 프리팹 선택
        switch (fileName.ToLower()) // 소문자로 변환하여 비교
        {
            case "bassletfish":
                fishPrefab = BassletFish;
                break;
            case "blueyellowfish":
                fishPrefab = BlueYellowFish;
                break;
            case "butterflyfish":
                fishPrefab = ButterflyFish;
                break;
            case "clownfish":
                fishPrefab = Clownfish;
                break;
            case "gray_mullet":
                fishPrefab = Gray_mullet;
                break;
            case "konosiruspunctatus":
                fishPrefab = Konosiruspunctatus;
                break;
            case "stripedfish":
                fishPrefab = StripedFish;
                break;
            case "sweetfish":
                fishPrefab = Sweetfish;
                break;
            case "triggerfish":
                fishPrefab = TriggerFish;
                break;
            case "yellowangelfish":
                fishPrefab = YellowAngelFish;
                break;
            default:
                Debug.LogError("해당하는 프리팹이 없습니다: " + fileName);
                return;
        }

        // 프리팹이 설정된 경우에만 생성
        if (fishPrefab != null)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-15, 15), Random.Range(-9, 9), 0);
            newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.Euler(0, 90, 0));
        }
    }
}