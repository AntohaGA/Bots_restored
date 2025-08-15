public class CreatorTasksBuildBase
{
    private BaseSpawner _baseSpawner;
    private BoxStorage _boxStorage;

    private int _basePrice = 5;

    public CreatorTasksBuildBase(BaseSpawner baseSpawner, BoxStorage boxStorage)
    {
        _baseSpawner = baseSpawner;
        _boxStorage = boxStorage;
    }

    public bool TryCreateTask(out ITaskable task, Flag flag)
    {
        if (_boxStorage.TryGetBoxes(_basePrice))
        {
            task = new BuildBaseTask(_baseSpawner, flag);

            return true;
        }

        task = null;

        return false;
    }
}