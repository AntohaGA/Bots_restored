using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BotRotation : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Transform _transform;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _transform = GetComponent<Transform>();
    }

    public IEnumerator SmoothLookAt(Transform target)
    {
        PrepareForRotation();
        Quaternion endRot = CalculateTargetRotation(target);

        yield return RotateTowards(endRot);

        RestoreAgent();
    }

    private void PrepareForRotation()
    {
        _agent.isStopped = true;
        _agent.updateRotation = false;
    }

    private Quaternion CalculateTargetRotation(Transform target)
    {
        Vector3 direction = (target.position - _transform.position).normalized;

        if (direction == Vector3.zero)
        {
            direction = _transform.forward;
        }

        return Quaternion.LookRotation(direction);
    }

    private IEnumerator RotateTowards(Quaternion targetRotation)
    {
        Quaternion startRotation = _transform.rotation;
        float elapsed = 0f;
        float duration = 0.2f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);

            yield return null;
        }

        _transform.rotation = targetRotation;
    }

    private void RestoreAgent()
    {
        _agent.updateRotation = true;
        _agent.isStopped = false;
    }
}