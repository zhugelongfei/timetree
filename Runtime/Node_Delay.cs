namespace Lonfee.TimeTree
{
    public class Node_Delay : ATreeNode
    {
        private float delayTime = 0;
        private float timer = 0;

        public Node_Delay(float delayTime)
        {
            this.delayTime = delayTime;
        }

        protected override void OnEnter()
        {
            timer = 0;
        }

        protected override void OnUpdate(float deltaTime)
        {
            timer += deltaTime;

            if (timer >= delayTime)
                IsSelfFinished = true;
        }

        protected override void OnExit()
        {

        }

        public override string ToString()
        {
            return string.Format("Delay({0})", delayTime);
        }
    }
}
