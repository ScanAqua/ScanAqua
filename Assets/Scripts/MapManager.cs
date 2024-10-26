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

    public GameObject uiCanvas;

    private VideoPlayer videoPlayer;
    private GameObject[] fishes;
    private float speed = 0;
    public void OnSeaButtonClick()
    {
        seaPanel.SetActive(true);  // SeaPanel Ȱ��ȭ
        fishManager.SetActive(true);
        PlayVideo(seaPanel);       // �ٴ� ���� ���
    }

    public void OnSkyButtonClick()
    {
        skyPanel.SetActive(true);  // SkyPanel Ȱ��ȭ
        birdManager.SetActive(true);
        PlayVideo(skyPanel);       // �ϴ� ���� ���
    }

    public void OnGroundButtonClick()
    {
        groundPanel.SetActive(true);  // GroundPanel Ȱ��ȭ
        dinosaurManager.SetActive(true);
        PlayVideo(groundPanel);       // �� ���� ���
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
        if (videoPlayer.time >= 26 && videoPlayer.time <= 41)
        {
            if (videoPlayer.time >= 26 && speed < 10)
            {
                speed += 1.6f * Time.deltaTime;
            }
            else if(videoPlayer.time > 32 && videoPlayer.time < 35)
            {
                speed = 10f;
            }
            else if (videoPlayer.time >= 35 && speed < 10)
            {

            }
            fishes = GameObject.FindGameObjectsWithTag("fish");
            foreach (GameObject fish in fishes)
            {
                fish.transform.Translate(Vector3.forward * -speed * Time.deltaTime, Space.World);
            }
            
        }
    }
}
