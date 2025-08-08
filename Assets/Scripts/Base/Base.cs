using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(BotCreator))]
[RequireComponent(typeof(BotReceiver))]
[RequireComponent(typeof(BringBoxesTasker))]
[RequireComponent(typeof(BaseStorage))]
public class Base : MonoBehaviour
{
    [SerializeField] private BoxKeeper _boxKeeper;
    [SerializeField] private PoolBoxes _poolBoxes;

    private BringBoxesTasker _bringBoxesTasker;
    private BotReceiver _botReceiver;
    private BotCreator _botCreator;
    private BaseStorage _baseStorageBoxes;

    private BaseStation _baseStation;

    private enum BaseStation
    {
        CreateBots,
        CreateBase
    }

    private void Awake()
    {
        _botCreator = GetComponent<BotCreator>();
        _baseStorageBoxes = GetComponent<BaseStorage>();
        _botReceiver = GetComponent<BotReceiver>();
        _bringBoxesTasker = GetComponent<BringBoxesTasker>();

        _bringBoxesTasker.Init(_boxKeeper, _botCreator);
        _botReceiver.Init(_baseStorageBoxes);

        _baseStation = BaseStation.CreateBots;
    }

    private void Start()
    {
        StartCoroutine(DoJobs());
    }

    private IEnumerator DoJobs()
    {
        while (enabled)
        {
            _bringBoxesTasker.TryBringBox();

            yield return new WaitForSeconds(0.3f);
        }
    }
}