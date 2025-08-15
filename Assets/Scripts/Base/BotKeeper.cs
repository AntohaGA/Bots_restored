using UnityEngine;

public class BotKeeper : MonoBehaviour
{
    private const int FirstBaseCountBots = 1;
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

        if (bot != null)
        {
            _botStateChanger.AddNewBot(bot);
        }
        else
        {
            for (int i = 0; i < FirstBaseCountBots; i++)
            {
                CreateBot(transform.TransformPoint(_offsetPositionSpawnBots));
            }
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

    public bool TryCreateNewBot()
    {
        if (_boxStorage.TryGetBoxes(BotPrice))
        {
            CreateBot(transform.TransformPoint(_offsetPositionSpawnBots));

            return true;
        }

        return false;
    }

    private void CreateBot(Vector3 spawnPosition)
    {
        var bot = Instantiate(_botPrefab, spawnPosition, Quaternion.identity);
        bot.Init();
        _botStateChanger.AddNewBot(bot);
    }
}