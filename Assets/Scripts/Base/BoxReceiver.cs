using UnityEngine;

public class BoxReceiver
{
    private int _boxCount;
    private ResourceScanner _boxScanner;

    public BoxReceiver(ResourceScanner boxScanner)
    {
        _boxScanner = boxScanner;
    }

    public int BoxCount => _boxCount;

    public void TakeBox(Box box)
    {
        if (box == null) return;

        box.IsTaken = false;
        box.transform.SetParent(null);
        box.SetRigidBodyKinematic(false);
        _boxCount++;
        Debug.Log("Box count: " + _boxCount);
        _boxScanner.ReturnBox(box);
    }
}