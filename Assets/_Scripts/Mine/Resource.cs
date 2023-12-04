using UnityEngine;

public class Resource : MonoBehaviour
{
    public CollectableItemSO itemData;
    public float maxHealth;

    private float health;

    void Start()
    {
        health = maxHealth;
    }

    public bool takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            return true;
        }
        return false;
    }

    public void setHealth(float health)
    {
        this.health = health;
    }

    public string getHealth()
    {
        return health.ToString();
    }

    public int getHealthInt()
    {
        return (int)health;
    }
}