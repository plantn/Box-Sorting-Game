using UnityEngine;

public class DropState : NPCStateMachine
{
    private bool hasStartedDropping;
    private bool hasDropped;
    private float targetHandAngle;

    public override void EnterState()
    {
        if (npc.CarriedBox == null)
        {
            npc.TransitionToState(new IdleState());
            return;
        }

        Transform dropZone = npc.GetDropZoneForBox(npc.CarriedBox);
        bool isDropZoneOnLeft = dropZone.position.x < npc.transform.position.x;
        targetHandAngle = isDropZoneOnLeft ? GameConstants.LEFT_HAND_ANGLE : GameConstants.RIGHT_HAND_ANGLE;
        
        hasStartedDropping = true;
        hasDropped = false;
    }

    public override void UpdateState()
    {
        if (hasStartedDropping)
        {
            RotateHandTowardsTarget();
            
            if (IsHandAtTargetAngle())
            {
                DropBox();
            }
        }
        else if (hasDropped)
        {
            RotateHandToCarryPosition();
            npc.TransitionToState(new IdleState());
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
    }    private void DropBox()
    {
        if (npc.CarriedBox != null)
        {
            Transform dropZone = npc.GetDropZoneForBox(npc.CarriedBox);
            npc.CarriedBox.transform.position = dropZone.position;
            npc.DropBox();
            hasStartedDropping = false;
            hasDropped = true;
        }
    }

    private void RotateHandToCarryPosition()
    {
        npc.HandJoint.rotation = Quaternion.Euler(GameConstants.IDLE_X_POSITION, GameConstants.IDLE_X_POSITION, GameConstants.CARRYING_HAND_ANGLE);
    }
}
