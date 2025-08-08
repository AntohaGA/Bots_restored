using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BotWithBoxDetector))]
public class BaseStorage : MonoBehaviour
{
    private BotWithBoxDetector _botWithBoxDetector;

    private List<Bot> _bots;

    private int _countBoxes = 0;

    private void Awake()
    {
        _botWithBoxDetector = GetComponent<BotWithBoxDetector>();
    }

    private void OnEnable()
    {
        _botWithBoxDetector.OurBotReceived += AddBoxOnBase;
    }

    private void OnDisable()
    {
        _botWithBoxDetector.OurBotReceived -= AddBoxOnBase;
    }

    public void Init(List<Bot> bots)
    {
        _bots = bots;
    }

    public void AddBoxOnBase(Bot bot)
    {
        if (IsOurBot(bot))
        {
            bot.Box.transform.SetParent(null);
            bot.Box.SetRigidBodyKinematic(false);
            bot.Box.Return();
            bot.MadeFree();
            _countBoxes++;
        }

        return;
    }

    private bool IsOurBot(Bot bot)
    {
        if (_bots.Contains(bot))
        {
            return true;
        }

        return false;
    }
}