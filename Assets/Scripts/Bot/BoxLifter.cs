using System.Collections;
using UnityEngine;

public class BoxLifter : MonoBehaviour
{
    [SerializeField] private Transform _handHolder;
    private Box _box;
    private BotAnimator _botAnimator;

    public bool WithBox => _box != null;

    public void Init(BotAnimator botAnimator)
    {
        _botAnimator = botAnimator;
    }

    public IEnumerator Lift(Box box)
    {
        _box = box;

        _botAnimator.PlayLift();
        yield return new WaitUntil(() => _botAnimator.IsLifting);

        box.Take(_handHolder);
        yield return new WaitUntil(() => _botAnimator.IsLifted);
    }

    public void DropBox()
    {
        if (_box != null)
        {
            _box.Return();
            _box = null;
            _botAnimator.PlayWait();
        }
        else
        {
            Debug.LogWarning("Попытка сбросить коробку, но _box равен null");
        }
    }
}