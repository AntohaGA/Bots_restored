using System.Collections;
using UnityEngine;

public class BringBoxTask: ITaskable
{
    private Box _box;
    private Vector3 _destination;

    public BringBoxTask(Box box, Vector3 destination)
    {
        _box = box;
        _destination = destination;
    }

    public IEnumerator Do(Bot bot)
    {
        yield return bot.GoTo(_box.SpotLift);
        yield return bot.LiftBox(_box);
        yield return bot.GoTo(_destination);
    }
}