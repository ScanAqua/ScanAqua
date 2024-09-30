using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI TimeTxt; // 화면의 "시간" 글자
    public TextMeshProUGUI TimerTxt; // 화면의 남은 시간 표시
    public GameObject ResultPanel; // 결과창
    public GameObject SettingPanel; // 설정창
    public GameObject StopBtn; // 게임 종료 버튼 추가
    private float RemainTime = 0; // 남은 시간
    private bool inTimerRunning = false; // 타이머 실행 여부

    void Start()
    {
        TimeTxt.gameObject.SetActive(false);
        TimerTxt.gameObject.SetActive(false);
        SettingPanel.SetActive(false);
        ResultPanel.SetActive(false);
        StopBtn.SetActive(false); // 초기 상태에서 StopBtn 숨김
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

    // 3분 타이머 설정
    public void Set3MinTimer()
    {
        SetTimer(3);
        Start3MinGameLogic();
    }

    // 6분 타이머 설정
    public void Set6MinTimer()
    {
        SetTimer(6);
        Start6MinGameLogic();
    }

    // 9분 타이머 설정
    public void Set9MinTimer()
    {
        SetTimer(9);
        Start9MinGameLogic();
    }

    // 10분 타이머 설정
    public void Set10MinTimer()
    {
        SetTimer(10);
        Start10MinGameLogic();
    }

    // 타이머 공통설정
    private void SetTimer(int minutes)
    {
        RemainTime = minutes * 60; // 분을 초로 변환
        inTimerRunning = true;

        // TimeTxt와 TimerTxt를 화면에 표시
        TimeTxt.gameObject.SetActive(true);
        TimerTxt.gameObject.SetActive(true);
        StopBtn.SetActive(true); // 게임 시작 시 StopBtn 표시

        // SettingPanel 닫기
        SettingPanel.SetActive(false);

        ResultPanel.SetActive(false); // 결과창 숨기기
        UpdateTimerTxt();
    }

    // 타이머 텍스트 업데이트
    void UpdateTimerTxt()
    {
        int minutes = Mathf.FloorToInt(RemainTime / 60);
        int seconds = Mathf.FloorToInt(RemainTime % 60);

        // 게임 끝나면 0:00에서 멈추기
        if (RemainTime <= 0)
        {
            TimerTxt.text = "0:00";
        }
        else
        {
            TimerTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }

    // 시간이 끝났을 때 게임 종료 및 결과창 표시
    void EndGame()
    {
        inTimerRunning = false;
        RemainTime = 0;
        TimerTxt.text = "0:00"; // 0:00에서 멈추기
        StopBtn.SetActive(false); // 게임 종료 시 StopBtn 숨김
        ResultPanel.SetActive(true); // 결과창 표시
    }

    // 결과창을 닫고 초기 상태로 돌아가기
    public void CloseResultPanel()
    {
        ResultPanel.SetActive(false);
        TimerTxt.text = ""; // 타이머 텍스트 초기화
        TimeTxt.gameObject.SetActive(false);
        TimerTxt.gameObject.SetActive(false);
        inTimerRunning = false;
        StopBtn.SetActive(false); // 결과창 닫을 때 StopBtn 숨김

        SettingPanel.SetActive(false);
    }

    // 게임 중지 버튼 클릭 시 호출되는 함수
    public void StopGame()
    {
        EndGame(); // 게임 종료 로직 호출
    }

    void Start3MinGameLogic()
    {
        Debug.Log("3분짜리 게임시작");
    }

    void Start6MinGameLogic()
    {
        Debug.Log("6분짜리 게임시작");
    }

    void Start9MinGameLogic()
    {
        Debug.Log("9분짜리 게임시작");
    }

    void Start10MinGameLogic()
    {
        Debug.Log("10분짜리 게임시작");
    }
}