using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtncode : MonoBehaviour
{
    // 애니메이터 변수
    public Animator rankingAnimator;
    
    // 스타트 버튼 클릭
    public void OnStratBtn()
    {
        // 초이스 씬으로 이동
        SceneManager.LoadScene("CoiceScene");
    }

    public void OnRankingBtn()
    {
        
    }
}


