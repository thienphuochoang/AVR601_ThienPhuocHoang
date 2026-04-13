using UnityEngine;

public class ExplosionVFX : MonoBehaviour
{
    public float lifetime = 0.5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}