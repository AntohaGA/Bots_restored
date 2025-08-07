using System;
using System.Collections.Generic;
using UnityEngine;

public class BoxKeeper : MonoBehaviour
{
    [SerializeField] private BoxSpawner _spawner;

    public HashSet<Box> BoxesOnMap { get; private set; }

    private void Awake()
    {
        BoxesOnMap = new HashSet<Box>();
    }

    private void OnEnable()
    {
        _spawner.BoxCreated += AddBox;
    }

    private void OnDisable()
    {
        _spawner.BoxCreated -= AddBox;
    }

    public bool TryFindNearestBox(Vector3 center, out Box box)
    {
        Box closestBox = null;
        float minDistance = float.MaxValue;

        if (BoxesOnMap.Count == 0)
        {
            box = null;

            return false;
        }

        foreach (Box checkedBox in BoxesOnMap)
        {
            float distance = Vector3.Distance(center, checkedBox.transform.position);

            if (distance < minDistance)
            {
                closestBox = checkedBox;
                minDistance = distance;
            }
        }

        if (closestBox == null) 
        {
            Debug.Log("closestBox == null");
        }

        box = closestBox;

        return true;
    }

    public void ReserveBox(Box box)
    {
        BoxesOnMap.Remove(box);
    }

    private void AddBox(Box box)
    {
        if (box == null)
            return;

        BoxesOnMap.Add(box);
    }
}