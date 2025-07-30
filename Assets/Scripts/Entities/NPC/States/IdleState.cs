using UnityEngine;

public class IdleState : NPCStateMachine
{
    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        BoxEntity closestBox = npc.FindClosestBox();
        
        if (closestBox != null)
        {
            npc.TransitionToState(new SeekState());
        }
        else
        {
            MoveToIdlePosition();
        }
    }

    public override void ExitState()
    {
    }

    private void MoveToIdlePosition()
    {
        Vector3 currentPosition = npc.transform.position;
        Vector3 idlePosition = new Vector3(GameConstants.IDLE_X_POSITION, currentPosition.y, currentPosition.z);
        
        float distanceToIdle = Mathf.Abs(currentPosition.x);
        if (distanceToIdle > GameConstants.IDLE_POSITION_THRESHOLD)
        {
            Vector3 direction = (idlePosition - currentPosition).normalized;
            npc.transform.position += direction * GameConstants.MOVEMENT_SPEED * Time.deltaTime;
        }
    }
}
