using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    protected Rigidbody2D rb2D;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
}
