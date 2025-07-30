using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] boxPrefabs;
    [SerializeField] float movementSpeed = GameConstants.SPAWNER_DEFAULT_MOVEMENT_SPEED;
    [SerializeField] float minSpawnInterval = GameConstants.SPAWNER_MIN_SPAWN_INTERVAL;
    [SerializeField] float maxSpawnInterval = GameConstants.SPAWNER_MAX_SPAWN_INTERVAL;
    [SerializeField] float dropForce = GameConstants.SPAWNER_DEFAULT_DROP_FORCE;
    [SerializeField] private SpriteRenderer colorDecoration;
    
    private float nextSpawnTime;
    private float startTime;
    private int nextBoxIndex;
    
    void Start()
    {
        startTime = Time.time;
        SetNextSpawnTime();
        SetNextBoxColor();
    }
    
    void Update()
    {
        MovePeriodically();
        
        if (Time.time >= nextSpawnTime)
        {
            SpawnBox();
            SetNextSpawnTime();
        }
    }
    
    void MovePeriodically()
    {
        float xPosition = Mathf.Sin((Time.time - startTime) * movementSpeed) * GameConstants.SPAWNER_MOVEMENT_RANGE;
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
    }
    
    void SpawnBox()
    {
        if (boxPrefabs.Length == GameConstants.INITIAL_SCORE) return;
        
        GameObject boxToSpawn = boxPrefabs[nextBoxIndex];
        GameObject box = Instantiate(boxToSpawn, transform.position, Quaternion.identity);
        
        Rigidbody2D boxRb = box.GetComponent<Rigidbody2D>();
        if (boxRb != null)
        {
            boxRb.AddForce(Vector2.down * dropForce, ForceMode2D.Impulse);
        }
        
        SetNextBoxColor();
    }
    
    void SetNextBoxColor()
    {
        if (boxPrefabs.Length == GameConstants.INITIAL_SCORE || colorDecoration == null) return;
        
        nextBoxIndex = Random.Range(GameConstants.INITIAL_SCORE, boxPrefabs.Length);
        
        BoxEntity nextBoxEntity = boxPrefabs[nextBoxIndex].GetComponent<BoxEntity>();
        if (nextBoxEntity != null)
        {
            BoxColor nextColor = nextBoxEntity.GetColor();
            colorDecoration.color = GetColorFromBoxColor(nextColor);
        }
    }
    
    Color GetColorFromBoxColor(BoxColor boxColor)
    {
        switch (boxColor)
        {
            case BoxColor.Red:
                return Color.red;
            case BoxColor.Blue:
                return Color.blue;
            default:
                return Color.white;
        }
    }
    
    void SetNextSpawnTime()
    {
        float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        nextSpawnTime = Time.time + randomInterval;
    }
}
