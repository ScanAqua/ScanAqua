using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    // UI 요소
    public TextMeshProUGUI TimeTxt; // 화면의 "시간" 글자
    public TextMeshProUGUI TimerTxt; // 화면의 남은 시간 표시 (게임 타이머)
    public TextMeshProUGUI BackgroundTimerTxt; // 배경 전환 타이머 표시 텍스트
    public GameObject ResultPanel; // 결과창
    public GameObject SettingPanel; // 설정창
    public GameObject StopBtn; // 게임 종료 버튼

    // 배경 오브젝트
    public GameObject Back1, Back2, Back3, Back4; // 네 개의 배경
    private GameObject[] Backgrounds; // 배경 배열

    // 타이머 변수
    private float gameRemainTime = 0f; // 게임 타이머 남은 시간
    private bool isGameTimerRunning = false; // 게임 타이머 실행 여부

    private float backgroundChangeInterval = 180f; // 3분(180초) 간격으로 배경 변경
    private int currentBackgroundIndex = 0; // 현재 배경 인덱스
    private float backgroundRemainTime = 180f; // 배경 전환까지 남은 시간 (초)
    private bool isBackgroundTimerRunning = true; // 배경 타이머 실행 여부 (기본 활성화)

    void Start()
    {
        // 배경 배열 초기화
        Backgrounds = new GameObject[] { Back1, Back2, Back3, Back4 };

        // 초기 상태 설정
        TimeTxt.gameObject.SetActive(false);
        TimerTxt.gameObject.SetActive(false);
        BackgroundTimerTxt.gameObject.SetActive(true); // 기본적으로 활성화
        SettingPanel.gameObject.SetActive(false);
        ResultPanel.gameObject.SetActive(false);
        StopBtn.gameObject.SetActive(false); // 초기 상태에서 StopBtn 숨김

        // 첫 배경 설정 및 배경 타이머 초기화
        ChangeBackground();
        UpdateBackgroundTimerTxt();
    }

    void Update()
    {
        // 배경 전환 타이머 업데이트
        if (isBackgroundTimerRunning)
        {
            backgroundRemainTime -= Time.deltaTime;
            UpdateBackgroundTimerTxt();

            if (backgroundRemainTime <= 0f)
            {
                ChangeBackground();
                backgroundRemainTime = backgroundChangeInterval; // 타이머 초기화
                UpdateBackgroundTimerTxt();
            }
        }

        // 게임 타이머 업데이트
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

    // 3분 타이머 설정
    public void Set3MinTimer()
    {
        StartGameTimer(3);
        Start3MinGameLogic();
    }

    // 6분 타이머 설정
    public void Set6MinTimer()
    {
        StartGameTimer(6);
        Start6MinGameLogic();
    }

    // 9분 타이머 설정
    public void Set9MinTimer()
    {
        StartGameTimer(9);
        Start9MinGameLogic();
    }

    // 10분 타이머 설정
    public void Set10MinTimer()
    {
        StartGameTimer(10);
        Start10MinGameLogic();
    }

    // 게임 타이머 시작
    private void StartGameTimer(int minutes)
    {
        // 기존 활성화된 배경 비활성화
        DeactivateAllBackgrounds();

        // 게임 타이머 초기화
        gameRemainTime = minutes * 60f;
        isGameTimerRunning = true;

        // UI 요소 활성화
        TimeTxt.gameObject.SetActive(true);
        TimerTxt.gameObject.SetActive(true);
        BackgroundTimerTxt.gameObject.SetActive(false); // 배경 타이머 비활성화
        StopBtn.gameObject.SetActive(true);

        // 설정 패널 닫기
        SettingPanel.gameObject.SetActive(false);

        // 결과창 숨기기
        ResultPanel.gameObject.SetActive(false);
        UpdateGameTimerTxt();

        // 배경 타이머 중지
        isBackgroundTimerRunning = false;
    }

    // 게임 타이머 텍스트 업데이트
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

    // 배경 전환 타이머 텍스트 업데이트
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

    // 배경을 변경하는 함수
    void ChangeBackground()
    {
        // 모든 배경을 비활성화
        DeactivateAllBackgrounds();

        // 현재 인덱스에 따라 배경 활성화
        if (currentBackgroundIndex >= 0 && currentBackgroundIndex < Backgrounds.Length)
        {
            Backgrounds[currentBackgroundIndex].SetActive(true);
        }

        // 다음 배경으로 인덱스 변경 (순환)
        currentBackgroundIndex = (currentBackgroundIndex + 1) % Backgrounds.Length;
    }

    // 모든 배경 비활성화 함수
    void DeactivateAllBackgrounds()
    {
        foreach (GameObject bg in Backgrounds)
        {
            bg.SetActive(false);
        }
    }

    // 시간이 끝났을 때 게임 종료 및 결과창 표시
    void EndGame()
    {
        isGameTimerRunning = false;
        gameRemainTime = 0f;
        TimerTxt.text = "0:00"; // 0:00에서 멈추기
        StopBtn.gameObject.SetActive(false); // 게임 종료 시 StopBtn 숨김

        // 결과창 표시
        ResultPanel.gameObject.SetActive(true);

        // 배경 타이머 중지
        isBackgroundTimerRunning = false;
        BackgroundTimerTxt.gameObject.SetActive(false);
    }

    // 결과창을 닫고 초기 상태로 돌아가기
    public void CloseResultPanel()
    {
        ResultPanel.gameObject.SetActive(false);
        TimerTxt.text = ""; // 타이머 텍스트 초기화
        TimeTxt.gameObject.SetActive(false);
        TimerTxt.gameObject.SetActive(false);
        isGameTimerRunning = false;
        StopBtn.gameObject.SetActive(false); // 결과창 닫을 때 StopBtn 숨김

        // 배경 타이머 재개 및 활성화
        isBackgroundTimerRunning = true;
        BackgroundTimerTxt.gameObject.SetActive(true);

        // 배경 즉시 활성화
        ChangeBackground();
    }

    // 게임 중지 버튼 클릭 시 호출되는 함수
    public void StopGame()
    {
        EndGame(); // 게임 종료 로직 호출
    }

    void Start3MinGameLogic()
    {
        Debug.Log("3분짜리 게임 시작");
    }

    void Start6MinGameLogic()
    {
        Debug.Log("6분짜리 게임 시작");
    }

    void Start9MinGameLogic()
    {
        Debug.Log("9분짜리 게임 시작");
    }

    void Start10MinGameLogic()
    {
        Debug.Log("10분짜리 게임 시작");
    }
}