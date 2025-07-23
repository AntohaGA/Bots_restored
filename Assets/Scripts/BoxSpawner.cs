using System;
using System.Collections;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private PoolBoxes _poolBoxes;
    [SerializeField] private Box _prefabBox;
    [SerializeField] private Map _map;
    [SerializeField] private float _spawnInterval = 0.2f;

    private int _maxAttempts = 10;
    private float _checkRadius = 1f;

    private readonly Collider[] _overlapResults = new Collider[10];

    public event Action<Vector3> BoxCreated;

    private void Awake()
    {
        _poolBoxes.Init(_prefabBox);
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_spawnInterval);

            TrySpawnResource();
        }
    }

    private void TrySpawnResource()
    {
        for (int attempt = 0; attempt < _maxAttempts; attempt++)
        {
            Vector3 spawnPos = _map.GetSpawnPosition();
            int count = Physics.OverlapSphereNonAlloc(spawnPos, _checkRadius, _overlapResults);

            if (count == 0)
            {
                Box box = _poolBoxes.GetInstance();
                Debug.Log("_poolBoxes.ActiveCount" + _poolBoxes.ActiveCount);
                box.Init(spawnPos);
                BoxCreated?.Invoke(spawnPos);

                return;
            }
        }
    }
}