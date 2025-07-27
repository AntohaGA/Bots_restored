using System.Collections.Generic;
using UnityEngine;

public class BotCreator : MonoBehaviour
{
    [SerializeField] private Bot _botPrefab;

    private List<Bot> _bots = new();

    private int _maxBots = 3;

    public Bot TryGetFreeBot(Base homeBase)
    {
        foreach (var bot in _bots)
        {
            if (bot.IsBusy == false)
            {
                return bot;
            }
        }

        if (_bots.Count < _maxBots)
        {
             return CreateNewBot(homeBase);
        }

        return null;
    }

    private Bot CreateNewBot(Base homeBase)
    {
        Bot bot = Instantiate(_botPrefab);
        bot.Init(homeBase);
        _bots.Add(bot);

        return bot;
    }
}