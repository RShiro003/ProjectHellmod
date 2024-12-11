using UnityEngine;
using UnityEngine.SceneManagement;

public class Choice : MonoBehaviour
{
    public GameObject hgPrefab; // HG 캐릭터 프리팹
    public GameObject arPrefab; // AR 캐릭터 프리팹
    public GameObject rfPrefab; // RF 캐릭터 프리팹
    public GameObject sgPrefab; // SG 캐릭터 프리팹

    public static GameObject selectedPrefab; // 선택한 프리팹을 저장하는 변수 (씬 간 데이터 공유)

    public void SelectHg()
    {
        CharacterStats stats = new CharacterStats(6, 1, 30, 0.8f); // HG 캐릭터 기본 스탯
        selectedPrefab = hgPrefab; // 선택한 프리팹 저장
        SetCharacterAndLoadScene(stats);
    }

    public void SelectAr()
    {
        CharacterStats stats = new CharacterStats(20, 5, 20, 0.9f); // AR 캐릭터 기본 스탯
        selectedPrefab = arPrefab; // 선택한 프리팹 저장
        Debug.Log($"HG Prefab selected: {selectedPrefab.name}");

        SetCharacterAndLoadScene(stats);
    }

    public void SelectRf()
    {
        CharacterStats stats = new CharacterStats(4, 35, 15, 0.8f); // RF 캐릭터 기본 스탯
        selectedPrefab = rfPrefab; // 선택한 프리팹 저장
        SetCharacterAndLoadScene(stats);
    }

    public void SelectSg()
    {
        CharacterStats stats = new CharacterStats(2, 100, 20, 0.8f); // SG 캐릭터 기본 스탯
        selectedPrefab = sgPrefab; // 선택한 프리팹 저장
        SetCharacterAndLoadScene(stats);
    }

    private void SetCharacterAndLoadScene(CharacterStats stats)
    {
        // 데이터를 CharacterData에 저장
        CharacterData.SetStats(stats);

        // 게임 씬 로드
        SceneManager.LoadScene("GameScene");
    }
}