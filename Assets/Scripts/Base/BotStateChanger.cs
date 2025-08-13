using System.Collections.Generic;

public class BotStateChanger
{
    private List<Bot> _freeBots = new();
    private List<Bot> _busyBots = new();
    private List<Bot> _withBoxBots = new();

    public void AddNewBot(Bot bot)
    {
        if (bot == null)
            return;

        _freeBots.Add(bot);
        SubscribeBotEvents(bot);
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

    public bool IsBotWithBox(Bot bot)
    {
        return (bot != null) && (_withBoxBots.Contains(bot));
    }

    public void Clear()
    {
        UnsubscribeAllBots();
        _freeBots.Clear();
        _busyBots.Clear();
        _withBoxBots.Clear();
    }

    private void SubscribeBotEvents(Bot bot)
    {
        bot.StartedWorking += OnBotStartedWorking;
        bot.LiftedBox += OnBotLiftedBox;
        bot.SetFree += OnBotSetFree;
    }

    private void UnsubscribeBot(Bot bot)
    {
        if (bot == null)
            return;

        bot.StartedWorking -= OnBotStartedWorking;
        bot.LiftedBox -= OnBotLiftedBox;
        bot.SetFree -= OnBotSetFree;
    }

    private void UnsubscribeAllBots()
    {
        foreach (var bot in _freeBots)
            UnsubscribeBot(bot);

        foreach (var bot in _busyBots)
            UnsubscribeBot(bot);

        foreach (var bot in _withBoxBots)
            UnsubscribeBot(bot);
    }

    private void OnBotStartedWorking(Bot bot)
    {
        _freeBots.Remove(bot);

        if (_busyBots.Contains(bot) == false)
            _busyBots.Add(bot);
    }

    private void OnBotLiftedBox(Bot bot)
    {
        _busyBots.Remove(bot);

        if (_withBoxBots.Contains(bot) == false)
            _withBoxBots.Add(bot);
    }

    private void OnBotSetFree(Bot bot)
    {
        if (_freeBots.Contains(bot) == false)
            _freeBots.Add(bot);

        _withBoxBots.Remove(bot);
    }
}