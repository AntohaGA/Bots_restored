using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScanner : MonoBehaviour
{
    [SerializeField] private BoxSpawner _spawner;
    [SerializeField] private PoolBoxes _poolBoxes;
    [SerializeField] private float _scanInterval = 0.2f;

    private Dictionary<Box, BoxState> _boxBusyStates = new Dictionary<Box, BoxState>();

    public event Action<Box> OfferedClosestBox;

    private void OnEnable()
    {
        _spawner.BoxCreated += RegisterBox;
    }

    private void OnDisable()
    {
        _spawner.BoxCreated -= RegisterBox;
    }

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
            return;

        foreach (Box box in _poolBoxes)
        {
            if (_boxBusyStates.TryGetValue(box, out var state) && state == BoxState.Free)
            {
                float distant = Vector3.Distance(center, box.transform.position);

                if (distant < minDistance)
                {
                    closestBox = box;
                    minDistance = distant;
                }
            }
        }

        OfferedClosestBox?.Invoke(closestBox);
    }

    public void ReturnBox(Box box)
    {
        if (box == null || _poolBoxes == null)
            return;

        _boxBusyStates.Remove(box);
        _poolBoxes.ReturnInstance(box);
    }

    public void AcceptBox(Box box)
    {
        _boxBusyStates[box] = BoxState.Taken;
    }

    private void RegisterBox(Box box)
    {
        _boxBusyStates.Add(box, BoxState.Free);
    }
}