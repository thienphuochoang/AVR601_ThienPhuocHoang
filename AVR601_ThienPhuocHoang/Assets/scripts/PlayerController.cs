using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Boundaries")]
    public SpriteRenderer backgroundSprite;

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
            // X: stay inside background width
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

        // Y: always use camera visible area (background is taller due to scrolling)
        minY = -camHeight + shipHalfHeight;
        maxY =  camHeight - shipHalfHeight;
    }

    void Update()
    {
        float h = 0f, v = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)  h = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) h =  1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)  v = -1f;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)    v =  1f;

        movement = new Vector2(h, v).normalized;
    }

    void FixedUpdate()
    {
        Vector2 newPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        rb.MovePosition(newPos);
    }
}