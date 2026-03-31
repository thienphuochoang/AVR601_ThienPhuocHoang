using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float sidewaysSpeed = 1.5f;
    public float changeDirectionTime = 2f;

    private Vector2 moveDirection;
    private float directionTimer;
    private bool enteredScreen = false;

    private float minX, maxX;
    private float destroyY;
    private float entryThreshold;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        minX = -2.2f;
        maxX =  2.2f;

        Camera cam = Camera.main;
        destroyY       = -cam.orthographicSize - 1f;
        entryThreshold =  cam.orthographicSize - 1f;

        // Always start straight down
        moveDirection = Vector2.down;
        directionTimer = changeDirectionTime;
    }

    void Update()
    {
        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
            return;
        }

        // Switch to patrol once inside screen
        if (!enteredScreen && transform.position.y < entryThreshold)
        {
            enteredScreen = true;
        }

        // After entering, start counting to first direction change
        if (enteredScreen)
        {
            directionTimer -= Time.deltaTime;
            if (directionTimer <= 0f)
            {
                float newX = Random.Range(-sidewaysSpeed, sidewaysSpeed);
                moveDirection = new Vector2(newX, -0.5f).normalized;
                directionTimer = changeDirectionTime;
            }
        }

        // Single movement call for BOTH phases
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

        // Bounce off left/right edges
        Vector3 pos = transform.position;
        if (pos.x < minX || pos.x > maxX)
        {
            moveDirection.x *= -1f;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            transform.position = pos;
        }
    }
}