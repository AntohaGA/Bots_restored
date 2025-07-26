using System.Collections.Generic;
using UnityEngine;

public class BoxStorage : MonoBehaviour
{
    private int _countBasesBoxes = 0;

    public HashSet<Box> FreeBoxes { get; private set; }
    public HashSet<Box> TakenBoxes { get; private set; }

    private void Awake()
    {
        FreeBoxes = new HashSet<Box>();
        TakenBoxes = new HashSet<Box>();
    }

    public void AddBox(Box box)
    {
        if (box == null)
            return;

        FreeBoxes.Add(box);
    }

    public void AcceptBox(Box box)
    {
        if (box == null)
            return;

        FreeBoxes.Remove(box);
        TakenBoxes.Add(box);
    }

    public void RemoveBox(Box box)
    {
        if (box == null)
            return;

        TakenBoxes.Remove(box);
        _countBasesBoxes++;
        Debug.Log(_countBasesBoxes);
    }
}