using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.isStopped = true;
    }

    public void Stop()
    {
        _agent.ResetPath();
        _agent.isStopped = true;
    }

    public IEnumerator MoveTo(Vector3 destination)
    {
        _agent.isStopped = false;
        _agent.SetDestination(destination);

        yield return new WaitUntil(IsAtDestination);

     //   _agent.isStopped = true;
    }

    private bool IsAtDestination()
    {
        float minTargetDistante = 2f;
        float minTargetVelocity = 0.1f;

        if (_agent.pathPending)
        {
            return false;
        }

        if (_agent.remainingDistance <= minTargetDistante)
        {
            if (_agent.hasPath==false || _agent.velocity.sqrMagnitude < minTargetVelocity)
            {
                return true;
            }
        }

        return false;
    }
}