using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health = 100;
    public int strength = 10;
    public int speed = 10;
    public float stamina = 0.5f;
    // 필요한 스탯들을 추가로 정의할 수 있습니다.
}

[System.Serializable]
public class ScoreData
{
    public string playerName;
    public int time; // 시간을 초 단위로 저장
}

[System.Serializable]
public class ScoreDataList
{
    public List<ScoreData> scores = new List<ScoreData>();
}

[System.Serializable]
public class GameSaveData
{
    public PlayerData playerData = new PlayerData();
    public ScoreDataList scoreDataList = new ScoreDataList();
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerData playerData = new PlayerData();
    public List<ScoreData> scoreList = new List<ScoreData>();

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

    // 스탯 증가 메서드
    public void IncreaseStat(string statName, int amount)
    {
        switch (statName)
        {
            case "Health":
                playerData.health += amount;
                break;
            case "Strength":
                playerData.strength += amount;
                break;
            default:
                Debug.LogWarning("Unknown stat: " + statName);
                break;
        }
        
        Debug.Log($"{statName} increased by {amount}. Current {statName}: {GetStat(statName)}");
        SaveGame("AutoSave"); // 자동 저장 예시
    }

    // 특정 스탯 값을 반환하는 메서드
    public int GetStat(string statName)
    {
        return statName switch
        {
            "Health" => playerData.health,
            "Strength" => playerData.strength,
            _ => 0
        };
    }

    // 게임 데이터 저장 메서드
    public void SaveGame(string saveName)
    {
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = $"{saveName}_{timestamp}.json";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        GameSaveData saveData = new GameSaveData();
        saveData.playerData = playerData;
        saveData.scoreDataList.scores = scoreList;

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(filePath, json);

        Debug.Log($"Game Saved: {fileName}");
    }

    // 게임 데이터 로드 메서드
    public void LoadGame(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            GameSaveData saveData = JsonUtility.FromJson<GameSaveData>(json);

            playerData = saveData.playerData;
            scoreList = saveData.scoreDataList.scores;

            Debug.Log($"Game Loaded: {fileName}");
        }
        else
        {
            Debug.LogWarning("Save file not found: " + fileName);
        }
    }

    // 저장된 파일 목록을 가져오는 메서드
    public string[] GetSaveFiles()
    {
        return Directory.GetFiles(Application.persistentDataPath, "*.json");
    }

    // 점수 추가 메서드 (시간 기준 오름차순 정렬)
    public void AddScore(string playerName, float time)
    {
        ScoreData newScore = new ScoreData { playerName = playerName, time = (int)time };
        scoreList.Add(newScore);

        // 시간을 기준으로 오름차순 정렬 (가장 짧은 시간이 상위에 위치)
        scoreList.Sort((x, y) => x.time.CompareTo(y.time));

        SaveGame("AutoSave"); // 자동으로 저장
        Debug.Log($"Score added for {playerName} with time: {time} seconds");
    }

    // 버튼을 통한 수동 세이브 메서드
    public void SaveGameButton()
    {
        SaveGame("ManualSave");
        Debug.Log("Game saved via button");
    }

    // 버튼을 통한 수동 로드 메서드
    public void LoadGameButton(string fileName)
    {
        LoadGame(fileName);
        Debug.Log("Game loaded via button");
    }
}
