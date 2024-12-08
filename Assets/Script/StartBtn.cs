using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
    // 애니메이터 변수
    public Animator saveLoadAnimator;
    public Animator rankingAnimator;

    // 스타트 버튼 클릭
    public void OnStratBtn()
    {
        // 초이스 씬으로 이동
        SceneManager.LoadScene("CoiceScene");
    }

    // 세이브 로드 버튼 클릭
    public void SaveLoadBtn()
    {
        if (saveLoadAnimator != null)
        {
            // 애니메이션 트리거 설정
            saveLoadAnimator.SetBool("isHidden", false);
        }
        else
        {
            Debug.LogWarning("SaveLoadAnimator가 연결되지 않았습니다!");
        }
    }

    // 랭킹 버튼 클릭
    public void RankingBtn()
    {
        if (rankingAnimator != null)
        {
            // 애니메이션 트리거 발동
            rankingAnimator.SetTrigger("ShowRanking");
        }
        else
        {
            Debug.LogWarning("RankingAnimator가 연결되지 않았습니다!");
        }
    }
}


