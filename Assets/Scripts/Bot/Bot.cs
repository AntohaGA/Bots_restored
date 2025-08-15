using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(BotAnimator))]
[RequireComponent(typeof(BoxLifter))]
[RequireComponent(typeof(Rotation))]
public class Bot : MonoBehaviour
{
    private Movement _movement;
    private Rotation _rotation;
    private BoxLifter _boxLifter;
    private NavMeshAgent _agent;
    private BotAnimator _botAnimator;

    public event Action<Bot> StartedWorking;
    public event Action<Bot> LiftedBox;
    public event Action<Bot> SetFree;
    public event Action<Bot> DropedBase;

    private void Awake()
    {
        _botAnimator = GetComponent<BotAnimator>();
        _movement = GetComponent<Movement>();
        _rotation = GetComponent<Rotation>();
        _boxLifter = GetComponent<BoxLifter>();
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Init()
    {
        _rotation.Init(_agent);
        _movement.Init(_agent, _botAnimator);
        _boxLifter.Init(_botAnimator);
    }

    public IEnumerator GoTo(Vector3 destination)
    {
        _movement.SetHasBox(_boxLifter.WithBox);

        yield return _movement.MoveTo(destination);
    }

    public IEnumerator LiftBox(Box box)
    {
        LookAt(box);

        yield return _boxLifter.Lift(box);

        LiftedBox?.Invoke(this);
    }

    public void DoJob(ITaskable task)
    {
        if (task != null)
        {
            StartedWorking?.Invoke(this);
            StartCoroutine(task.Do(this));
        }
    }

    public void MadeFree()
    {
        _boxLifter.DropBox();
        _movement.Stop();
        SetFree?.Invoke(this);
    }

    public void DropBase()
    {
        DropedBase?.Invoke(this);
    }

    private void LookAt(Box box)
    {
        if (box != null)
            StartCoroutine(_rotation.SmoothLookAt(box.transform));
    }
}