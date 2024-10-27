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
        fishManager.SetActive(false);
        birdManager.SetActive(false);
        dinosaurManager.SetActive(false);
    }
    void Update()
    {
        // 1. �ð��� 26�� �̻� �Ǿ��� ��
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
