using System;
using UnityEngine;

public class FlagPlacer : MonoBehaviour
{
    private Flag _flagPrefab;
    private Flag _flag;

    public event Action<Flag> FlagPlased;

    private void Awake()
    {
        _flagPrefab = Resources.Load<Flag>("Prefabs/Flag");
        _flag = Instantiate(_flagPrefab, transform.position, Quaternion.identity);
    }

    public Transform GetFlagTransform()
    {
        return _flag.transform;
    }

    public void ReturnFlag()
    {
        FlagPlased(_flag);
    }
}