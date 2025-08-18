using UnityEngine;

public class InitFirstBase : MonoBehaviour
{
    [SerializeField] private Base _firstBase;
    [SerializeField] private Bot _firstBot;
    [SerializeField] private BoxKeeper _boxKeeper;
    [SerializeField] private BaseSpawner _baseSpawner;

    private void Start()
    {
        _firstBase.InitDependencies(_boxKeeper, _baseSpawner);
        InitialisateFirstBase();
    }

    private void InitialisateFirstBase()
    {
        _firstBot.Init();
        _firstBase.Initialize(_firstBot);
    }
}