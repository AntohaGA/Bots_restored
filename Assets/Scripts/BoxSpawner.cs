using System.Collections;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    private const int CountCollideOverlap = 10;

    [SerializeField] private PoolBoxes _poolBoxes;
    [SerializeField] private Box _prefabBox;
    [SerializeField] private Map _map;
    [SerializeField] private float _spawnInterval = 0.2f;

    private int _maxAttempts = 10;
    private float _checkRadius = 1f;

    private readonly Collider[] _overlapResults = new Collider[CountCollideOverlap];

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

    private void SpawnBoxAtPosition(Vector3 position)
    {
        Box box = _poolBoxes.GetInstance();
        box.Init(position);
    }

    private void TrySpawnResource()
    {
        if (TryFindSpawnPosition(out Vector3 position))
        {
            SpawnBoxAtPosition(position);
        }
    }
}