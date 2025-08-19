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
    private BaseSpawner _baseSpawner;

    private BotKeeper _botKeeper;
    private QueueTasks _managerTasks;
    private BotWithBoxDetector _botWithBoxDetector;
    private BoxStorage _boxStorage;

    public FlagPlacer FlagPlacer { get; private set; }

    private void OnDisable()
    {
        _botWithBoxDetector.BotReceived -= OnBotReceived;
        FlagPlacer.FlagPlased -= ToggleBuildStatus;
    }

    public void InitDependencies(BoxKeeper boxKeeper, BaseSpawner baseSpawner)
    {
        _boxKeeper = boxKeeper;
        _baseSpawner = baseSpawner;
    }

    public void Initialize(Bot bot)
    {
        FlagPlacer = GetComponent<FlagPlacer>();
        _boxStorage = GetComponent<BoxStorage>();
        _botKeeper = GetComponent<BotKeeper>();
        _botKeeper.Init(bot, _boxStorage);
        _botWithBoxDetector = GetComponent<BotWithBoxDetector>();
        _managerTasks = GetComponent<QueueTasks>();
        _managerTasks.Init(_botKeeper, _boxKeeper, _baseSpawner);
        Subscribe();

        StartCoroutine(_managerTasks.DoTasks());
    }

    private void Subscribe()
    {
        _botWithBoxDetector.BotReceived += OnBotReceived;
        FlagPlacer.FlagPlased += ToggleBuildStatus;
    }

    private void ToggleBuildStatus(Flag flag)
    {
        _managerTasks.ToggleBuildStatus(flag);
    }

    private void OnBotReceived(Bot bot)
    {
        if (_botKeeper.IsOurBusyBot(bot))
        {
            _botKeeper.SetFree(bot);
            bot.MadeFree();
            _boxStorage.AddBoxOnBase();
        }
    }
}