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

    public event Action<Bot> Worked;
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

    public void GoTo(Vector3 destination)
    {
        _animator.PlayRun();
        StartCoroutine(_movement.MoveTo(destination));
    }

    public void GoToWithBox(Vector3 destination)
    {
        _animator.PlayRunWithBox();
        StartCoroutine(_movement.MoveTo(destination));
    }

    public void LiftBox(Box box)
    {
        _box = box;
        StartCoroutine(LiftBoxCoroutine());

        LiftedBox?.Invoke(this);
    }

    private IEnumerator LiftBoxCoroutine()
    {
        LookAt();

        _animator.PlayLift();
        yield return new WaitUntil(() => _animator.IsLifting);
        _boxLifter.Lift(_box);
        yield return new WaitUntil(() => _animator.IsLifted);
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
        Worked?.Invoke(this);

        Debug.Log("в боте метод doJob" + task);
        StartCoroutine(task.Do(this));
    }
}