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
        seaPanel.SetActive(true);  // SeaPanel Ȱ��ȭ
        fishManager.SetActive(true);
        PlayVideo(seaPanel);       // �ٴ� ���� ���
        shadow.transform.position = new Vector3(0, -20, 60);
    }

    public void OnSkyButtonClick()
    {
        skyPanel.SetActive(true);  // SkyPanel Ȱ��ȭ
        birdManager.SetActive(true);
        PlayVideo(skyPanel);       // �ϴ� ���� ���
        shadow.transform.position = new Vector3(0, -5, 60);
    }

    public void OnGroundButtonClick()
    {
        groundPanel.SetActive(true);  // GroundPanel Ȱ��ȭ
        dinosaurManager.SetActive(true);
        PlayVideo(groundPanel);       // �� ���� ���
        shadow.transform.position = new Vector3(0, -2, 60);
    }

    private void PlayVideo(GameObject panel)
    {
        videoPlayer = panel.GetComponent<VideoPlayer>(); // �гο� �ִ� VideoPlayer ������Ʈ ������

        uiCanvas.SetActive(false); // UI ��Ȱ��ȭ

        videoPlayer.Play();                              // ���� ���
        videoPlayer.loopPointReached += EndReached;      // ���� ������ �̺�Ʈ�� �Լ� ���
    }

    private void EndReached(VideoPlayer vp)
    {
        vp.gameObject.SetActive(false);  // �г� ��Ȱ��ȭ
        uiCanvas.SetActive(true); //UI �ٽ� Ȱ��ȭ
    }
    void Update()
    {
        // 1. �ð��� 5�� �̻� �Ǿ��� ��
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
