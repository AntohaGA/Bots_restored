using System;
using System.Collections;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private PoolBoxes _poolBoxes;
    [SerializeField] private float _scanInterval;

    public event Action<Box> OnBoxFound;

    public IEnumerator ScanRoutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_scanInterval);

            FindNearestBox(transform.position);
        }
    }

    public void FindNearestBox(Vector3 center)
    {
        Box closestBox = null;
        float minDistance = float.MaxValue;

        if (_poolBoxes == null)
        {
            OnBoxFound?.Invoke(null);

            return;
        }

        foreach (Box box in _poolBoxes)
        {
            if (!box.IsTaken)
            {
                float distant = Vector3.Distance(center, box.transform.position);

                if (distant < minDistance)
                {
                    closestBox = box;
                    minDistance = distant;
                }
            }
        }

        OnBoxFound?.Invoke(closestBox);
    }

    public void ReturnBox(Box box)
    {
        if (box == null || _poolBoxes == null)
            return;

        _poolBoxes.ReturnInstance(box);
    }
}