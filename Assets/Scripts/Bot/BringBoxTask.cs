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
        Debug.Log("начал делать работу иду, поднимаю и несу обратно " + _box);

        yield return bot.GoTo(_box.SpotForLift);
        yield return bot.LiftBox(_box);
        yield return bot.GoTo(_pointDestination);

        bot.MadeFree();
    }
}