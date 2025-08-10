using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxLifter))]
[RequireComponent(typeof(Rotation))]
public class Bot : MonoBehaviour
{
    private Box _box;

    public bool IsBusy { get; private set; }
    public BoxLifter BoxLifter { get; private set; }
    public Movement Movement { get; private set; }
    public Rotation Rotation { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Movement = GetComponent<Movement>();
        Rotation = GetComponent<Rotation>();
        BoxLifter = GetComponent<BoxLifter>();
    }

    public void Init()
    {
        MadeFree();
    }

    public void MadeFree()
    {
        Movement.Stop();
        IsBusy = false;
        Animator.PlayWait();
        _box = null;
    }

    public void ReleaseBox()
    {
        _box.Return();
        MadeFree();
        BoxLifter.WithBox = false;
    }

    public void DoJob(ITaskable task)
    {
        task.Do();
    }
}