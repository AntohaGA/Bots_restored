using System.Collections;

public class BuildBaseTask : ITask
{ 
    private Flag _flag;
    private BaseSpawner _baseSpawner;

    public BuildBaseTask(BaseSpawner baseSpawner, Flag flag)
    {
        _flag = flag;
        _baseSpawner = baseSpawner;
    }

    public IEnumerator Do(Bot bot)
    {
        yield return bot.GoTo(_flag.transform.position);

        _baseSpawner.Spawn(_flag.transform.position, bot);
    }
}