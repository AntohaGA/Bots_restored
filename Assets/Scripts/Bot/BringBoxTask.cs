using System.Collections;
using UnityEngine;

public class BringBoxTask
{
    private readonly Box _box;
    private readonly Base _homeBase;
    private bool _isLifting;
    private bool _isLifted;

    private BotAnimator _botAnimator;
    private BotMovement _botMovement;
    private BotRotation _botRotation;
    private BoxHandler _boxHandler;

    private void OnLifting() => _isLifting = true;
    private void OnLifted() => _isLifted = true;

    public BringBoxTask(Box box, Base homeBase, BotAnimator botAnimator, BotMovement botMovement, BotRotation botRotation, BoxHandler boxHandler)
    {
        _box = box;
        _homeBase = homeBase;
        _botAnimator = botAnimator;
        _botMovement = botMovement;
        _botRotation = botRotation;
        _boxHandler = boxHandler;

        _botAnimator.OnLiftingEvent += OnLifting;
        _botAnimator.OnLiftedEvent += OnLifted;
    }

    public IEnumerator Run()
    {
        _isLifting = false;
        _isLifted = false;

        yield return _botMovement.MoveTo(_box.GetSpotForLift());
        yield return _botRotation.SmoothLookAt(_box.transform);

        _botAnimator.PlayLift();
        yield return new WaitUntil(() => _isLifting);

        _boxHandler.LiftBox(_box);
        yield return new WaitUntil(() => _isLifted);

        _botAnimator.PlayRunWith();
        yield return _botMovement.MoveTo(_homeBase.GetPointIn());

        _boxHandler.DropBox(_homeBase);
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        _botAnimator.OnLiftingEvent -= OnLifting;
        _botAnimator.OnLiftedEvent -= OnLifted;
    }
}