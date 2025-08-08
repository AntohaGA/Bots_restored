using UnityEngine;

public class BaseStorage : MonoBehaviour
{
    private int _countBoxes = 0;

    public void AddBoxOnBase(Box box)
    {
        if (box == null)
            return;

        box.transform.SetParent(null);
        box.SetRigidBodyKinematic(false);
        box.Return();
        _countBoxes++;
    }
}