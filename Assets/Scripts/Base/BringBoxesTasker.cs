using System.Collections;
using UnityEngine;

public class BringBoxesTasker : MonoBehaviour 
{
    [SerializeField] private Transform _pointIn;

    private BoxKeeper _boxKeeper;
    private BotCreator _botCreator;
    private BoxStorage _boxStorage;

    private Coroutine _bringerBoxes;

    public void Init(BoxKeeper boxKeeper, BotCreator botCreator, BoxStorage boxStorage)
    {
        _boxKeeper = boxKeeper;
        _botCreator = botCreator;
        _boxStorage = boxStorage;
    }

    public void Do()
    {
        _bringerBoxes = StartCoroutine(AssignBotsRoutine());
    }

    private IEnumerator AssignBotsRoutine()
    {
        while (enabled)
        {
            if (_boxKeeper.TryFindNearestBox(transform.position, out Box box)
                && _botCreator.TryGetFreeBot(out Bot bot))
            {
                _boxKeeper.ReserveBox(box);
                _boxStorage.ReserveBox(box);
                bot.BringBox(box, _pointIn.position);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}