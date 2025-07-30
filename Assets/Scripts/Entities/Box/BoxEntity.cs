using UnityEngine;

public class BoxEntity : EntityBase, IPickable
{
    [SerializeField] private BoxColor color;
    private bool isDeposited = false;

    public BoxColor GetColor() => color;
    public bool IsDeposited => isDeposited;

    private void Update()
    {
        if (!isDeposited && transform.position.y <= GameConstants.BOX_DEPOSIT_Y_THRESHOLD)
        {
            OnDeposit();
        }
        
        if (transform.position.y <= GameConstants.BOX_SCORE_Y_THRESHOLD)
        {
            OnScore();
        }
    }
    
    private void OnScore()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(GameConstants.SCORE_POINTS_PER_BOX);
        }
        Destroy(gameObject);
    }

    public void OnPickUp()
    {
        if (isDeposited) return;
        rb2D.bodyType = RigidbodyType2D.Kinematic;
    }

    public void OnDrop()
    {
        if (isDeposited) return;
        rb2D.bodyType = RigidbodyType2D.Dynamic;
    }

    public void OnDeposit()
    {
        isDeposited = true;
        rb2D.bodyType = RigidbodyType2D.Dynamic; 
    }   
}
