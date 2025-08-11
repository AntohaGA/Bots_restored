using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(BotAnimator))]
[RequireComponent(typeof(BoxLifter))]
[RequireComponent(typeof(Rotation))]
public class Bot : MonoBehaviour
{
    private Box _box;

    private Movement _movement;
    private BotAnimator _animator;
    private Rotation _rotation;
    private BoxLifter _boxLifter;

    public event Action<Bot> StartedWorking;
    public event Action<Bot> LiftedBox;
    public event Action<Bot> OnFree;

    private void Awake()
    {
        _animator = GetComponent<BotAnimator>();
        _movement = GetComponent<Movement>();
        _rotation = GetComponent<Rotation>();
        _boxLifter = GetComponent<BoxLifter>();
    }

    public void Init()
    {
        MadeFree();
    }

    public IEnumerator GoTo(Vector3 destination)
    {
        _animator.PlayRun();

        yield return _movement.MoveTo(destination);
    }

    public IEnumerator GoToWithBox(Vector3 destination)
    {
        _animator.PlayRunWithBox();

        yield return StartCoroutine(_movement.MoveTo(destination));
    }

    public IEnumerator LiftBox(Box box)
    {
        _box = box;
        LookAt();

        _animator.PlayLift();
        yield return new WaitUntil(() => _animator.IsLifting);
        _boxLifter.Lift(_box);
        yield return new WaitUntil(() => _animator.IsLifted);

        LiftedBox?.Invoke(this);
    }

    private void LookAt()
    {
        StartCoroutine(_rotation.SmoothLookAt(_box.transform));
    }

    public void ReleaseBox()
    {
        if (_box != null)
        {
            _box.Return();
            MadeFree();
        }
    }

    public void MadeFree()
    {
        _movement.Stop();
        _animator.PlayWait();
        _box = null;

        OnFree?.Invoke(this);
    }

    public void DoJob(ITaskable task)
    {
        StartedWorking?.Invoke(this);
        StartCoroutine(task.Do(this));
    }
}