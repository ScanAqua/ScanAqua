using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MapManager : MonoBehaviour
{
    public GameObject seaPanel;
    public GameObject skyPanel;
    public GameObject groundPanel;
    public GameObject fishManager;
    public GameObject birdManager;
    public GameObject dinosaurManager;
    public GameObject shadow;
    public GameObject uiCanvas;

    private VideoPlayer videoPlayer;
    private GameObject[] fishes;
    private float speed = 0;
    public void OnSeaButtonClick()
    {
        seaPanel.SetActive(true);  // SeaPanel 활성화
        fishManager.SetActive(true);
        PlayVideo(seaPanel);       // 바다 영상 재생
        shadow.transform.position = new Vector3(0, -20, 60);
    }

    public void OnSkyButtonClick()
    {
        skyPanel.SetActive(true);  // SkyPanel 활성화
        birdManager.SetActive(true);
        PlayVideo(skyPanel);       // 하늘 영상 재생
        shadow.transform.position = new Vector3(0, -5, 60);
    }

    public void OnGroundButtonClick()
    {
        groundPanel.SetActive(true);  // GroundPanel 활성화
        dinosaurManager.SetActive(true);
        PlayVideo(groundPanel);       // 땅 영상 재생
        shadow.transform.position = new Vector3(0, -2, 60);
    }

    private void PlayVideo(GameObject panel)
    {
        videoPlayer = panel.GetComponent<VideoPlayer>(); // 패널에 있는 VideoPlayer 컴포넌트 가져옴

        uiCanvas.SetActive(false); // UI 비활성화

        videoPlayer.Play();                              // 영상 재생
        videoPlayer.loopPointReached += EndReached;      // 영상 끝나는 이벤트에 함수 등록
    }

    private void EndReached(VideoPlayer vp)
    {
        vp.gameObject.SetActive(false);  // 패널 비활성화
        uiCanvas.SetActive(true); //UI 다시 활성화
    }
    void Update()
    {
        // 1. 시간이 5초 이상 되었을 때
        if (videoPlayer.time >= 26-23 && videoPlayer.time <= 41-23)
        {
            if (videoPlayer.time >= 26 - 23)
            {
                if(speed < 40) speed += 6f * Time.deltaTime;
            }
            else if(videoPlayer.time > 32 - 23 && videoPlayer.time < 35 - 23)
            {
                speed = 40f;
            }
            else if (videoPlayer.time >= 35 - 23)
            {
                if(speed > 0) speed -= 6f * Time.deltaTime;
            }
            fishes = GameObject.FindGameObjectsWithTag("fish");
            foreach (GameObject fish in fishes)
            {
                fish.transform.Translate(Vector3.forward * -speed * Time.deltaTime, Space.World);
                fish.GetComponent<FishScript>().end = true;
            }
        }
        //else if (videoPlayer.time >= 3 && videoPlayer.time <= 18){
    }
}
