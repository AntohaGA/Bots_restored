using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(BotKeeper))]
[RequireComponent(typeof(BoxStorage))]
[RequireComponent(typeof(ManagerTasks))]
[RequireComponent(typeof(BotWithBoxDetector))]
[RequireComponent(typeof(FlagPlacer))]
public class Base : MonoBehaviour
{
    private BoxKeeper _boxKeeper;
    private ClickMapDetector _clickMapDetector;
    private BaseSpawner _baseSpawner;

    private BotKeeper _botKeeper;
    private ManagerTasks _managerTasks;
    private BotWithBoxDetector _botWithBoxDetector;
    private FlagPlacer _flagPlacer;

    public void InitDependencies(BoxKeeper boxKeeper, ClickMapDetector clickMapDetector, BaseSpawner baseSpawner)
    {
        _boxKeeper = boxKeeper;
        _clickMapDetector = clickMapDetector;
        _baseSpawner = baseSpawner;
    }

    public void Initialize()
    {
        _botKeeper = GetComponent<BotKeeper>();
        _botKeeper.Init();
        _botWithBoxDetector = GetComponent<BotWithBoxDetector>();
        _botWithBoxDetector.Init(_botKeeper);
        _managerTasks = GetComponent<ManagerTasks>();
        _managerTasks.Init(_botKeeper, _boxKeeper, _baseSpawner);
        _flagPlacer = GetComponent<FlagPlacer>();
        _flagPlacer.Init(_clickMapDetector);

        StartCoroutine(_managerTasks.DoTasks());
    }
}