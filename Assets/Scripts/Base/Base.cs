using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(BotKeeper))]
[RequireComponent(typeof(BaseStorage))]
[RequireComponent(typeof(ManagerTasks))]
[RequireComponent(typeof(BotWithBoxDetector))]
[RequireComponent(typeof(FlagPlacer))]
public class Base : MonoBehaviour
{
    [SerializeField] BoxKeeper _boxKeeper;
    [SerializeField] ClickMapDetector _clickMapDetector;

    private BotKeeper _botKeeper;
    private ManagerTasks _managerTasks;
    private BotWithBoxDetector _botWithBoxDetector;
    private FlagPlacer _flagPlacer;

    private void Awake()
    {
        _botKeeper = GetComponent<BotKeeper>();
        _botKeeper.Init();

        _botWithBoxDetector = GetComponent<BotWithBoxDetector>();
        _botWithBoxDetector.Init(_botKeeper);

        _managerTasks = GetComponent<ManagerTasks>();
        _managerTasks.Init(_botKeeper, _boxKeeper);

        _flagPlacer = GetComponent<FlagPlacer>();
        _flagPlacer.Init(_clickMapDetector);
    }

    private void Start()
    {
        StartCoroutine(_managerTasks.DoTasks());
    }
}