using System.Collections.Generic;
using UnityEngine;

public class BoxKeeper : MonoBehaviour
{
    [SerializeField] private BoxSpawner _spawner;

    public Queue<Box> BoxesOnMap { get; private set; }

    private void Awake()
    {
        BoxesOnMap = new Queue<Box>();
    }

    private void OnEnable()
    {
        _spawner.BoxCreated += AddBox;
    }

    private void OnDisable()
    {
        _spawner.BoxCreated -= AddBox;
    }

    public Box GetBox()
    {
        Box box = null;
        /* float minDistance = float.MaxValue;
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
         }*/

        if (BoxesOnMap.Count > 0)
        {
            box = BoxesOnMap.Dequeue();
        }

        return box;
    }

    private void AddBox(Box box)
    {
        if (box == null)
            return;

        BoxesOnMap.Enqueue(box);
    }
}