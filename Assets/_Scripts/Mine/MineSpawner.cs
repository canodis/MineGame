using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    public GameObject[] mines;
    public LayerMask ignoreCollision;
    public Vector3 areaSize;
    public int countPerSpawn;
    public float spawnTimer;
    public int maxMineCount;
    public int totalMineCount = 0;

    private int totalRarity = 0;
    private int attempts = 5;

    void Start()
    {
        sortMines();
        findRarity();
        InvokeRepeating("MineSpawnerLoop", 0, spawnTimer);
    }

    void MineSpawnerLoop()
    {
        if (totalMineCount < maxMineCount)
            createMines();
    }

    private void findRarity()
    {
        foreach (GameObject mine in mines)
        {
            Resource resource = mine.GetComponent<Resource>();
            totalRarity += resource.itemData.Rarity;
        }
    }

    void createMines()
    {
        for (int i = 0; i < countPerSpawn; i++)
        {
            InstantiateMine();
        }
    }

    private void InstantiateMine()
    {
        int random = Random.Range(0, totalRarity);
        int index;
        Vector3 position;
        for (int i = 0; i < attempts; i++)
        {
            position = getRandomPosition();
            index = 0;
            if (IsColliding(position))
                continue;
            else
                totalMineCount++;
            while (random > 0)
            {
                random -= mines[index].GetComponent<Resource>().itemData.Rarity;
                if (random <= 0)
                    break;
                index++;
            }
            GameObject mine = Instantiate(mines[index], position, Quaternion.identity);
            mine.transform.parent = transform;
            break;
        }
    }

    Vector3 getRandomPosition()
    {
        Vector3 position = new Vector3(
        Random.Range(transform.position.x - areaSize.x / 2f, transform.position.x + areaSize.x / 2f),
        transform.position.y,
        Random.Range(transform.position.z - areaSize.z / 2f, transform.position.z + areaSize.z / 2f)
    );
        Ray ray = new Ray(position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            position.y = hit.point.y;
        }
        return position;
    }

    bool IsColliding(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 1f, ignoreCollision);

        return colliders.Length > 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, areaSize * 2f);
    }

    private void sortMines()
    {
        for (int i = 0; i < mines.Length; i++)
        {
            for (int j = i + 1; j < mines.Length; j++)
            {
                Resource resource1 = mines[i].GetComponent<Resource>();
                Resource resource2 = mines[j].GetComponent<Resource>();
                if (resource1.itemData.Rarity < resource2.itemData.Rarity)
                {
                    GameObject temp = mines[i];
                    mines[i] = mines[j];
                    mines[j] = temp;
                }
            }
        }
    }

    public void decreaseMineCount()
    {
        totalMineCount--;
    }
}
