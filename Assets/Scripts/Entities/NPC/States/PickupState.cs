using UnityEngine;

public class PickupState : NPCStateMachine
{
    private BoxEntity targetBox;
    private float targetHandAngle;
    private bool isReaching;

    public override void EnterState()
    {
        targetBox = npc.FindClosestBox();
        if (targetBox == null)
        {
            npc.TransitionToState(new IdleState());
            return;
        }

        float distance = Vector3.Distance(npc.transform.position, targetBox.transform.position);
        if (distance > GameConstants.PICKUP_RANGE)
        {
            npc.TransitionToState(new SeekState());
            return;
        }

        targetHandAngle = npc.GetHandAngleForBox(targetBox);
        isReaching = true;
    }

    public override void UpdateState()
    {
        if (targetBox == null)
        {
            npc.TransitionToState(new IdleState());
            return;
        }

        if (isReaching)
        {
            RotateHandTowardsTarget();
            
            if (IsHandAtTargetAngle())
            {
                PickUpBox();
            }
        }
    }

    public override void ExitState()
    {
    }

    private void RotateHandTowardsTarget()
    {
        float currentAngle = npc.HandJoint.rotation.eulerAngles.z;
        if (currentAngle > GameConstants.ANGLE_NORMALIZATION) currentAngle -= GameConstants.ANGLE_FULL_CIRCLE;
        
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetHandAngle, GameConstants.HAND_REACH_SPEED * Time.deltaTime);
        npc.HandJoint.rotation = Quaternion.Euler(GameConstants.IDLE_X_POSITION, GameConstants.IDLE_X_POSITION, newAngle);
    }
    
    private bool IsHandAtTargetAngle()
    {
        float currentAngle = npc.HandJoint.rotation.eulerAngles.z;
        if (currentAngle > GameConstants.ANGLE_NORMALIZATION) currentAngle -= GameConstants.ANGLE_FULL_CIRCLE;
        
        return Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetHandAngle)) < GameConstants.HAND_ANGLE_TOLERANCE;
    }    private void PickUpBox()
    {
        npc.PickUpBox(targetBox);
        RotateHandToCarryPosition();
        npc.TransitionToState(new CarryState());
    }

    private void RotateHandToCarryPosition()
    {
        npc.HandJoint.rotation = Quaternion.Euler(GameConstants.IDLE_X_POSITION, GameConstants.IDLE_X_POSITION, GameConstants.CARRYING_HAND_ANGLE);
    }
}
