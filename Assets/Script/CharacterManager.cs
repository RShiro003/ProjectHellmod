using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public int currentHealth;
    public int currentStr;
    public int currentSpeed;
    public float currentStamina;

    // 초기화
    public void InitializeStats(CharacterStats stats)
    {
        currentHealth = stats.health;
        currentStr = stats.strength;
        currentSpeed = stats.speed;
        currentStamina = stats.stamina;

        Debug.Log($"Character Stats Initialized: Health={currentHealth}, Strength={currentStr}, Speed={currentSpeed}, Stamina={currentStamina}");
    }

    // Unity 버튼과 연결하기 위한 중간 메서드
    public void IncreaseHealth()
    {
        IncreaseStat("Health", 10);
        GameProgressManager.Instance.IncreaseStageCount(); // 스테이지 카운트도 증가
    }

    public void IncreaseStr()
    {
        IncreaseStat("Strength", 10);
        GameProgressManager.Instance.IncreaseStageCount(); // 스테이지 카운트도 증가
    }

    public void IncreaseSpeed()
    {
        IncreaseStat("Speed", 10);
        GameProgressManager.Instance.IncreaseStageCount(); // 스테이지 카운트도 증가
    }

    public void IncreaseStamina()
    {
        IncreaseStat("Stamina", 0.1f);
        GameProgressManager.Instance.IncreaseStageCount(); // 스테이지 카운트도 증가
    }

    // 공통 스탯 증가 로직
    private void IncreaseStat(string statName, float amount)
    {
        switch (statName)
        {
            case "Health":
                currentHealth += (int)amount;
                break;
            case "Strength":
                currentStr += (int)amount;
                break;
            case "Speed":
                currentSpeed += (int)amount;
                break;
            case "Stamina":
                currentStamina += amount;
                break;
            default:
                Debug.LogWarning($"Unknown stat: {statName}");
                return;
        }

        Debug.Log($"{statName} increased by {amount}. Current {statName}: {GetStat(statName)}");
    }

    // 특정 스탯 값을 가져오기
    public float GetStat(string statName)
    {
        return statName switch
        {
            "Health" => currentHealth,
            "Strength" => currentStr,
            "Speed" => currentSpeed,
            "Stamina" => currentStamina,
            _ => 0
        };
    }
}
