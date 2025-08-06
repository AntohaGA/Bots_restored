using System.Collections.Generic;
using UnityEngine;

public class BoxStorage
{
    [SerializeField] private PoolBoxes _poolBoxes;

    private int _countBasesBoxes = 0;

    public HashSet<Box> ReservedBoxes { get; private set; }

    public BoxStorage()
    {
        ReservedBoxes = new HashSet<Box>();
    }

    public void ReserveBox(Box box)
    {
        if (box == null)
            return;

        ReservedBoxes.Add(box);
    }

    public void AddBoxOnBase(Box box)
    {
        if (box == null)
            return;

        box.transform.SetParent(null); 
        box.SetRigidBodyKinematic(false);
        ReservedBoxes.Remove(box);
        _poolBoxes.ReturnInstance(box);
        _countBasesBoxes++;
    }
}