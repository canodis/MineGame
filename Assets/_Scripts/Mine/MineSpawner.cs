using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    public GameObject[] mines;
    public LayerMask ignoreCollision;
    public Vector3 areaSize;
    public int mineCount = 10;

    private int totalRarity = 0;
    private int attempts = 5;

    void Start()
    {
        sortMines();
        findRarity();
        InvokeRepeating("createMines", 0, 40f);
    }

    private void findRarity()
    {
        foreach (GameObject mine in mines)
        {
            Resource resource = mine.GetComponent<Resource>();
            totalRarity += resource.itemData.Rarity;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            createMines();
        }
    }

    void createMines()
    {
        for (int i = 0; i < mineCount; i++)
        {
            int random = Random.Range(0, totalRarity);
            Vector3 position = getRandomPosition();
            if (!IsColliding(position))
            {
                int index = 0;

                while (random > 0)
                {
                    random -= mines[index].GetComponent<Resource>().itemData.Rarity;
                    if (random <= 0)
                        break;
                    index++;
                }
                GameObject mine = Instantiate(mines[index], position, Quaternion.identity);
                mine.transform.parent = transform;
            }
            else
            {
                for (int j = 0; j < attempts; j++)
                {
                    position = getRandomPosition();
                    if (!IsColliding(position))
                    {
                        int index = 0;

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
            }
        }
    }

    Vector3 getRandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(-areaSize.x, areaSize.x), transform.position.y, Random.Range(-areaSize.z, areaSize.z));
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
}
