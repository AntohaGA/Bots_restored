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
    private BringBoxTask _currentTask;

    public bool IsBusy { get; private set; } = false;
    public Box Box { get; private set; }
    public BoxHandler BoxHandler { get ; private set; }

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
        _homeBase = basePoint;
    }

    public void BringBox(Box box)
    {
        if (IsBusy)
            return;

        IsBusy = true;
        Box = box;
        _currentTask = new BringBoxTask(Box, _homeBase, _botAnimator, _movement, _botRotation, BoxHandler);
        StartCoroutine(RunTask(_currentTask));
    }

    private IEnumerator RunTask(BringBoxTask task)
    {
        yield return task.Run();

        IsBusy = false;
    }
}