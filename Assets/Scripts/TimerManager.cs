using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI TimeTxt; // ȭ���� "�ð�" ����
    public TextMeshProUGUI TimerTxt; // ȭ���� ���� �ð� ǥ��
    public GameObject ResultPanel; // ���â
    public GameObject SettingPanel; // ����â
    public GameObject StopBtn; // ���� ���� ��ư �߰�
    private float RemainTime = 0; // ���� �ð�
    private bool inTimerRunning = false; // Ÿ�̸� ���� ����

    void Start()
    {
        TimeTxt.gameObject.SetActive(false);
        TimerTxt.gameObject.SetActive(false);
        SettingPanel.SetActive(false);
        ResultPanel.SetActive(false);
        StopBtn.SetActive(false); // �ʱ� ���¿��� StopBtn ����
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

    // 3�� Ÿ�̸� ����
    public void Set3MinTimer()
    {
        SetTimer(3);
        Start3MinGameLogic();
    }

    // 6�� Ÿ�̸� ����
    public void Set6MinTimer()
    {
        SetTimer(6);
        Start6MinGameLogic();
    }

    // 9�� Ÿ�̸� ����
    public void Set9MinTimer()
    {
        SetTimer(9);
        Start9MinGameLogic();
    }

    // 10�� Ÿ�̸� ����
    public void Set10MinTimer()
    {
        SetTimer(10);
        Start10MinGameLogic();
    }

    // Ÿ�̸� ���뼳��
    private void SetTimer(int minutes)
    {
        RemainTime = minutes * 60; // ���� �ʷ� ��ȯ
        inTimerRunning = true;

        // TimeTxt�� TimerTxt�� ȭ�鿡 ǥ��
        TimeTxt.gameObject.SetActive(true);
        TimerTxt.gameObject.SetActive(true);
        StopBtn.SetActive(true); // ���� ���� �� StopBtn ǥ��

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

        // ���� ������ 0:00���� ���߱�
        if (RemainTime <= 0)
        {
            TimerTxt.text = "0:00";
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
        TimerTxt.text = "0:00"; // 0:00���� ���߱�
        StopBtn.SetActive(false); // ���� ���� �� StopBtn ����
        ResultPanel.SetActive(true); // ���â ǥ��
    }

    // ���â�� �ݰ� �ʱ� ���·� ���ư���
    public void CloseResultPanel()
    {
        ResultPanel.SetActive(false);
        TimerTxt.text = ""; // Ÿ�̸� �ؽ�Ʈ �ʱ�ȭ
        TimeTxt.gameObject.SetActive(false);
        TimerTxt.gameObject.SetActive(false);
        inTimerRunning = false;
        StopBtn.SetActive(false); // ���â ���� �� StopBtn ����

        SettingPanel.SetActive(false);
    }

    // ���� ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void StopGame()
    {
        EndGame(); // ���� ���� ���� ȣ��
    }

    void Start3MinGameLogic()
    {
        Debug.Log("3��¥�� ���ӽ���");
    }

    void Start6MinGameLogic()
    {
        Debug.Log("6��¥�� ���ӽ���");
    }

    void Start9MinGameLogic()
    {
        Debug.Log("9��¥�� ���ӽ���");
    }

    void Start10MinGameLogic()
    {
        Debug.Log("10��¥�� ���ӽ���");
    }
}