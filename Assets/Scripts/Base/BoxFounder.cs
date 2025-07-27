using System;
using System.Collections;
using UnityEngine;

public class BoxFounder : MonoBehaviour
{
    [SerializeField] private BoxSpawner _spawner;
    [SerializeField] private float _scanInterval = 0.2f;
    [SerializeField] private PoolBoxes _poolBoxes;

    private BoxStorage _boxStorage;
    private WaitForSeconds _delayBetweenScanNewBox;

    public event Action<Box> OfferedClosestBox;

    private void Awake()
    {
        _delayBetweenScanNewBox = new WaitForSeconds(_scanInterval);
        _boxStorage = new BoxStorage();
    }

    private void OnEnable()
    {
        _spawner.BoxCreated += RegisterSpawnedBox;
    }

    private void OnDisable()
    {
        _spawner.BoxCreated -= RegisterSpawnedBox;
    }

    public IEnumerator ScanRoutine()
    {
        while (enabled)
        {
            yield return _delayBetweenScanNewBox;

            FindNearestBox(transform.position);
        }
    }

    public void FindNearestBox(Vector3 center)
    {
        Box closestBox = null;
        float minDistance = float.MaxValue;

        if (_boxStorage.FreeBoxes.Count == 0)
            return;

        foreach (Box box in _boxStorage.FreeBoxes)
        {
            float distance = Vector3.Distance(center, box.transform.position);

            if (distance < minDistance)
            {
                closestBox = box;
                minDistance = distance;
            }
        }

        if (closestBox != null)
        {
            OfferedClosestBox?.Invoke(closestBox);
        }
    }

    public void ReturnBox(Box box)
    {
        if (box == null)
            return;

        box.transform.SetParent(null);
        box.SetRigidBodyKinematic(false);
        _boxStorage.AddBoxOnBase(box);
        _poolBoxes.ReturnInstance(box);
    }

    public void SetBoxReserved(Box box)
    {
        _boxStorage.ReserveBox(box);
    }

    private void RegisterSpawnedBox(Box box)
    {
        _boxStorage.AddBox(box);
    }
}