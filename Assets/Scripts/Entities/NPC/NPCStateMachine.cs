using UnityEngine;

public abstract class NPCStateMachine
{
    protected NPCController npc;
    
    public void SetNPC(NPCController controller)
    {
        npc = controller;
    }
    
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
