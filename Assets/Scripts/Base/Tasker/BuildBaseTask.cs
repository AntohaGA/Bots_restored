using System.Collections;
public class BuildBaseTask : ITaskable
{ 
    private Flag _flag;
    private BaseSpawner _spawner;

    public BuildBaseTask(BaseSpawner baseSpawner, Flag flag)
    {
        _flag = flag;
        _spawner = baseSpawner;
    }

    public IEnumerator Do(Bot bot)
    {
        bot.DropBase();

        yield return bot.GoTo(_flag.transform.position);

        _spawner.SpawnBase(_flag.transform.position, bot);        
    }
}