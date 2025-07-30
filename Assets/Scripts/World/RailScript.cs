using UnityEngine;

public enum RailDirection
{
    Left,
    Right
}

public class RailScript : MonoBehaviour
{
    [SerializeField] private RailDirection direction = RailDirection.Right;
    [SerializeField] private float speedBoost = GameConstants.RAIL_DEFAULT_SPEED_BOOST;
    [SerializeField] private float maxSpeed = GameConstants.RAIL_DEFAULT_MAX_SPEED;
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        ApplySpeedBoost(collision.collider);
    }
            
    private void ApplySpeedBoost(Collider2D other)
    {
        if (!other.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) return;
        if (rb.bodyType != RigidbodyType2D.Dynamic) return;
        
        Vector2 boostVelocity = GetBoostVelocity(rb.linearVelocity);
        rb.linearVelocity = boostVelocity;
    }
    
    private Vector2 GetBoostVelocity(Vector2 currentVelocity)
    {
        float boostDirection = direction == RailDirection.Right ? GameConstants.RAIL_DIRECTION_RIGHT : GameConstants.RAIL_DIRECTION_LEFT;
        float newHorizontalSpeed = currentVelocity.x + (speedBoost * boostDirection * Time.deltaTime);
        
        newHorizontalSpeed = Mathf.Clamp(newHorizontalSpeed, -maxSpeed, maxSpeed);
        
        return new Vector2(newHorizontalSpeed, currentVelocity.y);
    }
}
