using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(BotCreator))]
[RequireComponent(typeof(BotWithBoxReceiver))]
[RequireComponent(typeof(BringBoxesTasker))]
[RequireComponent(typeof(BaseStorage))]
public class Base : MonoBehaviour
{
    [SerializeField] private BoxKeeper _boxKeeper;

    private BringBoxesTasker _bringBoxesTasker;
    private BotCreator _botCreator;

    private void Awake()
    {
        _botCreator = GetComponent<BotCreator>();
        _bringBoxesTasker = GetComponent<BringBoxesTasker>();

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