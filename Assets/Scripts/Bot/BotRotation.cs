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

    public IEnumerator SmoothLookAt(Transform target, float duration = 0.5f)
    {
        _agent.isStopped = true;
        _agent.updateRotation = false;

        Quaternion startRot = transform.rotation;
        Vector3 toTarget = (target.position - transform.position).normalized;

        if (toTarget == Vector3.zero)
            toTarget = transform.forward;

        Quaternion endRot = Quaternion.LookRotation(toTarget);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / duration);

            yield return null;
        }

        transform.rotation = endRot;
        _agent.updateRotation = true;
        _agent.isStopped = false;
    }
}