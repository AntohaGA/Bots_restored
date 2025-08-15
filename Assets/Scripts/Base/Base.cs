using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(BotKeeper))]
[RequireComponent(typeof(BoxStorage))]
[RequireComponent(typeof(QueueTasks))]
[RequireComponent(typeof(BotWithBoxDetector))]
[RequireComponent(typeof(FlagPlacer))]
public class Base : MonoBehaviour
{
    private BoxKeeper _boxKeeper;
    private ClickMapDetector _clickMapDetector;
    private BaseSpawner _baseSpawner;

    private BotKeeper _botKeeper;
    private QueueTasks _managerTasks;
    private BotWithBoxDetector _botWithBoxDetector;
    private FlagPlacer _flagPlacer;
    private BoxStorage _boxStorage;

    public void InitDependencies(BoxKeeper boxKeeper, ClickMapDetector clickMapDetector, BaseSpawner baseSpawner)
    {
        _boxKeeper = boxKeeper;
        _clickMapDetector = clickMapDetector;
        _baseSpawner = baseSpawner;
    }

    public void Initialize(Bot bot)
    {
        _boxStorage = GetComponent<BoxStorage>();
        _botKeeper = GetComponent<BotKeeper>();
        _botKeeper.Init(bot, _boxStorage);

        _botWithBoxDetector = GetComponent<BotWithBoxDetector>();
        _botWithBoxDetector.Init(_botKeeper);
        _managerTasks = GetComponent<QueueTasks>();
        _managerTasks.Init(_botKeeper, _boxKeeper, _baseSpawner);
        _flagPlacer = GetComponent<FlagPlacer>();
        _flagPlacer.Init(_clickMapDetector);

        StartCoroutine(_managerTasks.DoTasks());
    }
}