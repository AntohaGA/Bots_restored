using System.Collections.Generic;

public class BotStateChanger
{
    public List<Bot> _freeBots = new();
    public List<Bot> _busyBots = new();

    public void AddNewBot(Bot bot)
    {
        if (bot == null)
            return;

        _freeBots.Add(bot);
    }

    public int GetCountBots()
    {
        return _freeBots.Count + _busyBots.Count;
    }

    public bool TakeFreeBot(out Bot bot)
    {
        bot = null;

        if (_freeBots.Count > 0)
        {
            bot = _freeBots[0];
            _freeBots.RemoveAt(0);
            _busyBots.Add(bot);

            return true;
        }

        return false;
    }

    public void Clear()
    {
        _freeBots.Clear();
        _busyBots.Clear();
    }

    public void RemoveFromBase(Bot bot)
    {
        if (_freeBots.Contains(bot))
        {
            _freeBots.Remove(bot);
        }

        if (_busyBots.Contains(bot))
        {
            _busyBots.Remove(bot);
        }
    }

    public void BotSetFree(Bot bot)
    {
        if (_freeBots.Contains(bot) == false)
            _freeBots.Add(bot);

        _busyBots.Remove(bot);
    }

    public bool CheckBusyBot(Bot bot)
    {
        return _busyBots.Contains(bot);
    }
}