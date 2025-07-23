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

   // public event Action<Vector3> BoxCreated;

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

    private bool TryFindSpawnPosition(out Vector3 spawnPos)
    {
        for (int attempt = 0; attempt < _maxAttempts; attempt++)
        {
            Vector3 pos = _map.GetSpawnPosition();
            int count = Physics.OverlapSphereNonAlloc(pos, _checkRadius, _overlapResults);

            if (count == 0)
            {
                spawnPos = pos;
                return true;
            }
        }

        spawnPos = default;
        return false;
    }

    private void SpawnBoxAtPosition(Vector3 position)
    {
        Box box = _poolBoxes.GetInstance();
        box.Init(position);
     //   BoxCreated?.Invoke(position);
    }

    private void TrySpawnResource()
    {
        if (TryFindSpawnPosition(out Vector3 position))
        {
            SpawnBoxAtPosition(position);
        }
    }
}