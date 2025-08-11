using System.Collections;
using UnityEngine;

public class BringBoxTask: ITaskable
{
    private Box _box;
    private Vector3 _pointDestination;

    public BringBoxTask(Box box, Vector3 pointDestination)
    {
        _box = box;
        _pointDestination = pointDestination;
    }

    public IEnumerator Do(Bot bot)
    {
        Debug.Log("в методе Do задание bringboxTask");

        bot.GoTo(_box.SpotForLift);
        bot.LiftBox(_box);
        bot.GoToWithBox(_pointDestination);

        yield return null;
    }
}