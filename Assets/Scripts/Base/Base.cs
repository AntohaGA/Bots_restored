using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BotDetector))]
[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(BotCreator))]
[RequireComponent(typeof(ClickBaseDetector))]
[RequireComponent(typeof(MapFlagPlacer))]
[RequireComponent(typeof(BoxStorage))]
public class Base : MonoBehaviour
{
    [SerializeField] private Transform _pointIn;
    [SerializeField] private BoxKeeper _boxKeeper;

    private BotDetector _botDetector;
    private BotCreator _botCreator;
    private MapFlagPlacer _mapFlagPlacer;
    private BoxStorage _boxStorage;

    public Vector3 GetPointIn() => _pointIn.position;

    private void Awake()
    {
        _botCreator = GetComponent<BotCreator>();
        _botDetector = GetComponent<BotDetector>();
        _mapFlagPlacer = GetComponent<MapFlagPlacer>();
        _boxStorage = GetComponent<BoxStorage>();
    }

    private void Start()
    {
        StartCoroutine(TryAssignBot());
    }

    private void OnEnable()
    {
        _botDetector.BotReceived += TakeBotWithBox;
    }

    private void OnDisable()
    {
        _botDetector.BotReceived -= TakeBotWithBox;
    }

    private void TakeBotWithBox(Bot bot)
    {
        if (bot == null)
            return;

        _boxStorage.AddBoxOnBase(bot.Box);
        bot.MadeFree();
    }

    private IEnumerator TryAssignBot()
    {
        while (enabled)
        {
            if (_boxKeeper.TryFindNearestBox(transform.position, out Box box) && _botCreator.TryGetFreeBot(out Bot bot))
            {
                _boxKeeper.ReserveBox(box);
                _boxStorage.ReserveBox(box);
                bot.BringBox(box, GetPointIn());               
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}