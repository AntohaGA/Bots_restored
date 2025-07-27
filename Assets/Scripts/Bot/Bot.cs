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
    private BringBoxTask _currentTask;
    private BotAnimator _botAnimator;

    public bool IsBusy { get; private set; }
    public Box Box { get; private set; }
    public BoxHandler BoxHandler { get; private  set; }

    private void Awake()
    {
        _botAnimator = GetComponent<BotAnimator>();
        _movement = GetComponent<BotMovement>();
        _botRotation = GetComponent<BotRotation>();
        BoxHandler = GetComponent<BoxHandler>();
    }

    public void Init(Base basePoint)
    {
        _movement.ResetPosition(basePoint.GetPointOut());
        SetFree();
        _homeBase = basePoint;
    }

    public void BringBox(Box box)
    {
        IsBusy = true;
        Box = box;
        _currentTask = new BringBoxTask(Box, _homeBase, _botAnimator, _movement, _botRotation, BoxHandler);
        StartCoroutine(RunTask(_currentTask));
    }

    public void SetFree()
    {
        IsBusy = false;
        _botAnimator.PlayWait();
    }

    private IEnumerator RunTask(BringBoxTask task)
    {
        yield return task.Run();
    }
}