using UnityEngine;

public class CreatorTasksBuildBase
{
    private Base _basePrefab;

    public CreatorTasksBuildBase()
    {
        _basePrefab = Resources.Load<Base>("Resources/Base");
    }

    public ITaskable Get(Vector3 point)
    {

        return null;
    }
}