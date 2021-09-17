using System.Collections.Generic;

namespace Lonfee.TimeTree
{
    public abstract class ATreeNode
    {
        private enum EState
        {
            None,
            Running,
            Finished,
        }

        private const int CHILD_DEFAULT_SIZE = 2;

        protected List<ATreeNode> childList = null;
        private EState mState = EState.None;

        protected bool IsSelfFinished
        {
            get { return mState == EState.Finished; }
            set
            {
                if (value && mState != EState.Finished)
                {
                    mState = EState.Finished;

                    OnSelfFinished();
                }
            }
        }

        internal bool IsFinished
        {
            get
            {
                if (mState != EState.Finished)
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
                childList = new List<ATreeNode>(CHILD_DEFAULT_SIZE);

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
            mState = EState.Running;

#if DEBUG_LF_TIME_TREE && UNITY_EDITOR
            UnityEngine.Debug.LogFormat(">>>>Enter: {0}", this.ToString());
#endif
            OnEnter();
        }

        internal void Update(float deltaTime)
        {
            if (mState != EState.Finished)
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

#if DEBUG_LF_TIME_TREE && UNITY_EDITOR
            UnityEngine.Debug.LogFormat("<<<<Exit: {0}", this.ToString());
#endif
        }

        protected abstract void OnEnter();

        protected virtual void OnUpdate(float deltaTime) { }

        protected abstract void OnExit();

        internal void Stop()
        {
            if (mState == EState.None)
                return;

            if (mState == EState.Running)
            {
                StopSelf();
            }
            else if (mState == EState.Finished)
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
