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
        seaPanel.SetActive(true);  // SeaPanel Ȱ��ȭ
        GameObject.Find("System").GetComponent<SystemScript>().theme = 0;
        PlayVideo(seaPanel);       // �ٴ� ���� ���
    }

    public void OnSkyButtonClick()
    {
        skyPanel.SetActive(true);  // SkyPanel Ȱ��ȭ
        GameObject.Find("System").GetComponent<SystemScript>().theme = 1;
        PlayVideo(skyPanel);       // �ϴ� ���� ���
    }

    public void OnGroundButtonClick()
    {
        groundPanel.SetActive(true);  // GroundPanel Ȱ��ȭ
        GameObject.Find("System").GetComponent<SystemScript>().theme = 2;
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
}
