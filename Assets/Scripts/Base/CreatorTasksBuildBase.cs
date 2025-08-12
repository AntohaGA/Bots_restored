using UnityEngine;

public class CreatorTasksBuildBase
{
    private Base _basePrefab;

    public CreatorTasksBuildBase()
    {
        _basePrefab = Resources.Load<Base>("Resources/Base");
    }

    public void CreateTask(out ITaskable task, Vector3 pointSpawn)
    {
        task = new BuildBaseTask(pointSpawn);
    }
}