using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BotAnimator : MonoBehaviour
{
    private static readonly int RunTrigger = Animator.StringToHash("run");
    private static readonly int WaitTrigger = Animator.StringToHash("wait");
    private static readonly int LiftTrigger = Animator.StringToHash("lift");
    private static readonly int RunWithBoxTrigger = Animator.StringToHash("runWithBox");

    private Animator _animator;

    public bool IsLifting { get; private set; } = false;
    public bool IsLifted { get; private set; } = false;

    private void OnLifting() => IsLifting = true;
    private void OnLifted() => IsLifted = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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