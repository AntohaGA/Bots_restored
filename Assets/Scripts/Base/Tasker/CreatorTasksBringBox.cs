using UnityEngine;

public class CreatorTasksBringBox
{
    private BoxKeeper _boxKeeper;
    private Vector3 _pointOffsetDestination = new(5.2f, 1.5f, 10);
    private Vector3 _pointDestination;

    public CreatorTasksBringBox(BoxKeeper boxKeeper, Transform basa)
    {
        _boxKeeper = boxKeeper;
        _pointDestination = basa.TransformPoint(_pointOffsetDestination);
    }

    public bool CreateTask(out ITask task)
    {
        Box box = _boxKeeper.GetClosest(_pointDestination);

        if (box != null)
        {
            task = new BringBoxTask(box, _pointDestination);

            return true;
        }

        task = null;

        return false;
    }
}