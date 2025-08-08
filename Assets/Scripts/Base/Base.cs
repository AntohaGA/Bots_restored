using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(ClickBaseDetector))]
[RequireComponent(typeof(MapFlagPlacer))]
[RequireComponent(typeof(BotCreator))]
[RequireComponent(typeof(BotReceiver))]
[RequireComponent(typeof(BringBoxesTasker))]
[RequireComponent(typeof(BaseStorage))]
public class Base : MonoBehaviour
{
    [SerializeField] private BoxKeeper _boxKeeper;
    [SerializeField] private BaseBuilder _builder;
    [SerializeField] private Map _map;
    [SerializeField] private PoolBoxes _poolBoxes;

    private BringBoxesTasker _bringBoxesTasker;
    private BotReceiver _botReceiver;
    private BotCreator _botCreator;
    private MapFlagPlacer _mapFlagPlacer;
    private BaseStorage _baseStorageBoxes;

    private Vector3 _positionForNewBase;

    private BaseStation _baseStation;

    private enum BaseStation
    {
        CreateBots,
        CreateBase
    }

    private void Awake()
    {
        _botCreator = GetComponent<BotCreator>();
        _mapFlagPlacer = GetComponent<MapFlagPlacer>();
        _baseStorageBoxes = GetComponent<BaseStorage>();
        _botReceiver = GetComponent<BotReceiver>();
        _bringBoxesTasker = GetComponent<BringBoxesTasker>();

        _bringBoxesTasker.Init(_boxKeeper, _botCreator);
        _botReceiver.Init(_baseStorageBoxes, _botCreator);
        _baseStorageBoxes.Init(_poolBoxes);
        _mapFlagPlacer.Init(_map);

        _baseStation = BaseStation.CreateBots;
    }

    public void Init(BoxKeeper boxKeeper, BaseBuilder builder, PoolBoxes poolBoxes, Map map)
    {
        _boxKeeper = boxKeeper;
        _builder = builder;
        _poolBoxes = poolBoxes;
        _map = map;
    }

    private void OnEnable()
    {
        _mapFlagPlacer.FlagPlaced += ToggleBaseToBildStation;
    }

    private void OnDisable()
    {
        _mapFlagPlacer.FlagPlaced -= ToggleBaseToBildStation;
    }

    private void Start()
    {
        StartCoroutine(DoJobs());
    }

    private void ToggleBaseToBildStation(Vector3 position)
    {
        _positionForNewBase = position;
        _baseStation = BaseStation.CreateBase;
    }

    private IEnumerator DoJobs()
    {
        while (enabled)
        {
            if (_baseStation == BaseStation.CreateBots)
            {
                _bringBoxesTasker.TryBringBox();
            }

            if (_baseStation == BaseStation.CreateBase)
            {
                _builder.Build(_positionForNewBase, _boxKeeper, _builder, _poolBoxes, _map);
                _baseStation = BaseStation.CreateBots;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
}