using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    public int currentHealth;
    public int currentStr;
    public int currentSpeed;
    public float currentStamina;

    [SerializeField] private Button healthButton;
    [SerializeField] private Button strButton;
    [SerializeField] private Button speedButton;
    [SerializeField] private Button staminaButton;

    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private GameObject strengthPrefab;
    [SerializeField] private GameObject speedPrefab;
    [SerializeField] private GameObject staminaPrefab;

    [SerializeField] private Transform spawnPoint;

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
        // CharacterData에서 데이터를 로드하여 초기화
        InitializeStats(new CharacterStats(CharacterData.Health, 
            CharacterData.Strength, 
            CharacterData.Speed, 
            CharacterData.Stamina));

        // 선택된 프리팹 생성
        SpawnSelectedPrefab();

        // 버튼 이벤트 설정
        SetupButtonEvents();
    }

    public void InitializeStats(CharacterStats stats)
    {
        currentHealth = stats.health;
        currentStr = stats.strength;
        currentSpeed = stats.speed;
        currentStamina = stats.stamina;

        Debug.Log($"Character Stats Initialized: Health={currentHealth}, Strength={currentStr}, Speed={currentSpeed}, Stamina={currentStamina}");
    }

    private void SetupButtonEvents()
    {
        if (healthButton != null)
        {
            healthButton.onClick.AddListener(() => {
                IncreaseHealth();
                SpawnPrefab(healthPrefab);
                IncreaseStageCount();
            });
        }

        if (strButton != null)
        {
            strButton.onClick.AddListener(() => {
                IncreaseStrength();
                SpawnPrefab(strengthPrefab);
                IncreaseStageCount();
            });
        }

        if (speedButton != null)
        {
            speedButton.onClick.AddListener(() => {
                IncreaseSpeed();
                SpawnPrefab(speedPrefab);
                IncreaseStageCount();
            });
        }

        if (staminaButton != null)
        {
            staminaButton.onClick.AddListener(() => {
                IncreaseStamina();
                SpawnPrefab(staminaPrefab);
                IncreaseStageCount();
            });
        }
    }

    public void IncreaseHealth()
    {
        currentHealth += 1;
        Debug.Log($"Health increased to {currentHealth}");
    }

    public void IncreaseStrength()
    {
        currentStr += 10;
        Debug.Log($"Strength increased to {currentStr}");
    }

    public void IncreaseSpeed()
    {
        currentSpeed += 10;
        Debug.Log($"Speed increased to {currentSpeed}");
    }

    public void IncreaseStamina()
    {
        currentStamina += 0.5f;
        Debug.Log($"Stamina increased to {currentStamina:F1}");
    }

    private void SpawnPrefab(GameObject prefab)
    {
        if (prefab != null && spawnPoint != null)
        {
            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log($"Spawned prefab: {prefab.name} at {spawnPoint.position}");
        }
        else
        {
            Debug.LogWarning("Prefab or spawn point is not set!");
        }
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

    private void IncreaseStageCount()
    {
        if (GameProgressManager.Instance != null)
        {
            GameProgressManager.Instance.IncreaseStageCount();
        }
        else
        {
            Debug.LogError("GameProgressManager.Instance is null! Ensure GameProgressManager is present in the scene.");
        }
    }
}