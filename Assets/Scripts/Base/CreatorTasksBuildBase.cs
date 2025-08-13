using UnityEngine;

public class CreatorTasksBuildBase
{
    public void CreateTask(BaseSpawner baseSpawner, out ITaskable task, Vector3 pointSpawn)
    {
        task = new BuildBaseTask(baseSpawner, pointSpawn);
    }
}