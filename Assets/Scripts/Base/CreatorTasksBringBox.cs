using UnityEngine;

public class CreatorTasksBringBox
{
    private BoxKeeper _boxKeeper;
    private Vector3 _pointDestination;

    public CreatorTasksBringBox(BoxKeeper boxKeeper, Vector3 pointDestination)
    {
        _boxKeeper = boxKeeper;
        _pointDestination = pointDestination;
    }

    public bool GetTask(out ITaskable task)
    {
        Box box = _boxKeeper.GetBox();

        if (box != null)
        {
            task = new BringBoxTask(box, _pointDestination);

            return true;
        }

        task = null;

        return false;
    }
}