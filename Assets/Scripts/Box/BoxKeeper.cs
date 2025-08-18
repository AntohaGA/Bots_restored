using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxSpawner))]
public class BoxKeeper : MonoBehaviour
{
    private BoxSpawner _spawner;
    private List<Box> _boxesOnMap;

    private void Awake()
    {
        _spawner = GetComponent<BoxSpawner>();
        _boxesOnMap = new List<Box>();
    }

    private void OnEnable()
    {
        _spawner.BoxCreated += AddBox;
    }

    private void OnDisable()
    {
        _spawner.BoxCreated -= AddBox;
    }

    public Box GetClosest(Vector3 center)
    {
        Box box = null;
        float minDistance = float.MaxValue;

        if (_boxesOnMap.Count == 0)
        {
            return null;
        }

        foreach (Box checkedBox in _boxesOnMap)
        {
            float distance = (center - checkedBox.transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                box = checkedBox;
                minDistance = distance;
            }
        }

        _boxesOnMap.Remove(box);

        return box;
    }

    private void AddBox(Box box)
    {
        if (box == null)
            return;

        _boxesOnMap.Add(box);
    }
}