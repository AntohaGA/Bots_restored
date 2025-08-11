using System.Collections.Generic;
using UnityEngine;

public class BotKeeper : MonoBehaviour
{
    private Vector3 _offsetPositionSpawnBots = new(-5.5f, 1.5f, 10);

    private Bot _botPrefab;

    private List<Bot> _freeBots;
    private List<Bot> _busyBots;
    private List<Bot> _withBoxBots;

    private int _maxBots = 1;

    public void Init()
    {
        _botPrefab = Resources.Load<Bot>("Prefabs/Bot");
        _freeBots = new List<Bot>();
        _busyBots = new List<Bot>();

        for (int i = 0; i < _maxBots; i++)
        {
            CreateBot(transform.TransformPoint(_offsetPositionSpawnBots));
        }
    }

    public bool GetFree(out Bot bot)
    {
        bot = null;

        if (_freeBots.Count > 0)
        {
            bot = _freeBots[0];

            return true;
        }

        return false;
    }

    public void SetBotOnfree(Bot bot)
    {
        _freeBots.Add(bot);
        _busyBots.Remove(bot);
    }

    public void SetBotWithBox(Bot bot)
    {
        _withBoxBots.Add(bot);
        _busyBots.Remove(bot);
    }

    public bool IsBotWithBox(Bot bot)
    {
        if (_busyBots.Contains(bot))
        {
            return true;
        }

        return false;
    }

    private void SetBotOnWork(Bot bot)
    {
        _freeBots.Remove(bot);
        _busyBots.Add(bot);
    }
    private Bot CreateBot(Vector3 spawnPosition)
    {
        Bot bot = Instantiate(_botPrefab, spawnPosition, Quaternion.identity);
        bot.Init();
        _freeBots.Add(bot);

        bot.Worked += SetBotOnWork;
        bot.LiftedBox += SetBotWithBox;
        bot.OnFree += SetBotOnfree;

        return bot;
    }
}