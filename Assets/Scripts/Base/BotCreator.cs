using System.Collections.Generic;
using UnityEngine;

public class BotCreator : MonoBehaviour
{
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Transform _botTransform;

    private List<Bot> _bots = new();

    private int _maxBots = 3;

    public bool TryGetFreeBot(out Bot bot)
    {
        foreach (var possibleBot in _bots)
        {
            if (possibleBot.IsBusy == false)
            {
                bot = possibleBot;

                return true;
            }
        }

        if (_bots.Count < _maxBots)
        {
            bot = CreateNewBot(_botTransform.position);

            return true;
        }

        bot = null;

        return false;
    }

    private Bot CreateNewBot(Vector3 spawnPosition)
    {
        Bot bot = Instantiate(_botPrefab);
        bot.Init(spawnPosition);
        _bots.Add(bot);

        return bot;
    }
}