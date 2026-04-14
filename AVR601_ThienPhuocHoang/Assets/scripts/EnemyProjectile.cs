using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;

    void Update()
    {
        // Move downward
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Meteor"))
        {
            other.GetComponent<Meteor>()?.TakeDamage();
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}