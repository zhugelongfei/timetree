using Lonfee.TimeTree;
using UnityEngine;

public class TestTree : MonoBehaviour
{
    private TimeTree tt;

    public void Awake()
    {
        tt = new TimeTree(OnFinished);

        ATreeNode entry = tt.GetEntry();
        entry.AddChild(new Node_Delay(2)).AddChild(new Node_Delay(2));
        entry.AddChild(new Node_PlayAnim("Attack")).AddChild(new Node_Delay(3)).AddChild(new Node_PlayAnim("Idle"));

        tt.Start();
    }

    public void Update()
    {
        tt.Update(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.Debug.Log("Stop time tree");
            tt.Stop();
        }
    }

    private void OnFinished(bool obj)
    {
        UnityEngine.Debug.Log("Finished : is break : " + obj);
    }
}
