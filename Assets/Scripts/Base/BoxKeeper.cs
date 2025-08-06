using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxKeeper : MonoBehaviour
{
    [SerializeField] private BoxSpawner _spawner;
    [SerializeField] private float _scanInterval = 0.2f;
    [SerializeField] private PoolBoxes _poolBoxes;
    [SerializeField] private BoxStorage _storage;

    private BoxStorage _boxStorage;
    private WaitForSeconds _delayBetweenScanNewBox;

    public HashSet<Box> BoxesOnMap { get; private set; }

    private void Awake()
    {
        _delayBetweenScanNewBox = new WaitForSeconds(_scanInterval);
    }

    private void OnEnable()
    {
        _spawner.BoxCreated += AddBox;
    }

    private void OnDisable()
    {
        _spawner.BoxCreated -= AddBox;
    }

    public Box FindNearestBox(Vector3 center)
    {
        Box closestBox = null;
        float minDistance = float.MaxValue;

        if (BoxesOnMap.Count == 0)
            return null;

        foreach (Box box in BoxesOnMap)
        {
            float distance = Vector3.Distance(center, box.transform.position);

            if (distance < minDistance)
            {
                closestBox = box;
                minDistance = distance;
            }
        }

        return closestBox;
    }

    private void AddBox(Box box)
    {
        if (box == null)
            return;

        BoxesOnMap.Add(box);
    }
}