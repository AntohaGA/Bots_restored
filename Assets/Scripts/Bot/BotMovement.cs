using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.isStopped = true;
    }

    public void ResetPosition(Vector3 position)
    {
        _agent.Warp(position);
        _agent.ResetPath();
        _agent.isStopped = true;
    }

    public IEnumerator MoveTo(Vector3 destination)
    {
        float minTargetDistante = 1.5f;
        float minTargetVelocity = 0.1f;

        _agent.isStopped = false;
        _agent.SetDestination(destination);

        while (_agent.pathPending || _agent.remainingDistance > minTargetDistante || _agent.velocity.sqrMagnitude > minTargetVelocity)
        {
            yield return null;
        }

        _agent.isStopped = true;
    }
}