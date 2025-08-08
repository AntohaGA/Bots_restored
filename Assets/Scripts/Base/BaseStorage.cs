using UnityEngine;

public class BaseStorage : MonoBehaviour
{
    private PoolBoxes _poolBoxes;

    private int _countBoxes = 0;

    public void Init(PoolBoxes poolBoxes)
    {
        _poolBoxes = poolBoxes;
    }

    public void AddBoxOnBase(Box box)
    {
        if (box == null)
            return;

        box.transform.SetParent(null);
        box.SetRigidBodyKinematic(false);
        _poolBoxes.ReturnInstance(box);
        _countBoxes++;
    }
}