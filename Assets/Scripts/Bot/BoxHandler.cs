using UnityEngine;
using UnityEngine.AI;

public class BoxHandler : MonoBehaviour
{
    [SerializeField] private Transform _handHolder;

    private Box _currentBox;

    public void LiftBox(Box box)
    {
        _currentBox = box;

        if (_currentBox == null)
            return;

        _currentBox.IsTaken = true;

        if (_currentBox.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
        }

        _currentBox.transform.SetParent(_handHolder);
        _currentBox.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        if (_currentBox.TryGetComponent<NavMeshObstacle>(out var obstacle))
        {
            obstacle.enabled = false;
        }
    }

    public void DropBox(Base homeBase)
    {
        if (_currentBox == null)
            return;

        _currentBox.IsTaken = false;
        _currentBox.transform.SetParent(null);
        _currentBox.SetRigidBodyKinematic(false);

        homeBase.TakeBox(_currentBox);
        _currentBox = null;
    }
}