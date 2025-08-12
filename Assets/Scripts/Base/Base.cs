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
    private BotKeeper _botKeeper;
    private ManagerTasks _managerTasks;

    private void Awake()
    {
        _botKeeper = GetComponent<BotKeeper>();
        _managerTasks = GetComponent<ManagerTasks>();

        _botKeeper.Init();
    }

    private void Start()
    {
        StartCoroutine( _managerTasks.DoTasks());
    }
}