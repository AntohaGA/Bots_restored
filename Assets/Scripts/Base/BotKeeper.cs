using System.Collections.Generic;
using UnityEngine;

public class BotKeeper : MonoBehaviour
{
    private Vector3 _offsetPositionSpawnBots = new(-5.5f, 1.5f, 10);

    private Bot _botPrefab;

    private List<Bot> _freeBots;
    private List<Bot> _busyBots;
    private List<Bot> _withBoxBots;

    private int _maxBots = 3;

    private void OnDestroy()
    {
        UnsubscribeAllBots();
    }

    private void UnsubscribeAllBots()
    {
        foreach (var bot in _freeBots)
        {
            UnsubscribeBot(bot);
        }

        foreach (var bot in _busyBots)
        {
            UnsubscribeBot(bot);
        }

        foreach (var bot in _withBoxBots)
        {
            UnsubscribeBot(bot);
        }
    }

    private void UnsubscribeBot(Bot bot)
    {
        if (bot == null)
            return;

        bot.StartedWorking -= SetBotOnWork;
        bot.LiftedBox -= SetBotWithBox;
        bot.SetFree -= SetBotOnfree;
    }

    public void Init()
    {
        _botPrefab = Resources.Load<Bot>("Prefabs/Bot");
        _freeBots = new List<Bot>();
        _busyBots = new List<Bot>();
        _withBoxBots = new List<Bot>();

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

    private void SetBotOnfree(Bot bot)
    {
        _freeBots.Add(bot);
        _withBoxBots.Remove(bot);
    }

    private void SetBotWithBox(Bot bot)
    {
        _withBoxBots.Add(bot);
        _busyBots.Remove(bot);
    }

    public bool IsBotWithBox(Bot bot)
    {
        if (_withBoxBots.Contains(bot))
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

        bot.StartedWorking += SetBotOnWork;
        bot.LiftedBox += SetBotWithBox;
        bot.SetFree += SetBotOnfree;

        return bot;
    }
}