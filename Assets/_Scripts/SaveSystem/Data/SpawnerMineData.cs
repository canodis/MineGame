
using UnityEngine;

[System.Serializable]

public class SpawnerMineData
{
    public string spawner;
    public string name;
    public Vector3 position;
    public int health;

    public SpawnerMineData(string spawnerName,string name, Vector3 position, int health)
    {
        this.spawner = spawnerName;
        this.name = name;
        this.position = position;
        this.health = health;
    }
}