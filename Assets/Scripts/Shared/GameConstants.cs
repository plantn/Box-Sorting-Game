using UnityEngine;

public static class GameConstants
{
    public const float LEFT_HAND_ANGLE = 15f;
    public const float RIGHT_HAND_ANGLE = -195f;
    public const float CARRYING_HAND_ANGLE = -90f;
    public const float PICKUP_RANGE = 0.8f;
    public const float BOX_DETECTION_RANGE = 30f;
    public const float MAX_CHASE_DISTANCE = 25f;
    public const float MOVEMENT_SPEED = 3f;
    public const float HAND_REACH_SPEED = 90f;
    public const float BOX_PICKUP_TIMEOUT = 0.5f;
    
    public const float MOVEMENT_DETECTION_THRESHOLD = 0.001f;
    public const float IDLE_POSITION_THRESHOLD = 0.1f;
    public const float HAND_ANGLE_TOLERANCE = 1f;
    public const float ANGLE_NORMALIZATION = 180f;
    public const float ANGLE_FULL_CIRCLE = 360f;
    public const float WHEEL_SPIN_MULTIPLIER = -1f;
    public const float IDLE_X_POSITION = 0f;
    
    public const float BOX_DEPOSIT_Y_THRESHOLD = -2.5f;
    public const float BOX_SCORE_Y_THRESHOLD = -5.25f;
    public const int SCORE_POINTS_PER_BOX = 1;
    public const int INITIAL_SCORE = 0;
    
    public const float SPAWNER_MOVEMENT_RANGE = 6f;
    public const float RAIL_DIRECTION_RIGHT = 1f;
    public const float RAIL_DIRECTION_LEFT = -1f;
    
    public const float RAIL_DEFAULT_SPEED_BOOST = 5f;
    public const float RAIL_DEFAULT_MAX_SPEED = 15f;
    
    public const float SPAWNER_DEFAULT_MOVEMENT_SPEED = 2f;
    public const float SPAWNER_MIN_SPAWN_INTERVAL = 3f;
    public const float SPAWNER_MAX_SPAWN_INTERVAL = 7f;
    public const float SPAWNER_DEFAULT_DROP_FORCE = 5f;
    
    public const float NPC_DEFAULT_WHEEL_SPIN_SPEED = 180f;
}
