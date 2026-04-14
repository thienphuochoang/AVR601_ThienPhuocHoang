using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    void Update()
    {
        // Move upward
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>()?.TakeDamage(damage);
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
        // Auto destroy when off screen
        Destroy(gameObject);
    }
}