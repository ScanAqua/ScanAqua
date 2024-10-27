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
    public GameObject uiUp;
    public GameObject uiDown;

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
        fishManager.SetActive(false);
        birdManager.SetActive(false);
        dinosaurManager.SetActive(false);
    }
    void Update()
    {
        // 1. 시간이 26초 이상 되었을 때
        if (videoPlayer.time >= 26 && videoPlayer.time <= 41)
        {
            if (videoPlayer.time >= 26)
            {
                if (speed < 60) speed += 10f * Time.deltaTime;

                Vector3 uiUpVector = uiUp.GetComponent<RectTransform>().localPosition;
                Vector3 uiDownVector = uiDown.GetComponent<RectTransform>().localPosition;
                if (uiUpVector.y > 482)
                {
                    uiUp.GetComponent<RectTransform>().localPosition = uiUpVector - new Vector3(0, 118 * Time.deltaTime, 0);
                    uiDown.GetComponent<RectTransform>().localPosition = uiDownVector + new Vector3(0, 131 * Time.deltaTime, 0);
                }
                else
                {
                    uiUp.GetComponent<RectTransform>().localPosition = new Vector3(0, 482, 0);
                    uiDown.GetComponent<RectTransform>().localPosition = new Vector3(0, -469, 0);
                }

            }
            else if (videoPlayer.time > 32 && videoPlayer.time < 36)
            {
                speed = 60f;
            }
            else if (videoPlayer.time >= 36)
            {
                if (speed > 0) speed -= 10f * Time.deltaTime;
            }
            fishes = GameObject.FindGameObjectsWithTag("fish");
            foreach (GameObject fish in fishes)
            {
                fish.transform.Translate(Vector3.forward * -speed * Time.deltaTime, Space.World);
                fish.GetComponent<FishScript>().end = true;
            }
        }

        else if (videoPlayer.time >= 75 && videoPlayer.time <= 85)
        {
            if (videoPlayer.time >= 75)
            {
                if (speed < 60) speed += 12f * Time.deltaTime;
            }
            else if (videoPlayer.time > 80  && videoPlayer.time < 82 )
            {
                speed = 60f;
            }
            else if (videoPlayer.time >= 82)
            {
                if (speed > 0) speed -= 20f * Time.deltaTime;
            }
            fishes = GameObject.FindGameObjectsWithTag("fish");
            foreach (GameObject fish in fishes)
            {
                fish.transform.Translate(Vector3.forward * -speed * Time.deltaTime, Space.World);
                fish.GetComponent<FishScript>().end = true;
            }
        }

        else if (videoPlayer.time >= 127 && videoPlayer.time <= 140)
        {
            if (videoPlayer.time >= 127)
            {
                if (speed < 60) speed += 20f * Time.deltaTime;
            }
            else if (videoPlayer.time > 130 && videoPlayer.time < 138)
            {
                speed = 60f;
            }
            else if (videoPlayer.time >= 138)
            {
                if (speed > 0) speed -= 30f * Time.deltaTime;
            }
            fishes = GameObject.FindGameObjectsWithTag("fish");
            foreach (GameObject fish in fishes)
            {
                fish.transform.Translate(Vector3.forward * -speed * Time.deltaTime, Space.World);
                fish.GetComponent<FishScript>().end = true;
            }
        }
        else
        {
            speed = 0;

            Vector3 uiUpVector = uiUp.GetComponent<RectTransform>().localPosition;
            Vector3 uiDownVector = uiDown.GetComponent<RectTransform>().localPosition;
            if (uiUpVector.y < 718)
            {
                uiUp.GetComponent<RectTransform>().localPosition = uiUpVector + new Vector3(0, 118 * Time.deltaTime, 0);
                uiDown.GetComponent<RectTransform>().localPosition = uiDownVector - new Vector3(0, 131 * Time.deltaTime, 0);
            }
            else
            {
                uiUp.GetComponent<RectTransform>().localPosition = new Vector3(0, 718, 0);
                uiDown.GetComponent<RectTransform>().localPosition = new Vector3(0, -731, 0);
            }
        }
    }
}
