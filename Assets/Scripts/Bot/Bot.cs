using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BotMovement))]
[RequireComponent(typeof(BotAnimator))]
[RequireComponent(typeof(BoxLifter))]
[RequireComponent(typeof(BotRotation))]
public class Bot : MonoBehaviour
{
    private BotMovement _movement;
    private BotRotation _botRotation;
    private BringBoxTask _currentTask;
    private BotAnimator _botAnimator;

    private Coroutine _bringBoxCoroutine;

    public bool IsBusy { get; private set; }
    public Box Box { get; private set; }
    public BoxLifter BoxHandler { get; private set; }

    private void Awake()
    {
        _botAnimator = GetComponent<BotAnimator>();
        _movement = GetComponent<BotMovement>();
        _botRotation = GetComponent<BotRotation>();
        BoxHandler = GetComponent<BoxLifter>();
    }

    public void Init(Vector3 spawnPosition)
    {
        _movement.ResetPosition(spawnPosition);
        MadeFree();
    }

    public void BringBoxTask(Box box, Vector3 positionBase)
    {
        IsBusy = true;
        Box = box;
        _currentTask = new BringBoxTask(Box, positionBase, _botAnimator, _movement, _botRotation, BoxHandler);

        _bringBoxCoroutine = StartCoroutine(RunTask(_currentTask));
    }

    public void MadeFree()
    {
        IsBusy = false;
        _botAnimator.PlayWait();

        if (_bringBoxCoroutine != null)
            StopCoroutine(_bringBoxCoroutine);
    }

    private IEnumerator RunTask(BringBoxTask task)
    {
        yield return task.Run();
    }
}