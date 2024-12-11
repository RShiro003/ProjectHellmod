using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance { get; private set; }

    public int stageCount = 1; // 초기 스테이지를 1로 설정
    [SerializeField] private TMP_Text stageText; // TextMeshPro UI 텍스트
    [SerializeField] private int maxStageCount = 30; // 최대 스테이지

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        stageCount = 1; // 명시적으로 초기값 설정
        UpdateStageUI(); // UI 초기화
    }

    public void IncreaseStageCount()
    {
        stageCount++;
        UpdateStageUI();

        // 최대 스테이지를 초과하면 씬 전환
        if (stageCount >= maxStageCount)
        {
            Debug.Log("Max stage count reached. Loading next scene...");
            SceneManager.LoadScene("MainScene");
        }
    }

    private void UpdateStageUI()
    {
        if (stageText != null)
        {
            // 스테이지 숫자를 항상 두 자리 형식으로 표시
            stageText.text = $"{stageCount:00} / {maxStageCount:00}";
        }
    }
}