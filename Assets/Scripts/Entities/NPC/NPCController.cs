using UnityEngine;
using System.Collections.Generic;

public class NPCController : MonoBehaviour
{
    [SerializeField] private Transform handJoint;
    [SerializeField] private Transform leftDropZone;
    [SerializeField] private Transform rightDropZone;
    [SerializeField] private Transform boxGripZone;

    [SerializeField] private GameObject[] wheels;
    [SerializeField] private float wheelSpinSpeed = GameConstants.NPC_DEFAULT_WHEEL_SPIN_SPEED;

    private NPCStateMachine currentState;
    private BoxEntity carriedBox;
    private Dictionary<BoxEntity, float> recentlyDroppedBoxes = new Dictionary<BoxEntity, float>();
    private Vector3 lastPosition;
    private float currentMovementDirection = 0f;
    
    public Transform HandJoint => handJoint;
    public Transform LeftDropZone => leftDropZone;
    public Transform RightDropZone => rightDropZone;
    public Transform BoxGripZone => boxGripZone;
    public BoxEntity CarriedBox => carriedBox;
    
    private void Start()
    {
        lastPosition = transform.position;
        TransitionToState(new IdleState());
    }
    
    private void Update()
    {
        currentState?.UpdateState();
        CleanupTimeoutBoxes();
        UpdateWheelRotation();
    }
    
    private void UpdateWheelRotation()
    {
        Vector3 currentPosition = transform.position;
        float deltaX = currentPosition.x - lastPosition.x;
        
        if (Mathf.Abs(deltaX) > GameConstants.MOVEMENT_DETECTION_THRESHOLD)
        {
            currentMovementDirection = Mathf.Sign(deltaX);
        }
        else
        {
            currentMovementDirection = GameConstants.IDLE_X_POSITION;
        }
        
        if (wheels != null && wheels.Length > GameConstants.INITIAL_SCORE)
        {
            float rotationAmount = currentMovementDirection * GameConstants.WHEEL_SPIN_MULTIPLIER * wheelSpinSpeed * Time.deltaTime;

            foreach (GameObject wheel in wheels)
            {
                if (wheel != null)
                {
                    wheel.transform.Rotate(GameConstants.IDLE_X_POSITION, GameConstants.IDLE_X_POSITION, rotationAmount);
                }
            }
        }
        
        lastPosition = currentPosition;
    }
    
    private void CleanupTimeoutBoxes()
    {
        List<BoxEntity> expiredBoxes = new List<BoxEntity>();
        
        foreach (var kvp in recentlyDroppedBoxes)
        {
            if (Time.time >= kvp.Value)
            {
                expiredBoxes.Add(kvp.Key);
            }
        }
        
        foreach (BoxEntity box in expiredBoxes)
        {
            recentlyDroppedBoxes.Remove(box);
        }
    }
    
    public void TransitionToState(NPCStateMachine newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.SetNPC(this);
        currentState.EnterState();
    }
    
    public void PickUpBox(BoxEntity box)
    {
        carriedBox = box;
        box.transform.SetParent(boxGripZone);
        
        Vector3 overheadPosition = new Vector3(GameConstants.IDLE_X_POSITION, Mathf.Abs(boxGripZone.localPosition.y), GameConstants.IDLE_X_POSITION);
        box.transform.localPosition = overheadPosition;
        
        box.OnPickUp();
    }
    
    public void DropBox()
    {
        if (carriedBox != null)
        {
            Vector3 currentWorldScale = carriedBox.transform.lossyScale;
            Quaternion currentWorldRotation = carriedBox.transform.rotation;
            
            carriedBox.transform.SetParent(null);
            
            carriedBox.transform.rotation = currentWorldRotation;
            carriedBox.transform.localScale = currentWorldScale;
            
            carriedBox.OnDrop();
            
            recentlyDroppedBoxes[carriedBox] = Time.time + GameConstants.BOX_PICKUP_TIMEOUT;
            
            carriedBox = null;
        }
    }
     
    public BoxEntity FindClosestBox()
    {
        float searchRadius = GameConstants.BOX_DETECTION_RANGE;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, searchRadius);
        BoxEntity closest = null;
        float closestDistanceSqr = float.MaxValue;
        
        foreach (Collider2D collider in colliders)
        {
            BoxEntity box = collider.GetComponent<BoxEntity>();
            
            if (box != null && !box.IsDeposited && !IsBoxOnTimeout(box))
            {
                float distanceSqr = (box.transform.position - transform.position).sqrMagnitude;
                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    closest = box;
                }
            }
        }
        
        return closest;
    }
    
    private bool IsBoxOnTimeout(BoxEntity box)
    {
        return recentlyDroppedBoxes.ContainsKey(box) && Time.time < recentlyDroppedBoxes[box];
    }
    
    public Transform GetDropZoneForBox(BoxEntity box)
    {
        return box.GetColor() == BoxColor.Red ? leftDropZone : rightDropZone;
    }
    
    public float GetHandAngleForBox(BoxEntity box)
    {
        bool isBoxOnLeft = box.transform.position.x < transform.position.x;
        return isBoxOnLeft ? GameConstants.LEFT_HAND_ANGLE : GameConstants.RIGHT_HAND_ANGLE;
    }
}
