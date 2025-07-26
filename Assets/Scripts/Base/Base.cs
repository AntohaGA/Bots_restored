using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BotDetector))]
[RequireComponent(typeof(NavMeshObstacle))]
public class Base : MonoBehaviour
{
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private PoolBots _poolBots;
    [SerializeField] private Transform _pointOut;
    [SerializeField] private Transform _pointIn;
    [SerializeField] private BoxScanner _boxScanner;

    private BotDetector _botDetector;
    private BotReceiver _botReceiver;
    private BoxReceiver _boxManager;

    private int _countMaxBots = 5;

    private void Awake()
    {
        _botDetector = GetComponent<BotDetector>();
        _botReceiver = new BotReceiver(_poolBots, _countMaxBots);
        _botReceiver.Init(_botPrefab);
        _boxManager = new BoxReceiver(_boxScanner);
    }

    private void OnEnable()
    {
        _botDetector.BotReceived += TakeBot;
        _boxScanner.OfferedClosestBox += HandleBoxFound;
    }

    private void OnDisable()
    {
        _botDetector.BotReceived -= TakeBot;
        _boxScanner.OfferedClosestBox -= HandleBoxFound;
    }

    private void Start()
    {
        StartCoroutine(_boxScanner.ScanRoutine());
    }

    private void HandleBoxFound(Box box)
    {
        if (box != null)
        {
            AssignBot(box);
        }
    }

    public void TakeBot(Bot bot)
    {
        if (bot == null)
            return;

        _boxManager.TakeBox(bot.Box);
        _botReceiver.TakeBot(bot);
    }

    public Vector3 GetPointIn() => _pointIn.position;

    public Vector3 GetPointOut() => _pointOut.position;

    private void AssignBot(Box box)
    {
        Bot bot = _botReceiver.GetBot();

        if (bot != null)
        {
            _boxScanner.AcceptBox(box);
            bot.Init(this);
            bot.BringBox(box);
        }
    }
}