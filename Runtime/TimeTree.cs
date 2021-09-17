using System;
using UnityEngine;

namespace Lonfee.TimeTree
{
    public class TimeTree
    {
        private ATreeNode entryNode = null;
        private Action<bool> finishedCallback;
        private bool isRunning = false;

        public TimeTree()
            : this(null)
        {

        }

        public TimeTree(Action<bool> finishedCallback)
        {
            this.finishedCallback = finishedCallback;
            entryNode = new Node_Entry();
        }

        public ATreeNode GetEntry()
        {
            return entryNode;
        }

        public void Start()
        {
            if (isRunning)
                return;

            isRunning = true;

            entryNode.Enter();
        }

        public void Update(float deltaTime)
        {
            if (!isRunning)
                return;

            entryNode.Update(deltaTime);

            if (entryNode.IsFinished)
            {
                OnFinished(false);
            }
        }

        public void Stop()
        {
            if (!isRunning)
                return;

            OnFinished(true);
        }

        private void OnFinished(bool isBreak)
        {
            isRunning = false;

            if (isBreak)
            {
                // stop node
                entryNode.Stop();
            }

            finishedCallback?.Invoke(isBreak);
        }
    }
}
