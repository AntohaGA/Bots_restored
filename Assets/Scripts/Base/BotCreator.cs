using System.Collections.Generic;
using UnityEngine;

public class BotCreator : MonoBehaviour 
{
    [SerializeField] private Bot _botPrefab;

    private List<Bot> _bots = new ();

    private int _maxBots = 3;

    public void Init(Base homeBase)
    {
        for (int i = 0; i < _maxBots; i++)
        {
            Bot bot = Instantiate(_botPrefab);
            bot.Init(homeBase);
            _bots.Add(bot);
        }
    }

    public Bot GetFreeBot()
    {
        foreach (var bot in _bots)
        {
            if (bot.IsBusy == false)
            {
                return bot;
            }
        }

        return null;
    }

    public void ReturnBot(Bot bot)
    {
        if (bot == null)
            return;

        bot.IsBusy = false;
    }
}