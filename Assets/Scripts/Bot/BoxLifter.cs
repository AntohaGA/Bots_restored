using UnityEngine;

public class BoxLifter : MonoBehaviour
{
    [SerializeField] private Transform _handHolder;

    private Box _currentBox;

    public void Lift(Box box)
    {
        _currentBox = box;

        if (_currentBox == null)
            return;

        box.Take(_handHolder);
    }
}