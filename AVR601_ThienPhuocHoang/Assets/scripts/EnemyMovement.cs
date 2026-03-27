using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float sidewaysSpeed = 1.5f;
    public float swoopAmount = 2f;
    public float changeDirectionTime = 2f;

    private Vector2 moveDirection;
    private float directionTimer;
    private bool enteredScreen = false;

    private float minX, maxX;

    void Start()
    {
        // Calculate play area bounds from background width
        minX = -2.2f;
        maxX =  2.2f;

        // Start moving downward into the screen
        moveDirection = new Vector2(Random.Range(-0.5f, 0.5f), -1f).normalized;
        directionTimer = changeDirectionTime;
    }

    void Update()
    {
        // Phase 1: fly into screen from top
        if (!enteredScreen)
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

            // Once inside the visible area, start patrol behaviour
            if (transform.position.y < 2.5f)
                enteredScreen = true;
        }
        // Phase 2: patrol/swoop behaviour inside play area
        else
        {
            directionTimer -= Time.deltaTime;

            if (directionTimer <= 0f)
            {
                // Pick a new random sideways direction
                float newX = Random.Range(-sidewaysSpeed, sidewaysSpeed);
                moveDirection = new Vector2(newX, -0.3f).normalized;
                directionTimer = changeDirectionTime;
            }

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            // Bounce off left/right edges of play area
            Vector3 pos = transform.position;
            if (pos.x < minX || pos.x > maxX)
            {
                moveDirection.x *= -1f;
                pos.x = Mathf.Clamp(pos.x, minX, maxX);
                transform.position = pos;
            }

            // Destroy if they fly off the bottom
            if (transform.position.y < -8f)
                Destroy(gameObject);
        }
    }
}