using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BotDetector))]
[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(BotCreator))]
public class Base : MonoBehaviour
{
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Transform _pointOut;
    [SerializeField] private Transform _pointIn;
    [SerializeField] private BoxScanner _boxScanner;

    private BotDetector _botDetector;
    private BoxReceiver _boxReceiver;
    private BotCreator _botCreator;

    private void Awake()
    {
        _botCreator = GetComponent<BotCreator>();
        _botCreator.Init(this);
        _botDetector = GetComponent<BotDetector>();
        _boxReceiver = new BoxReceiver(_boxScanner);
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

        _boxReceiver.TakeBox(bot.Box);
        _botCreator.ReturnBot(bot);
    }

    public Vector3 GetPointIn() => _pointIn.position;

    public Vector3 GetPointOut() => _pointOut.position;

    private void AssignBot(Box box)
    {
        Bot bot = _botCreator.GetFreeBot();

        if (bot != null)
        {
            _boxScanner.AcceptBox(box);
            bot.Init(this);
            bot.BringBox(box);
        }
    }
}