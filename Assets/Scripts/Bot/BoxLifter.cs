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

        _currentBox.SetRigidBodyKinematic(true);
        _currentBox.transform.SetParent(_handHolder);
        _currentBox.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        _currentBox.SetNavMeshObstacle(false);
    }
}