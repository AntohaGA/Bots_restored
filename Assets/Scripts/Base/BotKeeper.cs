using System.Collections.Generic;
using UnityEngine;

public class BotKeeper : MonoBehaviour
{
    [SerializeField] private Transform _botSpawn;

    private Bot _botPrefab;
    private List<Bot> _bots = new();

    public int MaxBots { get; private set; } = 10;

    private void Awake()
    {
        _botPrefab = Resources.Load<Bot>("Prefabs/Bot");
    }

    public void Init(List<Bot> bots)
    {
        _bots = bots;
    }

    public void IncreaseCountBots(int maxBots)
    {
        MaxBots = maxBots;
    }

    public bool TryGetBotForBilding(out Bot bot)
    {
        if (_bots.Count <= 1)
        {
            bot = null;

            return false;
        }
        else
        {
            bot = TryFindFreeBot();

            return bot != null;
        }
    }

    public bool TryGetFreeBot(out Bot bot)
    {
        bot = TryFindFreeBot();

        if (bot != null)
            return true;

        if (_bots.Count < MaxBots)
        {
            bot = CreateNewBot(_botSpawn.position);

            return true;
        }

        bot = null;

        return false;
    }

    private Bot TryFindFreeBot()
    {
        foreach (var possibleBot in _bots)
        {
            if (possibleBot.IsBusy == false)
                return possibleBot;
        }

        return null;
    }

    private Bot CreateNewBot(Vector3 spawnPosition)
    {
        Bot bot = Instantiate(_botPrefab, spawnPosition, Quaternion.identity);
        bot.Init();
        _bots.Add(bot);

        return bot;
    }
}