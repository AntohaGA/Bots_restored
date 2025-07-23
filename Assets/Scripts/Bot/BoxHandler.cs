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

        Rigidbody rb = _currentBox.GetComponent<Rigidbody>();

        if (rb != null)
            rb.isKinematic = true;

        _currentBox.transform.SetParent(_handHolder);
        _currentBox.transform.localPosition = Vector3.zero;
        _currentBox.transform.localRotation = Quaternion.identity;

        NavMeshObstacle obstacle = _currentBox.GetComponent<NavMeshObstacle>();

        if (obstacle != null)
            obstacle.enabled = false;
    }

    public void DropBox(Base homeBase)
    {
        if (_currentBox == null) 
            return;

        _currentBox.IsTaken = false;
        _currentBox.transform.SetParent(null);

        Rigidbody rb = _currentBox.GetComponent<Rigidbody>();

        if (rb != null) 
            rb.isKinematic = false;

        homeBase.TakeBox(_currentBox);
        _currentBox = null;
    }
}