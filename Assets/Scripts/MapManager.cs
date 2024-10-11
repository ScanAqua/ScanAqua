using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MapManager : MonoBehaviour
{
    public GameObject seaPanel;
    public GameObject skyPanel;
    public GameObject groundPanel;

    private VideoPlayer videoPlayer;

    public void OnSeaButtonClick()
    {
        seaPanel.SetActive(true);  // SeaPanel Ȱ��ȭ
        PlayVideo(seaPanel);       // �ٴ� ���� ���
    }

    public void OnSkyButtonClick()
    {
        skyPanel.SetActive(true);  // SkyPanel Ȱ��ȭ
        PlayVideo(skyPanel);       // �ϴ� ���� ���
    }

    public void OnGroundButtonClick()
    {
        groundPanel.SetActive(true);  // GroundPanel Ȱ��ȭ
        PlayVideo(groundPanel);       // �� ���� ���
    }

    private void PlayVideo(GameObject panel)
    {
        videoPlayer = panel.GetComponent<VideoPlayer>(); // �гο� �ִ� VideoPlayer ������Ʈ ������
        videoPlayer.Play();                              // ���� ���
        videoPlayer.loopPointReached += EndReached;      // ���� ������ �̺�Ʈ�� �Լ� ���
    }

    private void EndReached(VideoPlayer vp)
    {
        vp.gameObject.SetActive(false);  // �г� ��Ȱ��ȭ
    }
}
