using System.Collections.Generic;

public class BoxStorage
{
    private int _countBasesBoxes = 0;

    public HashSet<Box> FreeBoxes { get; private set; }
    public HashSet<Box> ReservedBoxes { get; private set; }

    public BoxStorage()
    {
        FreeBoxes = new HashSet<Box>();
        ReservedBoxes = new HashSet<Box>();
    }

    public void AddBox(Box box)
    {
        if (box == null)
            return;

        FreeBoxes.Add(box);
    }

    public void ReserveBox(Box box)
    {
        if (box == null)
            return;

        FreeBoxes.Remove(box);
        ReservedBoxes.Add(box);
    }

    public void AddBoxOnBase(Box box)
    {
        if (box == null)
            return;

        ReservedBoxes.Remove(box);
        _countBasesBoxes++;
    }
}