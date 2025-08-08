using UnityEngine;

[RequireComponent(typeof(UnityEngine.Animator))]
public class Animator : MonoBehaviour
{
    private const string RunTrigger = "run";
    private const string WaitTrigger = "wait";
    private const string LiftTrigger = "lift";
    private const string RunWithBoxTrigger = "runWithBox";

    private UnityEngine.Animator _animator;

    public bool IsLifting { get; private set; } = false;
    public bool IsLifted { get; private set; } = false;

    private void OnLifting() => IsLifting = true;
    private void OnLifted() => IsLifted = true;

    private void Awake()
    {
        _animator = GetComponent<UnityEngine.Animator>();
    }

    public void PlayWait()
    {
        _animator.SetTrigger(WaitTrigger);
    }

    public void PlayRun()
    {
        _animator.SetTrigger(RunTrigger);
    }

    public void PlayLift()
    {
        IsLifting = false;
        IsLifted = false;
        _animator.SetTrigger(LiftTrigger);
    }

    public void PlayRunWithBox()
    {
        _animator.SetTrigger(RunWithBoxTrigger);
    }
}