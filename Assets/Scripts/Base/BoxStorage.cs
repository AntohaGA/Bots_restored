using UnityEngine;

[RequireComponent(typeof(BotWithBoxDetector))]
public class BoxStorage : MonoBehaviour
{
    private int _countBoxes = 0;

    public bool TryGetBoxes(int count)
    {
        if (_countBoxes >= count)
        {
            _countBoxes -= count;

            return true;
        }

        return false;
    }

    public void AddBoxOnBase()
    {
        _countBoxes++;
    }
}