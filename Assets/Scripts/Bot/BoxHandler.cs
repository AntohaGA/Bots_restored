using UnityEngine;

public class BoxHandler : MonoBehaviour
{
    [SerializeField] private Transform _handHolder;

    private Box _currentBox;

    public bool WithBox { get; private set; } = false;

    private void OnEnable()
    {
        WithBox = false;
    }

    public void LiftBox(Box box)
    {
        _currentBox = box;

        if (_currentBox == null)
            return;

        WithBox = true;
        _currentBox.IsTaken = true;
        _currentBox.SetRigidBodyKinematic(true);
        _currentBox.transform.SetParent(_handHolder);
        _currentBox.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        _currentBox.SetNavMeshObstacle(false);
    }
}