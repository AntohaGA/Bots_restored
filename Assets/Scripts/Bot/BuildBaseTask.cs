using System.Collections;
using UnityEngine;

public class BuildBaseTask : ITaskable
{
    private Vector3 _pointBuildBase;

    public BuildBaseTask(Vector3 pointBuildBase)
    {
        _pointBuildBase = pointBuildBase;
    }

    public IEnumerator Do(Bot bot)
    {
        bot.GoTo(_pointBuildBase);
        yield return null;
    }
}