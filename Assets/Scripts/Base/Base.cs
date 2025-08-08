using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(BotKeeper))]
[RequireComponent(typeof(BringBoxTasker))]
[RequireComponent(typeof(BaseStorage))]
public class Base : MonoBehaviour
{
    [SerializeField] private BoxKeeper _boxKeeper;

    private BringBoxTasker _bringBoxesTasker;
    private BotKeeper _botCreator;
    private BaseStorage _storage;

    private List<Bot> _bots = new();

    private void Awake()
    {
        _botCreator = GetComponent<BotKeeper>();
        _bringBoxesTasker = GetComponent<BringBoxTasker>();
        _storage = GetComponent<BaseStorage>();

        _storage.Init(_bots);
        _botCreator.Init(_bots);
        _bringBoxesTasker.Init(_boxKeeper, _botCreator);

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