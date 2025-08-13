using UnityEngine;

public class BotKeeper : MonoBehaviour
{
    private const int StartCountBots = 1;

    private Vector3 _offsetPositionSpawnBots = new(-5.5f, 1.5f, 10);

    private Bot _botPrefab;
    private BotStateChanger _botStateChanger;

    public int CountBots { get; private set; } = StartCountBots;

    private void OnDestroy()
    {
        _botStateChanger?.Clear();
    }

    public void Init()
    {
        _botPrefab = Resources.Load<Bot>("Prefabs/Bot");
        _botStateChanger = new BotStateChanger();

        for (int i = 0; i < CountBots; i++)
        {
            CreateBot(transform.TransformPoint(_offsetPositionSpawnBots));
        }
    }

    public bool GetFree(out Bot bot)
    {
        return _botStateChanger.GetFree(out bot);
    }

    public bool IsBotWithBox(Bot bot)
    {
        return _botStateChanger.IsBotWithBox(bot);
    }

    public void CreateNewBot()
    {
        CreateBot(transform.TransformPoint(_offsetPositionSpawnBots));
    }

    private void CreateBot(Vector3 spawnPosition)
    {
        var bot = Instantiate(_botPrefab, spawnPosition, Quaternion.identity);
        bot.Init();
        _botStateChanger.AddNewBot(bot);
    }
}