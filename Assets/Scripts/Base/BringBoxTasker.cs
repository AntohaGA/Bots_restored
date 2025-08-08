using UnityEngine;

public class BringBoxTasker : MonoBehaviour
{
    [SerializeField] private Transform _pointIn;

    private BoxKeeper _boxKeeper;
    private BotKeeper _botKeeper;

    public void Init(BoxKeeper boxKeeper, BotKeeper botCreator)
    {
        _boxKeeper = boxKeeper;
        _botKeeper = botCreator;
    }

    public bool TryBringBox()
    {
        if (_botKeeper.TryGetFreeBot(out Bot bot))
        {
            if (_boxKeeper.TryFindNearestBox(transform.position, out Box box))
            {
                bot.MadeTaskBringBox(box, _pointIn.position);

                return true;
            }
        }

        return false;
    }
}