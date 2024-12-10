using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    public int health = 120;    // 기본 체력
    public int strength = 15;  // 기본 힘
    public int speed = 20;     // 기본 속도
    public float stamina = 0.8f; // 기본 스태미나

    // 생성자 - 필요에 따라 커스텀 스탯 초기화 가능
    public CharacterStats(int health, int strength, int speed, float stamina)
    {
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.stamina = stamina;
    }

    // 기본 생성자 - 정해진 초기값 사용
    public CharacterStats()
    {
    }
}