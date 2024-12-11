using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentHealth;
    public int currentStr;
    public int currentSpeed;
    public float currentStamina;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager initialized.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 선택된 캐릭터 스탯 초기화 메서드
    public void InitializeStats(CharacterStats selectedStats)
    {
        currentHealth = selectedStats.health;
        currentStr = selectedStats.strength;
        currentSpeed = selectedStats.speed;
        currentStamina = selectedStats.stamina;

        Debug.Log($"Stats Initialized: Health={currentHealth}, Strength={currentStr}, Speed={currentSpeed}, Stamina={currentStamina}");
    }
}