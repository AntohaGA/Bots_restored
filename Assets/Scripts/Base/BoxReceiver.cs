using UnityEngine;

public class BoxReceiver
{
    [SerializeField] private BoxSpawner _boxSpawner;

    private int _boxCount;
    private BoxScanner _boxScanner;

    public BoxReceiver(BoxScanner boxScanner)
    {
        _boxScanner = boxScanner;
    }

    public int BoxCount => _boxCount;

    public void TakeBox(Box box)
    {
        if (box == null)
            return;

        box.transform.SetParent(null);
        box.SetRigidBodyKinematic(false);
        _boxCount++;
        Debug.Log("Box count: " + _boxCount);

        _boxScanner.ReturnBox(box);
    }
}