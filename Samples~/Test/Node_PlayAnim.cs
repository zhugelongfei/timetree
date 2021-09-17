using Lonfee.TimeTree;

public class Node_PlayAnim : ATreeNode
{
    private string animName;

    public Node_PlayAnim(string animName)
    {
        this.animName = animName;
    }

    protected override void OnEnter()
    {
        // play your animation

        // oh, i don't have any animation, so we call this immediately
        OnAnimEnd();
    }

    private void OnAnimEnd()
    {
        IsSelfFinished = true;
    }

    protected override void OnExit()
    {

    }

    public override string ToString()
    {
        // Time tree debug will print log by this.ToString().
        // so we need override this function to view we wanted log.
        return string.Format("Play Animation({0})", animName);
    }

}
