using UnityEngine;

public class SeekState : NPCStateMachine
{
    private BoxEntity targetBox;

    public override void EnterState()
    {
        targetBox = npc.FindClosestBox();
    }

    public override void UpdateState()
    {
        if (targetBox == null)
        {
            npc.TransitionToState(new IdleState());
            return;
        }

        float distance = Vector3.Distance(npc.transform.position, targetBox.transform.position);
        if (distance > GameConstants.MAX_CHASE_DISTANCE)
        {
            npc.TransitionToState(new IdleState());
            return;
        }

        MoveTowardsBox();
        
        if (IsInPickupRange())
        {
            npc.TransitionToState(new PickupState());
        }
    }

    public override void ExitState()
    {
    }

    private void MoveTowardsBox()
    {
        Vector3 direction = (targetBox.transform.position - npc.transform.position).normalized;
        npc.transform.position += direction * GameConstants.MOVEMENT_SPEED * Time.deltaTime;
    }

    private bool IsInPickupRange()
    {
        return Vector3.Distance(npc.transform.position, targetBox.transform.position) <= GameConstants.PICKUP_RANGE;
    }
}
