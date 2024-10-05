using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    // UI ���
    public TextMeshProUGUI TimeTxt; // ȭ���� "�ð�" ����
    public TextMeshProUGUI TimerTxt; // ȭ���� ���� �ð� ǥ�� (���� Ÿ�̸�)
    public TextMeshProUGUI BackgroundTimerTxt; // ��� ��ȯ Ÿ�̸� ǥ�� �ؽ�Ʈ
    public GameObject ResultPanel; // ���â
    public GameObject SettingPanel; // ����â
    public GameObject StopBtn; // ���� ���� ��ư

    // ��� ������Ʈ
    public GameObject Back1, Back2, Back3, Back4; // �� ���� ���
    private GameObject[] Backgrounds; // ��� �迭

    // Ÿ�̸� ����
    private float gameRemainTime = 0f; // ���� Ÿ�̸� ���� �ð�
    private bool isGameTimerRunning = false; // ���� Ÿ�̸� ���� ����

    private float backgroundChangeInterval = 180f; // 3��(180��) �������� ��� ����
    private int currentBackgroundIndex = 0; // ���� ��� �ε���
    private float backgroundRemainTime = 180f; // ��� ��ȯ���� ���� �ð� (��)
    private bool isBackgroundTimerRunning = true; // ��� Ÿ�̸� ���� ���� (�⺻ Ȱ��ȭ)

    void Start()
    {
        // ��� �迭 �ʱ�ȭ
        Backgrounds = new GameObject[] { Back1, Back2, Back3, Back4 };

        // �ʱ� ���� ����
        TimeTxt.gameObject.SetActive(false);
        TimerTxt.gameObject.SetActive(false);
        BackgroundTimerTxt.gameObject.SetActive(true); // �⺻������ Ȱ��ȭ
        SettingPanel.gameObject.SetActive(false);
        ResultPanel.gameObject.SetActive(false);
        StopBtn.gameObject.SetActive(false); // �ʱ� ���¿��� StopBtn ����

        // ù ��� ���� �� ��� Ÿ�̸� �ʱ�ȭ
        ChangeBackground();
        UpdateBackgroundTimerTxt();
    }

    void Update()
    {
        // ��� ��ȯ Ÿ�̸� ������Ʈ
        if (isBackgroundTimerRunning)
        {
            backgroundRemainTime -= Time.deltaTime;
            UpdateBackgroundTimerTxt();

            if (backgroundRemainTime <= 0f)
            {
                ChangeBackground();
                backgroundRemainTime = backgroundChangeInterval; // Ÿ�̸� �ʱ�ȭ
                UpdateBackgroundTimerTxt();
            }
        }

        // ���� Ÿ�̸� ������Ʈ
        if (isGameTimerRunning)
        {
            gameRemainTime -= Time.deltaTime;
            UpdateGameTimerTxt();

            if (gameRemainTime <= 0f)
            {
                EndGame();
            }
        }
    }

    // 3�� Ÿ�̸� ����
    public void Set3MinTimer()
    {
        StartGameTimer(3);
        Start3MinGameLogic();
    }

    // 6�� Ÿ�̸� ����
    public void Set6MinTimer()
    {
        StartGameTimer(6);
        Start6MinGameLogic();
    }

    // 9�� Ÿ�̸� ����
    public void Set9MinTimer()
    {
        StartGameTimer(9);
        Start9MinGameLogic();
    }

    // 10�� Ÿ�̸� ����
    public void Set10MinTimer()
    {
        StartGameTimer(10);
        Start10MinGameLogic();
    }

    // ���� Ÿ�̸� ����
    private void StartGameTimer(int minutes)
    {
        // ���� Ȱ��ȭ�� ��� ��Ȱ��ȭ
        DeactivateAllBackgrounds();

        // ���� Ÿ�̸� �ʱ�ȭ
        gameRemainTime = minutes * 60f;
        isGameTimerRunning = true;

        // UI ��� Ȱ��ȭ
        TimeTxt.gameObject.SetActive(true);
        TimerTxt.gameObject.SetActive(true);
        BackgroundTimerTxt.gameObject.SetActive(false); // ��� Ÿ�̸� ��Ȱ��ȭ
        StopBtn.gameObject.SetActive(true);

        // ���� �г� �ݱ�
        SettingPanel.gameObject.SetActive(false);

        // ���â �����
        ResultPanel.gameObject.SetActive(false);
        UpdateGameTimerTxt();

        // ��� Ÿ�̸� ����
        isBackgroundTimerRunning = false;
    }

    // ���� Ÿ�̸� �ؽ�Ʈ ������Ʈ
    void UpdateGameTimerTxt()
    {
        if (gameRemainTime > 0)
        {
            int minutes = Mathf.FloorToInt(gameRemainTime / 60);
            int seconds = Mathf.FloorToInt(gameRemainTime % 60);
            TimerTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else
        {
            TimerTxt.text = "0:00";
        }
    }

    // ��� ��ȯ Ÿ�̸� �ؽ�Ʈ ������Ʈ
    void UpdateBackgroundTimerTxt()
    {
        if (backgroundRemainTime > 0)
        {
            int minutes = Mathf.FloorToInt(backgroundRemainTime / 60);
            int seconds = Mathf.FloorToInt(backgroundRemainTime % 60);
            BackgroundTimerTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else
        {
            BackgroundTimerTxt.text = "0:00";
        }
    }

    // ����� �����ϴ� �Լ�
    void ChangeBackground()
    {
        // ��� ����� ��Ȱ��ȭ
        DeactivateAllBackgrounds();

        // ���� �ε����� ���� ��� Ȱ��ȭ
        if (currentBackgroundIndex >= 0 && currentBackgroundIndex < Backgrounds.Length)
        {
            Backgrounds[currentBackgroundIndex].SetActive(true);
        }

        // ���� ������� �ε��� ���� (��ȯ)
        currentBackgroundIndex = (currentBackgroundIndex + 1) % Backgrounds.Length;
    }

    // ��� ��� ��Ȱ��ȭ �Լ�
    void DeactivateAllBackgrounds()
    {
        foreach (GameObject bg in Backgrounds)
        {
            bg.SetActive(false);
        }
    }

    // �ð��� ������ �� ���� ���� �� ���â ǥ��
    void EndGame()
    {
        isGameTimerRunning = false;
        gameRemainTime = 0f;
        TimerTxt.text = "0:00"; // 0:00���� ���߱�
        StopBtn.gameObject.SetActive(false); // ���� ���� �� StopBtn ����

        // ���â ǥ��
        ResultPanel.gameObject.SetActive(true);

        // ��� Ÿ�̸� ����
        isBackgroundTimerRunning = false;
        BackgroundTimerTxt.gameObject.SetActive(false);
    }

    // ���â�� �ݰ� �ʱ� ���·� ���ư���
    public void CloseResultPanel()
    {
        ResultPanel.gameObject.SetActive(false);
        TimerTxt.text = ""; // Ÿ�̸� �ؽ�Ʈ �ʱ�ȭ
        TimeTxt.gameObject.SetActive(false);
        TimerTxt.gameObject.SetActive(false);
        isGameTimerRunning = false;
        StopBtn.gameObject.SetActive(false); // ���â ���� �� StopBtn ����

        // ��� Ÿ�̸� �簳 �� Ȱ��ȭ
        isBackgroundTimerRunning = true;
        BackgroundTimerTxt.gameObject.SetActive(true);

        // ��� ��� Ȱ��ȭ
        ChangeBackground();
    }

    // ���� ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void StopGame()
    {
        EndGame(); // ���� ���� ���� ȣ��
    }

    void Start3MinGameLogic()
    {
        Debug.Log("3��¥�� ���� ����");
    }

    void Start6MinGameLogic()
    {
        Debug.Log("6��¥�� ���� ����");
    }

    void Start9MinGameLogic()
    {
        Debug.Log("9��¥�� ���� ����");
    }

    void Start10MinGameLogic()
    {
        Debug.Log("10��¥�� ���� ����");
    }
}