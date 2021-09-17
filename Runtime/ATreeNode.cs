using System.Collections.Generic;

namespace Lonfee.TimeTree
{
    public abstract class ATreeNode
    {
        protected List<ATreeNode> childList = null;
        private bool isSelfFinished = false;
        private bool isRunning = false;

        protected bool IsSelfFinished
        {
            get { return isSelfFinished; }
            set { isSelfFinished = value; OnSelfFinished(); }
        }

        internal bool IsFinished
        {
            get
            {
                if (!isSelfFinished)
                    return false;

                if (childList == null)
                    return true;

                for (int i = 0, iMax = childList.Count; i < iMax; i++)
                {
                    if (!childList[i].IsFinished)
                        return false;
                }

                return true;
            }
        }

        public ATreeNode AddChild(ATreeNode node)
        {
            if (node == null)
                return null;

            if (childList == null)
                childList = new List<ATreeNode>(2);

            childList.Add(node);

            return node;
        }

        public void RemoveChild(ATreeNode node)
        {
            if (childList == null)
                return;

            childList.Remove(node);
        }

        private void OnSelfFinished()
        {
            Exit();

            isRunning = false;

            if (childList != null)
            {
                for (int i = 0, iMax = childList.Count; i < iMax; i++)
                {
                    childList[i].Enter();
                }
            }
        }

        internal void Enter()
        {
            isSelfFinished = false;

            isRunning = true;

            UnityEngine.Debug.LogError("Enter : " + this);
            OnEnter();
        }

        internal void Update(float deltaTime)
        {
            if (!isSelfFinished)
            {
                // update self
                OnUpdate(deltaTime);
            }
            else
            {
                // update child
                if (childList != null)
                {
                    for (int i = 0, iMax = childList.Count; i < iMax; i++)
                    {
                        childList[i].Update(deltaTime);
                    }
                }
            }
        }

        internal void Exit()
        {
            OnExit();
            UnityEngine.Debug.LogError("Exit : " + this);
        }

        protected abstract void OnEnter();

        protected virtual void OnUpdate(float deltaTime) { }

        protected abstract void OnExit();

        internal void Stop()
        {
            if (!isSelfFinished)
            {
                if (isRunning)
                {
                    StopSelf();
                }
            }
            else
            {
                if (childList != null)
                {
                    for (int i = 0, iMax = childList.Count; i < iMax; i++)
                    {
                        childList[i].Stop();
                    }
                }
            }
        }

        protected virtual void StopSelf()
        {
            Exit();
        }

    }
}
