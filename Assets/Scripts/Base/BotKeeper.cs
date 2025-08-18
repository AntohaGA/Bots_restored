using UnityEngine;

public class BotKeeper : MonoBehaviour
{
    private const int BotPrice = 3;

    private Vector3 _offsetPositionSpawnBots = new(-5.5f, 1.5f, 10);

    private Bot _botPrefab;
    private BotStateChanger _botStateChanger;
    private BoxStorage _boxStorage;

    public int CountBots => _botStateChanger.GetCountBots();

    private void OnDestroy()
    {
        _botStateChanger?.Clear();
    }

    public void Init(Bot bot, BoxStorage boxStorage)
    {
        _botPrefab = Resources.Load<Bot>("Prefabs/Bot");
        _botStateChanger = new BotStateChanger();
        _boxStorage = boxStorage;
        _botStateChanger.AddNewBot(bot);
    }

    public bool GetFree(out Bot bot)
    {
        return _botStateChanger.TakeFreeBot(out bot);
    }

    public void SetFree(Bot bot)
    {
        _botStateChanger.BotSetFree(bot);
    }

    public bool TryCreateNewBot()
    {
        if (_boxStorage.TryGetBoxes(BotPrice))
        {
            CreateBot(transform.TransformPoint(_offsetPositionSpawnBots));

            return true;
        }

        return false;
    }

    public void RemoveBot(Bot bot)
    {
        _botStateChanger.RemoveFromBase(bot);
    }

    public bool IsOurBusyBot(Bot bot)
    {
        return _botStateChanger.CheckBusyBot(bot);
    }

    private void CreateBot(Vector3 spawnPosition)
    {
        var bot = Instantiate(_botPrefab, spawnPosition, Quaternion.identity);
        bot.Init();
        _botStateChanger.AddNewBot(bot);
    }
}