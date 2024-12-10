using UnityEngine;
using UnityEngine.SceneManagement;

public class Choice : MonoBehaviour
{
    public void SelectHg()
    {
        CharacterStats stats = new CharacterStats(100, 15, 12, 0.8f); // HG 캐릭터 기본 스탯
        InitializeCharacter(stats);
    }

    public void SelectAr()
    {
        CharacterStats stats = new CharacterStats(120, 20, 10, 0.9f); // AR 캐릭터 기본 스탯
        InitializeCharacter(stats);
    }

    public void SelectRf()
    {
        CharacterStats stats = new CharacterStats(100, 35, 12, 0.8f); // RF 캐릭터 기본 스탯
        InitializeCharacter(stats);
    }

    public void SelectSg()
    {
        CharacterStats stats = new CharacterStats(120, 30, 12, 0.8f); // SG 캐릭터 기본 스탯
        InitializeCharacter(stats);
    }

    private void InitializeCharacter(CharacterStats stats)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.InitializeStats(stats); // GameManager에서 스탯 초기화
            LoadGameScene();
        }
        else
        {
            Debug.LogError("GameManager.Instance is null! Ensure GameManager is present in the scene.");
        }
    }

    private void LoadGameScene()
    {
        Debug.Log("Loading GameScene...");
        SceneManager.LoadScene("GameScene"); // "GameScene"은 게임 씬 이름
    }
}