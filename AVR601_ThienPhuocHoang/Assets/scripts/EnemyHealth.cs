using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 3;
    private int currentHealth;
    public Image[] hpDots;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateDots();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateDots();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateDots()
    {
        for (int i = 0; i < hpDots.Length; i++)
        {
            hpDots[i].enabled = i < currentHealth;
        }
    }

    void Die()
    {
        Debug.Log("Enemy Dead");
        Destroy(gameObject);
    }
}