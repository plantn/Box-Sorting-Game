using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI scoreText;
    private int currentScore = GameConstants.INITIAL_SCORE;
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        UpdateScoreDisplay();
    }
    
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreDisplay();
    }
    
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }
    
    public int GetScore()
    {
        return currentScore;
    }
}
