using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    public float currentProgress = 0f;   // 현재 이동 거리
    public float targetProgress = 3600f; // 목표 이동 거리
    public float currentSpeedProgress = 0f; // 누적 속도 값

    [SerializeField] private TMP_Text progressText; // 이동 거리 텍스트
    [SerializeField] private TMP_Text elapsedTimeText; // 경과 시간 텍스트
    [SerializeField] private TMP_Text speedProgressText; // 속도 누적 텍스트
    [SerializeField] private TMP_Text skillOutputText; // 스킬 출력 텍스트

    [SerializeField] private GameObject gameEndPanel; // 게임 종료 패널
    [SerializeField] private TMP_Text panelElapsedTimeText; // 패널 경과 시간 텍스트
    [SerializeField] private TMP_Text panelProgressText; // 패널 진행 상태 텍스트
    [SerializeField] private TMP_Text panelStatsText; // 패널 스텟 텍스트
    [SerializeField] private TMP_Text rankingText; // 랭킹 표시 텍스트

    [SerializeField] private Transform spawnPoint; // 프리팹 생성 위치

    private float elapsedTime = 0f;      // 경과 시간
    private bool isGameEnded = false;    // 게임 종료 여부

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
        if (gameEndPanel != null)
        {
            gameEndPanel.SetActive(false); // 게임 시작 시 패널 비활성화
        }

        // 선택된 프리팹 생성
        SpawnSelectedPrefab();

        UpdateUI(); // 초기 UI 업데이트
    }

    private void Update()
    {
        if (isGameEnded) return; // 게임 종료 시 업데이트 중단

        UpdateProgress(); // 진행 상태 업데이트
        UpdateElapsedTime(); // 경과 시간 업데이트
        UpdateSpeedProgress(); // 속도 누적 업데이트
        CheckGameEnd(); // 게임 종료 조건 확인
    }

    private void SpawnSelectedPrefab()
    {
        if (Choice.selectedPrefab != null && spawnPoint != null)
        {
            Instantiate(Choice.selectedPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log($"Spawned selected prefab: {Choice.selectedPrefab.name} at {spawnPoint.position}");
        }
        else
        {
            Debug.LogWarning("Selected prefab or spawn point is not set!");
        }
    }

    private void UpdateProgress()
    {
        float increment = CharacterManager.Instance?.currentSpeed * Time.deltaTime ?? 0f;
        currentProgress += increment;

        UpdateUI();
    }

    private void UpdateSpeedProgress()
    {
        currentSpeedProgress += CharacterManager.Instance?.currentSpeed * Time.deltaTime ?? 0f;

        if (speedProgressText != null)
        {
            speedProgressText.text = $"{currentSpeedProgress:0000} / {targetProgress:0000}";
        }
    }

    private void UpdateElapsedTime()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTimeText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            float seconds = elapsedTime % 60f;
            elapsedTimeText.text = $"Time: {minutes:00}:{seconds:00.000}";
        }
    }

    private void CheckGameEnd()
    {
        if (currentProgress >= targetProgress)
        {
            isGameEnded = true; // 게임 종료 상태로 설정
            if (gameEndPanel != null)
            {
                UpdateGameEndPanel(); // 패널 텍스트 업데이트
                gameEndPanel.SetActive(true); // 패널 활성화
            }

            // 게임 종료 후 랭킹 업데이트
            SaveRanking(elapsedTime);
            DisplayRanking();
        }
    }

    private void UpdateGameEndPanel()
    {
        if (panelElapsedTimeText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            float seconds = elapsedTime % 60f;
            panelElapsedTimeText.text = $"Time: {minutes:00}:{seconds:00.000}";
        }

        if (panelProgressText != null)
        {
            panelProgressText.text = $"Progress: {currentProgress:0000} / {targetProgress:0000}";
        }

        if (panelStatsText != null && CharacterManager.Instance != null)
        {
            // 스킬 값 계산 (STR * Stamina * 0.7)
            float skillValue = CharacterManager.Instance.currentStr *
                               CharacterManager.Instance.currentStamina *
                               0.7f;

            panelStatsText.text = $"Stats:\n" +
                                  $"Speed: {CharacterManager.Instance.currentSpeed}\n" +
                                  $"Skill Value: {skillValue:F2}";
        }

    }

    private void UpdateUI()
    {
        if (progressText != null)
        {
            progressText.text = $"Progress: {currentProgress:0000} / {targetProgress:0000}";
        }

        if (skillOutputText != null && CharacterManager.Instance != null)
        {
            skillOutputText.text = $"HP: {CharacterManager.Instance.currentHealth}";
        }

        // 강제 Canvas 업데이트
        Canvas.ForceUpdateCanvases();
    }

    private void SaveRanking(float time)
    {
        List<float> rankings = LoadRankingData();
        rankings.Add(time);
        rankings = rankings.OrderBy(t => t).ToList(); // 시간 기준 오름차순 정렬

        for (int i = 0; i < rankings.Count && i < 10; i++)
        {
            PlayerPrefs.SetFloat($"Ranking_{i}", rankings[i]);
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

    public void UseSkill()
    {
        if (CharacterManager.Instance == null)
        {
            Debug.LogError("CharacterManager instance is null. Cannot calculate skill.");
            return;
        }

        // HP가 0이면 스킬 사용 불가
        if (CharacterManager.Instance.currentHealth <= 0)
        {
            Debug.LogWarning("HP is 0. Cannot use skill.");
            return;
        }

        // 새로운 스킬 공식: STR * Stamina * 0.7
        float skillValue = CharacterManager.Instance.currentStr *
                           CharacterManager.Instance.currentStamina *
                           0.7f;

        // HP 감소
        CharacterManager.Instance.currentHealth--;

        // 스킬로 계산된 값 추가: currentProgress 업데이트
        currentProgress += skillValue;

        // 진행 상태 업데이트
        Debug.Log($"Skill used! Value: {skillValue:F2}. Remaining HP: {CharacterManager.Instance.currentHealth}. Progress: {currentProgress:F2}/{targetProgress}");

        // 즉시 UI 반영
        UpdateUI();
        Canvas.ForceUpdateCanvases(); // Canvas 강제 업데이트
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        Debug.Log("Game is quitting... (Editor)");
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 실행 종료
#else
        Debug.Log("Game is quitting..."); // 실행 파일에서 게임 종료
        Application.Quit();
#endif
    }
}
