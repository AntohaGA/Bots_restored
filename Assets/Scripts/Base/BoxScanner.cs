using System;
using System.Collections;
using UnityEngine;

public class BoxScanner : MonoBehaviour
{
    [SerializeField] private BoxSpawner _spawner;
    [SerializeField] private PoolBoxes _poolBoxes;
    [SerializeField] private float _scanInterval = 0.2f;
    [SerializeField] private BoxStorage _boxStorage;

    private WaitForSeconds _waitForSeconds;
    public event Action<Box> OfferedClosestBox;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_scanInterval);
    }

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
            yield return _waitForSeconds;

            FindNearestBox(transform.position);
        }
    }

    public void FindNearestBox(Vector3 center)
    {
        Box closestBox = null;
        float minDistance = float.MaxValue;

        if (_poolBoxes == null)
            return;

        foreach (Box box in _boxStorage.FreeBoxes)
        {
            if (box == null)
                continue;

            float distance = Vector3.Distance(center, box.transform.position);

            if (distance < minDistance)
            {
                closestBox = box;
                minDistance = distance;
            }
        }

        OfferedClosestBox?.Invoke(closestBox);
    }

    public void ReturnBox(Box box)
    {
        if (box == null || _poolBoxes == null)
            return;

        _boxStorage.RemoveBox(box);
        _poolBoxes.ReturnInstance(box);
    }

    public void AcceptBox(Box box)
    {
        _boxStorage.AcceptBox(box);
    }

    private void RegisterBox(Box box)
    {
        _boxStorage.AddBox(box);
    }
}