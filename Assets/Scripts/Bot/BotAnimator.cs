using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BotAnimator : MonoBehaviour
{
    private const string LiftTrigger = "lift";
    private const string RunWithBoxTrigger = "runWithBox";

    private Animator _animator;

    public event Action OnLiftingEvent;
    public event Action OnLiftedEvent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayLift()
    {
        _animator.SetTrigger(LiftTrigger);
    }

    public void PlayRunWith()
    {
        _animator.SetTrigger(RunWithBoxTrigger);
    }

    private void OnLifting()
    {
        OnLiftingEvent?.Invoke();
    }

    private void OnLifted()
    {
        OnLiftedEvent?.Invoke();
    }
}