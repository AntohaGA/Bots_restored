using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(ClickBaseDetector))]
[RequireComponent(typeof(MapFlagPlacer))]
[RequireComponent(typeof(BotCreator))]
[RequireComponent(typeof(BoxStorage))]
public class Base : MonoBehaviour
{
    [SerializeField] private BoxKeeper _boxKeeper;
    [SerializeField] private BringBoxesTasker _bringBoxesTasker;
    [SerializeField] private BotReceiver _botReceiver;

    private BotCreator _botCreator;
    private MapFlagPlacer _mapFlagPlacer;
    private BoxStorage _boxStorage;

    private void Awake()
    {
        _botCreator = GetComponent<BotCreator>();
        _mapFlagPlacer = GetComponent<MapFlagPlacer>();
        _boxStorage = GetComponent<BoxStorage>();

        _bringBoxesTasker.Init(_boxKeeper, _botCreator, _boxStorage);
        _botReceiver.Init(_boxStorage);
    }

    private void Start()
    {
        _bringBoxesTasker.Do();
    }
}