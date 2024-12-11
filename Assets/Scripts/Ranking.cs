using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    [System.Serializable]
    public class RankingEntry
    {
        public string playerName;
        public float elapsedTime;

        public RankingEntry(string name, float time)
        {
            playerName = name;
            elapsedTime = time;
        }
    }

    [System.Serializable]
    public class RankingData
    {
        public List<RankingEntry> rankingList = new List<RankingEntry>();
    }

}
