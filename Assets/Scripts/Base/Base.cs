using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(BotKeeper))]
[RequireComponent(typeof(BaseStorage))]
public class Base : MonoBehaviour
{
    [SerializeField] private BoxKeeper _boxKeeper;

    private BotKeeper _botKeeper;
    private BaseStorage _storage;
    private MaderTaskBringBox _maderTaskBringBox;

    private Vector3 _pointDestination;

    private List<Bot> _bots = new();

    private void Awake()
    {
        _botKeeper = GetComponent<BotKeeper>();
        _storage = GetComponent<BaseStorage>();

        _maderTaskBringBox = new MaderTaskBringBox(_botKeeper, _boxKeeper, _pointDestination);

        _storage.Init(_bots);
        _botKeeper.Init(_bots);
    }
}