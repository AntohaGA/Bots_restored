using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private BotAnimator _botAnimator;

    public void Init(NavMeshAgent agent, BotAnimator botAnimator)
    {
        _agent = agent;
        _agent.isStopped = true;
        _botAnimator = botAnimator;
    }

    public void Stop()
    {
        _agent.ResetPath();
        _agent.isStopped = true;
        _botAnimator.PlayWait();
    }

    public IEnumerator MoveTo(Vector3 destination, bool HasBox)
    {
        _agent.isStopped = false;

        if (HasBox)
            _botAnimator.PlayRunWithBox();
        else
            _botAnimator.PlayRun();

        _agent.SetDestination(destination);
        yield return new WaitUntil(IsAtDestination);
        Stop();
    }

    private bool IsAtDestination()
    {
        float minTargetDistance = 2f;
        float minTargetVelocity = 0.1f;

        if (_agent.pathPending)
            return false;

        if (_agent.remainingDistance <= minTargetDistance)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude < minTargetVelocity)
                return true;
        }

        return false;
    }
}