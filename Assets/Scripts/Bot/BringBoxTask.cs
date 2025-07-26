using System.Collections;
using UnityEngine;

public class BringBoxTask
{
    private Box _box;
    private Base _homeBase;
    private BotAnimator _botAnimator;
    private BotMovement _botMovement;
    private BotRotation _botRotation;
    private BoxHandler _boxHandler;

    public BringBoxTask(Box box, Base homeBase, BotAnimator botAnimator, BotMovement botMovement, BotRotation botRotation, BoxHandler boxHandler)
    {
        _box = box;
        _homeBase = homeBase;
        _botAnimator = botAnimator;
        _botMovement = botMovement;
        _botRotation = botRotation;
        _boxHandler = boxHandler;
    }

    public IEnumerator Run()
    {
        yield return _botMovement.MoveTo(_box.SpotForLift);
        yield return _botRotation.SmoothLookAt(_box.transform);
        _botAnimator.PlayLift();
        yield return new WaitUntil(() => _botAnimator.IsLifting);
        _boxHandler.LiftBox(_box);
        yield return new WaitUntil(() => _botAnimator.IsLifted);
        _botAnimator.PlayRunWith();
        _botMovement.GoToPoint(_homeBase.GetPointIn());
    }
}