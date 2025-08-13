using System.Collections;
using UnityEngine;

public class BuildBaseTask : ITaskable
{
    private Vector3 _pointBuildBase;
    private BaseSpawner _spawner;

    public BuildBaseTask(BaseSpawner baseSpawner, Vector3 pointBuildBase)
    {
        _pointBuildBase = pointBuildBase;
        _spawner = baseSpawner;
    }

    public IEnumerator Do(Bot bot)
    {      
        yield return bot.GoTo(_pointBuildBase);

        Debug.Log("отправился строить базу корутина");

        _spawner.SpawnBase(_pointBuildBase);
    }
}