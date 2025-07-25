using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(Rigidbody))]
public class Box : MonoBehaviour
{
    [SerializeField] private Transform _spotForLift;

    private NavMeshObstacle _obstacle;
    private Rigidbody _rigidbody;

    public Vector3 SpotForLift => _spotForLift.position;

    private void Awake()
    {
        _obstacle = GetComponent<NavMeshObstacle>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 position)
    {
        _obstacle.enabled = true;
        transform.SetPositionAndRotation(position, Quaternion.identity);
    }

    public void SetRigidBodyKinematic(bool isKinematic)
    {
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = isKinematic;
        }
    }

    public void SetNavMeshObstacle(bool isEnabled)
    {
        if (_obstacle != null)
        {
            _obstacle.enabled = isEnabled;
        }
    }
}