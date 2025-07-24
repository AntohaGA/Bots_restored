using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BotRotation : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
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
        Vector3 direction = (target.position - transform.position).normalized;

        if (direction == Vector3.zero)
        {
            direction = transform.forward;
        }

        return Quaternion.LookRotation(direction);
    }

    private IEnumerator RotateTowards(Quaternion targetRotation)
    {
        Quaternion startRotation = transform.rotation;
        float elapsed = 0f;
        float duration = 0.2f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);

            yield return null;
        }

        transform.rotation = targetRotation;
    }

    private void RestoreAgent()
    {
        _agent.updateRotation = true;
        _agent.isStopped = false;
    }
}