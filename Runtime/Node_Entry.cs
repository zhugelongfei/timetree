namespace Lonfee.TimeTree
{
    internal class Node_Entry : ATreeNode
    {
        protected override void OnEnter()
        {
            IsSelfFinished = true;
        }

        protected override void OnExit()
        {

        }

        public override string ToString()
        {
            return "Entry";
        }
    }
}
