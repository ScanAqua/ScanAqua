using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI TimeTxt; // "시간" 텍스트
    public TextMeshProUGUI TimerTxt; // 남은 시간을 표시할 TextMeshPro
    public GameObject ResultPanel; // 결과창
    public GameObject SettingPanel; // 설정 패널
    private float RemainTime = 0; // 남은 시간
    private bool inTimerRunning = false; // 타이머 실행 여부

    void Start()
    {
        // 초기 상태: TimeTxt, TimerTxt, SettingPanel, ResultPanel 모두 숨겨진 상태
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

    // 버튼을 누르면 해당하는 시간이 설정되고 타이머 시작
    public void SetTimer(int minutes)
    {
        RemainTime = minutes * 60; // 분을 초로 변환
        inTimerRunning = true;

        // TimeTxt와 TimerTxt를 화면에 표시
        TimeTxt.gameObject.SetActive(true);
        TimerTxt.gameObject.SetActive(true);

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

        // 00:00에서 멈추기
        if (RemainTime <= 0)
        {
            TimerTxt.text = "00:00";
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
        TimerTxt.text = "0:00"; // 00:00에서 멈추기
        ResultPanel.SetActive(true); // 결과창 표시
    }

    // 결과창을 닫고 초기 상태로 돌아가기
    public void CloseResultPanel()
    {
        // 모든 UI 요소를 초기 상태로 숨기기
        ResultPanel.SetActive(false);
        TimerTxt.text = ""; // 타이머 텍스트 초기화
        TimeTxt.gameObject.SetActive(false); // TimeTxt 숨기기
        TimerTxt.gameObject.SetActive(false); // TimerTxt 숨기기
        inTimerRunning = false;

        // SettingPanel 숨긴 상태로 초기화
        SettingPanel.SetActive(false);
    }
}