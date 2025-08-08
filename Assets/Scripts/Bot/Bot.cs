using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxLifter))]
[RequireComponent(typeof(Rotation))]
public class Bot : MonoBehaviour
{
    private Coroutine _bringBoxCoroutine;

    public bool IsBusy { get; private set; }
    public Box Box { get; private set; }
    public BoxLifter BoxLifter { get; private set; }
    public Movement Movement { get; private set; }
    public Rotation Rotation { get; private set; }
    public Animator Animator { get; private set; }
    public BringBoxTask BringBoxTask { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Movement = GetComponent<Movement>();
        Rotation = GetComponent<Rotation>();
        BoxLifter = GetComponent<BoxLifter>();
    }

    public void Init()
    {
        Movement.ResetPosition();
        MadeFree();
    }

    public void MadeTaskBringBox(Box box, Vector3 positionBase)
    {
        IsBusy = true;
        Box = box;
        BringBoxTask = new BringBoxTask(this, Box, positionBase);

        _bringBoxCoroutine = StartCoroutine(RunTask(BringBoxTask));
    }

    public void MadeFree()
    {
        IsBusy = false;
        Animator.PlayWait();

        if (_bringBoxCoroutine != null)
        {
            StopCoroutine(_bringBoxCoroutine);
            _bringBoxCoroutine = null;
        }
    }

    private IEnumerator RunTask(BringBoxTask task)
    {
        yield return task.Run();
    }
}