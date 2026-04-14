using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
    [Header("Health")]
    public int health = 3;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float rotateSpeed = 50f;

    [Header("VFX")]
    public GameObject explosionPrefab;

    private SpriteRenderer sr;
    private Color originalColor;
    private Vector2 moveDirection; // direction set on spawn

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        // Default direction is straight down
        if (moveDirection == Vector2.zero)
        {
            moveDirection = Vector2.down;
        }
    }

    // Called by MeteorSpawner right after Instantiate
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        if (transform.position.y < -8f || 
            transform.position.x < -6f || 
            transform.position.x > 6f)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashWhite());
        }
    }

    IEnumerator FlashWhite()
    {
        sr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColor;
    }

    void Die()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Die();
        }
    }
}