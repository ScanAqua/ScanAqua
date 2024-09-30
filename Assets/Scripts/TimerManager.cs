using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI TimeTxt; // "�ð�" �ؽ�Ʈ
    public TextMeshProUGUI TimerTxt; // ���� �ð��� ǥ���� TextMeshPro
    public GameObject ResultPanel; // ���â
    public GameObject SettingPanel; // ���� �г�
    private float RemainTime = 0; // ���� �ð�
    private bool inTimerRunning = false; // Ÿ�̸� ���� ����

    void Start()
    {
        // �ʱ� ����: TimeTxt, TimerTxt, SettingPanel, ResultPanel ��� ������ ����
        TimeTxt.gameObject.SetActive(false);
        TimerTxt.gameObject.SetActive(false);
        SettingPanel.SetActive(false);
        ResultPanel.SetActive(false);
    }

    void Update()
    {
        if (inTimerRunning)
        {
            RemainTime -= Time.deltaTime;
            UpdateTimerTxt();

            if (RemainTime <= 0)
            {
                EndGame();
            }
        }
    }

    // ��ư�� ������ �ش��ϴ� �ð��� �����ǰ� Ÿ�̸� ����
    public void SetTimer(int minutes)
    {
        RemainTime = minutes * 60; // ���� �ʷ� ��ȯ
        inTimerRunning = true;

        // TimeTxt�� TimerTxt�� ȭ�鿡 ǥ��
        TimeTxt.gameObject.SetActive(true);
        TimerTxt.gameObject.SetActive(true);

        // SettingPanel �ݱ�
        SettingPanel.SetActive(false);

        ResultPanel.SetActive(false); // ���â �����
        UpdateTimerTxt();
    }

    // Ÿ�̸� �ؽ�Ʈ ������Ʈ
    void UpdateTimerTxt()
    {
        int minutes = Mathf.FloorToInt(RemainTime / 60);
        int seconds = Mathf.FloorToInt(RemainTime % 60);

        // 00:00���� ���߱�
        if (RemainTime <= 0)
        {
            TimerTxt.text = "00:00";
        }
        else
        {
            TimerTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }

    // �ð��� ������ �� ���� ���� �� ���â ǥ��
    void EndGame()
    {
        inTimerRunning = false;
        RemainTime = 0;
        TimerTxt.text = "0:00"; // 00:00���� ���߱�
        ResultPanel.SetActive(true); // ���â ǥ��
    }

    // ���â�� �ݰ� �ʱ� ���·� ���ư���
    public void CloseResultPanel()
    {
        // ��� UI ��Ҹ� �ʱ� ���·� �����
        ResultPanel.SetActive(false);
        TimerTxt.text = ""; // Ÿ�̸� �ؽ�Ʈ �ʱ�ȭ
        TimeTxt.gameObject.SetActive(false); // TimeTxt �����
        TimerTxt.gameObject.SetActive(false); // TimerTxt �����
        inTimerRunning = false;

        // SettingPanel ���� ���·� �ʱ�ȭ
        SettingPanel.SetActive(false);
    }
}