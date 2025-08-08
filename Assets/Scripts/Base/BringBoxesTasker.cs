using UnityEngine;

public class BringBoxesTasker : MonoBehaviour
{
    [SerializeField] private Transform _pointIn;

    private BoxKeeper _boxKeeper;
    private BotCreator _botCreator;

    public void Init(BoxKeeper boxKeeper, BotCreator botCreator)
    {
        _boxKeeper = boxKeeper;
        _botCreator = botCreator;
    }

    public bool TryBringBox()
    {
        if (_botCreator.TryGetFreeBot(out Bot bot))
        {
            if (_boxKeeper.TryFindNearestBox(transform.position, out Box box))
            {
                bot.BringBoxTask(box, _pointIn.position);

                return true;
            }
        }

        return false;
    }
}