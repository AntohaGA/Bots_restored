using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BotAnimator : MonoBehaviour
{
    private const string LiftTrigger = "lift";
    private const string RunWithBoxTrigger = "runWithBox";

    private Animator _animator;

    public bool IsLifting { get ;private set; }
    public bool IsLifted { get ;private set; }

    private void OnLifting() => IsLifting = true;
    private void OnLifted() => IsLifted = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        IsLifting = false;
        IsLifted =false;
    }

    public void PlayLift()
    {
        _animator.SetTrigger(LiftTrigger);
    }

    public void PlayRunWith()
    {
        _animator.SetTrigger(RunWithBoxTrigger);
    }
}