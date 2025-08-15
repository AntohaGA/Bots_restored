public class CreatorTasksBuildBase
{
    private BaseSpawner _baseSpawner;

    public CreatorTasksBuildBase(BaseSpawner baseSpawner)
    {
        _baseSpawner = baseSpawner;
    }

    public void CreateTask(out ITaskable task, Flag flag)
    {
        task = new BuildBaseTask(_baseSpawner, flag);
    }
}