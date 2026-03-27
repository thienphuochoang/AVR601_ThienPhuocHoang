using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 2f;
    private float spriteHeight;
    private Transform[] tiles;

    void Start()
    {
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
        
        tiles = new Transform[2];
        tiles[0] = this.transform;

        GameObject copy = Instantiate(gameObject, 
            transform.position + Vector3.up * spriteHeight, 
            Quaternion.identity, transform.parent);
        
        // Remove the script from the copy to avoid recursion
        Destroy(copy.GetComponent<ScrollingBackground>());
        tiles[1] = copy.transform;
    }

    void Update()
    {
        foreach (Transform t in tiles)
        {
            // Move both backgrounds down
            t.Translate(Vector2.down * scrollSpeed * Time.deltaTime);
            
            if (t.position.y < -spriteHeight)
            {
                float highestY = Mathf.Max(tiles[0].position.y, tiles[1].position.y);
                t.position = new Vector3(t.position.x, highestY + spriteHeight, t.position.z);
            }
        }
    }
}