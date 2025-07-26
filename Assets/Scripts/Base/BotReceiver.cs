public class BotReceiver
{
    private PoolBots _poolBots;
    private int _maxBots;

    public BotReceiver(PoolBots poolBots, int maxBots)
    {
        _poolBots = poolBots;
        _maxBots = maxBots;
    }

    public void Init(Bot botPrefab)
    {
        _poolBots.Init(botPrefab);
    }

    public Bot GetBot()
    {
        if (_poolBots.ActiveCount < _maxBots)
        {
            return _poolBots.GetInstance();
        }
        return null;
    }

    public void TakeBot(Bot bot)
    {
        if (bot != null)
        {
            _poolBots.ReturnInstance(bot);
        }
    }
}