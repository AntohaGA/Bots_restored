using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BotMovement))]
[RequireComponent(typeof(BotAnimator))]
[RequireComponent(typeof(BoxHandler))]
[RequireComponent(typeof(BotRotation))]
public class Bot : MonoBehaviour
{
    private Base _homeBase;
    private BotMovement _movement;
    private BotRotation _botRotation;
    private BotAnimator _botAnimator;
    private BoxHandler _boxHandler;
    private Coroutine _currentRoutine;

    private bool _isLifted;
    private bool _isLifting;

    public bool IsBusy { get; private set; } = false;
    public Box Box { get; private set; }

    private void Awake()
    {
        _botAnimator = GetComponent<BotAnimator>();
        _movement = GetComponent<BotMovement>();
        _botRotation = GetComponent<BotRotation>();
        _boxHandler = GetComponent<BoxHandler>();

        _botAnimator.OnLiftingEvent += () => _isLifting = true;
        _botAnimator.OnLiftedEvent += () => _isLifted = true;
    }

    public void Init(Base basePoint)
    {
        _movement.ResetPosition(basePoint.GetPointOut());
        _homeBase = basePoint;
    }

    public void BringBox(Box box)
    {
        if (IsBusy)
            return;

        Box = box;
        IsBusy = true;

        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);

        _currentRoutine = StartCoroutine(ProcessBringBox());
    }

    private IEnumerator ProcessBringBox()
    {
        _isLifted = false;
        _isLifting = false;

        yield return _movement.MoveTo(Box.GetSpotForLift());

        yield return _botRotation.SmoothLookAt(Box.transform);

        _botAnimator.PlayLift();

        yield return new WaitUntil(() => _isLifting);

        _boxHandler.LiftBox(Box);

        yield return new WaitUntil(() => _isLifted);

        _botAnimator.PlayRunWith();

        yield return _movement.MoveTo(_homeBase.GetPointIn());

        _boxHandler.DropBox(_homeBase);

        OnProcessCompleted();
    }

    private void OnProcessCompleted()
    {
        IsBusy = false;
        _homeBase.TakeBot(this);
        _currentRoutine = null;
    }
}