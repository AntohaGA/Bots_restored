using UnityEngine;

public class BoxReceiver
{
    [SerializeField] private BoxSpawner _boxSpawner;

    private BoxScanner _boxScanner;

    public BoxReceiver(BoxScanner boxScanner)
    {
        _boxScanner = boxScanner;
    }

    public void TakeBox(Box box)
    {
        if (box == null)
            return;

        box.transform.SetParent(null);
        box.SetRigidBodyKinematic(false);
        _boxScanner.ReturnBox(box);
    }
}