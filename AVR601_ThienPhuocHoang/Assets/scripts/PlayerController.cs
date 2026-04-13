using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Boundaries")]
    public SpriteRenderer backgroundSprite;

    [Header("Health")]
    public int maxHealth = 5;
    private int currentHealth;
    public Image[] hpDots;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.3f;
    private float nextFireTime = 0f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize;
        float shipHalfWidth  = GetComponent<SpriteRenderer>().bounds.extents.x;
        float shipHalfHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        if (backgroundSprite != null)
        {
            Bounds bg = backgroundSprite.bounds;
            minX = bg.min.x + shipHalfWidth;
            maxX = bg.max.x - shipHalfWidth;
        }
        else
        {
            float camWidth = camHeight * cam.aspect;
            minX = -camWidth + shipHalfWidth;
            maxX =  camWidth - shipHalfWidth;
        }

        minY = -camHeight + shipHalfHeight;
        maxY =  camHeight - shipHalfHeight;

        currentHealth = maxHealth;
        UpdateDots();
    }

    void Update()
    {
        float h = 0f, v = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)  h = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) h =  1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)  v = -1f;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)    v =  1f;

        movement = new Vector2(h, v).normalized;

        // Auto shoot
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FixedUpdate()
    {
        Vector2 newPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        rb.MovePosition(newPos);
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            AudioManager.Instance.PlayShoot();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
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
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
    }
}