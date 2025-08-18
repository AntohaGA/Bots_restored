using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PoolBoxes))]
public class BoxSpawner : MonoBehaviour
{
    private const int CountCollideOverlap = 10;

    [SerializeField] private Map _map;
    [SerializeField] private float _spawnInterval = 3;

    private PoolBoxes _poolBoxes;
    private Box _prefabBox;

    private int _maxAttempts = 10;
    private float _checkRadius = 1f;

    private readonly Collider[] _overlapResults = new Collider[CountCollideOverlap];

    public event Action<Box> BoxCreated;

    private void Awake()
    {
        _prefabBox = Resources.Load<Box>("Prefabs/Box");

        _poolBoxes = GetComponent<PoolBoxes>();
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
        if (TryFindSpawnPosition(out Vector3 position))
        {
            SpawnBoxAtPosition(position);
        }
    }

    private void SpawnBoxAtPosition(Vector3 position)
    {
        Box box = _poolBoxes.GetInstance();
        box.Init(position);
        box.OnDestroy += ReturnBox;
        BoxCreated?.Invoke(box);
    }

    private void ReturnBox(Box box)
    {
        box.OnDestroy -= ReturnBox;
        _poolBoxes.ReturnInstance(box);
    }

    private bool TryFindSpawnPosition(out Vector3 spawnPosition)
    {
        for (int attempt = 0; attempt < _maxAttempts; attempt++)
        {
            Vector3 position = _map.GetSpawnPosition();
            int count = Physics.OverlapSphereNonAlloc(position, _checkRadius, _overlapResults);

            if (count == 0)
            {
                spawnPosition = position;

                return true;
            }
        }

        spawnPosition = default;

        return false;
    }
}