using System.Collections;
using UnityEngine;

public class BringBoxTask
{
    private Bot _bot;
    private Box _box;
    private Vector3 _pointDestination;

    public BringBoxTask(Bot bot, Box box, Vector3 pointDestination)
    {
        _bot = bot;
        _box = box;
        _pointDestination = pointDestination;
    }

    public IEnumerator Run()
    {
        _bot.Animator.PlayRun();
        yield return _bot.Movement.MoveTo(_box.SpotForLift);
        yield return _bot.Rotation.SmoothLookAt(_box.transform);

        _bot.Animator.PlayLift();
        yield return new WaitUntil(() => _bot.Animator.IsLifting);
        _bot.BoxLifter.Lift(_box);
        yield return new WaitUntil(() => _bot.Animator.IsLifted);

        _bot.Animator.PlayRunWithBox();
        yield return _bot.Movement.MoveTo(_pointDestination);
    }
}