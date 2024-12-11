public struct CharacterStats
{
    public int health;
    public int strength;
    public int speed;
    public float stamina;

    public CharacterStats(int health, int strength, int speed, float stamina)
    {
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.stamina = stamina;
    }
}
public static class CharacterData
{
    public static int Health;
    public static int Strength;
    public static int Speed;
    public static float Stamina;

    public static void SetStats(CharacterStats stats)
    {
        Health = stats.health;
        Strength = stats.strength;
        Speed = stats.speed;
        Stamina = stats.stamina;
    }
}
