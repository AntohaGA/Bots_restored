using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(Rigidbody))]
public class Box : MonoBehaviour
{
    [SerializeField] private Transform _spotForLift;
    private NavMeshObstacle _obstacle;

    public bool IsTaken { get; set; }

    private void Awake()
    {
        _obstacle = GetComponent<NavMeshObstacle>();
    }

    public void Init(Vector3 position)
    {
        _obstacle.enabled = true;
        transform.SetPositionAndRotation(position, Quaternion.identity);
        IsTaken = false;
    }

    public Vector3 GetSpotForLift()
    {
        return _spotForLift.position;
    }
}