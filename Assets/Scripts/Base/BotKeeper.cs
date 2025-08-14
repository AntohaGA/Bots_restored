using System;
using UnityEngine;

public class BotKeeper : MonoBehaviour
{
    private const int StartCountBots = 0;

    private Vector3 _offsetPositionSpawnBots = new(-5.5f, 1.5f, 10);

    private Bot _botPrefab;
    private BotStateChanger _botStateChanger;

    public int CountBots { get; private set; } = 0;

    private void OnDestroy()
    {
        _botStateChanger?.Clear();
    }

    public void Init()
    {
        _botPrefab = Resources.Load<Bot>("Prefabs/Bot");
        _botStateChanger = new BotStateChanger();

        for (int i = 0; i < StartCountBots; i++)
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

    public void AddNewBot(Bot bot)
    {
        if(bot == null)
        {
            CreateBot(new(0,0,0));
        }
        else
        {
            _botStateChanger.AddNewBot(bot);
        }

         CountBots++;
    }

    private void CreateBot(Vector3 spawnPosition)
    {
        var bot = Instantiate(_botPrefab, spawnPosition, Quaternion.identity);
        bot.Init();
        CountBots++;
        _botStateChanger.AddNewBot(bot);
    }

    internal void Remove(Bot bot)
    {
        _botStateChanger.Remove(bot);
    }
}