using UnityEngine;

public class CarryState : NPCStateMachine
{
    private Transform targetDropZone;

    public override void EnterState()
    {
        if (npc.CarriedBox == null)
        {
            npc.TransitionToState(new IdleState());
            return;
        }

        targetDropZone = npc.GetDropZoneForBox(npc.CarriedBox);
    }

    public override void UpdateState()
    {
        if (npc.CarriedBox == null)
        {
            npc.TransitionToState(new IdleState());
            return;
        }

        MoveTowardsDropZone();

        if (IsAtDropZone())
        {
            npc.TransitionToState(new DropState());
        }
    }

    public override void ExitState()
    {
    }

    private void MoveTowardsDropZone()
    {
        Vector3 direction = (targetDropZone.position - npc.transform.position).normalized;
        npc.transform.position += direction * GameConstants.MOVEMENT_SPEED * Time.deltaTime;
    }

    private bool IsAtDropZone()
    {
        return Vector3.Distance(npc.transform.position, targetDropZone.position) <= GameConstants.PICKUP_RANGE;
    }
}
