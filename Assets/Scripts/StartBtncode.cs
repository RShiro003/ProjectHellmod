using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtncode : MonoBehaviour
{
    // 애니메이터 변수
    public Animator rankingAnimator;

    // 랭킹 텍스트를 표시할 UI 요소
    public TMPro.TMP_Text rankingText;

    // 랭킹 패널
    public GameObject rankingPanel;

    // 스타트 버튼 클릭
    public void OnStratBtn()
    {
        // 초이스 씬으로 이동
        SceneManager.LoadScene("CoiceScene");
    }

    public void OnRankingBtn()
    {
        // 랭킹 패널 활성화
        if (rankingPanel != null)
        {
            rankingPanel.SetActive(true);
        }

        // 랭킹 데이터를 불러와 표시
        DisplayRanking();

        // 애니메이션 재생 (선택 사항)
        if (rankingAnimator != null)
        {
            rankingAnimator.SetTrigger("ShowRanking");
        }
    }

    private void DisplayRanking()
    {
        List<float> rankings = LoadRankingData();
        rankingText.text = "Ranking:\n";

        for (int i = 0; i < rankings.Count; i++)
        {
            int minutes = Mathf.FloorToInt(rankings[i] / 60f);
            float seconds = rankings[i] % 60f;
            rankingText.text += $"{i + 1}. {minutes:00}:{seconds:00.000}\n";
        }
    }

    private List<float> LoadRankingData()
    {
        List<float> rankings = new List<float>();

        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey($"Ranking_{i}"))
            {
                rankings.Add(PlayerPrefs.GetFloat($"Ranking_{i}"));
            }
        }

        return rankings;
    }
}