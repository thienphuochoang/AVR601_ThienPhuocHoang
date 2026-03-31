using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    private float destroyY;

    void Start()
    {
        Camera cam = Camera.main;
        destroyY = cam.orthographicSize + 1f; // above screen top
    }

    void Update()
    {
        // Move upward
        transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);

        // Destroy when off screen
        if (transform.position.y > destroyY)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            /*EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(damage);*/

            Destroy(gameObject); // destroy bullet on hit
        }
    }
}