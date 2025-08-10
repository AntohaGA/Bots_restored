using UnityEngine;

public class MaderTaskBringBox
{
    private BoxKeeper _boxKeeper;
    private BotKeeper _botKeeper;
    private Vector3 _pointDestination;
    private Transform _transform;

    public MaderTaskBringBox(BotKeeper botKeeper, BoxKeeper boxKeeper, Vector3 pointDestination)
    {
        _botKeeper = botKeeper;
        _boxKeeper = boxKeeper;
        _pointDestination = pointDestination;
    }

    public ITaskable TryGetTask()
    {
        Bot bot;
        Box box;

        if (_botKeeper.TryGetFreeBot(out bot))
        {
            if (_boxKeeper.TryFindNearestBox(_transform.position, out box))
            {
                return new BringBoxTask(bot, box, _pointDestination);
            }
        }

        return null;
    }
}