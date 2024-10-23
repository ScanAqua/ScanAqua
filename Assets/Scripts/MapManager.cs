using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MapManager : MonoBehaviour
{
    public GameObject seaPanel;
    public GameObject skyPanel;
    public GameObject groundPanel;

    public GameObject uiCanvas;

    private VideoPlayer videoPlayer;

    public void OnSeaButtonClick()
    {
        seaPanel.SetActive(true);  // SeaPanel 활성화
        GameObject.Find("System").GetComponent<SystemScript>().theme = 0;
        PlayVideo(seaPanel);       // 바다 영상 재생
    }

    public void OnSkyButtonClick()
    {
        skyPanel.SetActive(true);  // SkyPanel 활성화
        GameObject.Find("System").GetComponent<SystemScript>().theme = 1;
        PlayVideo(skyPanel);       // 하늘 영상 재생
    }

    public void OnGroundButtonClick()
    {
        groundPanel.SetActive(true);  // GroundPanel 활성화
        GameObject.Find("System").GetComponent<SystemScript>().theme = 2;
        PlayVideo(groundPanel);       // 땅 영상 재생
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
}
