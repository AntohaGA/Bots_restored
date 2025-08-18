using System.Collections;
using UnityEngine;

public class QueueTasks : MonoBehaviour
{
    private const float SecondsDelayBetweenTasks = 0.1f;
    private const int MinCountBotsOnBase = 1;

    [SerializeField] private BoxStorage _baseStorage;
    [SerializeField] private FlagPlacer _flagPlacer;

    private Flag _flag;
    private BotKeeper _botKeeper;
    private CreatorTasksBringBox _creatorTasksBringBox;
    private CreatorTasksBuildBase _creatorTasksBuildBase;

    public bool IsBaseBuild { get; private set; } = false;

    public void Init(BotKeeper botKeeper, BoxKeeper boxKeeper, BaseSpawner baseSpawner)
    {
        _botKeeper = botKeeper;
        _creatorTasksBringBox = new CreatorTasksBringBox(boxKeeper, transform);
        _creatorTasksBuildBase = new CreatorTasksBuildBase(baseSpawner, _baseStorage);
    }

    public void ToggleBuildStatus(Flag flag)
    {
        IsBaseBuild = true;
        _flag = flag;
    }

    public IEnumerator DoTasks()
    {
        WaitForSeconds delay = new(SecondsDelayBetweenTasks);

        while (enabled)
        {
            if (IsBaseBuild)
            {
                if (_botKeeper.CountBots <= MinCountBotsOnBase)
                {
                    TryCreateBot();
                }
                else
                {
                    if (_botKeeper.GetFree(out Bot builder))
                    {
                        if (TrySpawnBase(builder) == false)
                        {
                            _botKeeper.SetFree(builder);
                        }
                    }
                }
            }
            else
            {
                TryCreateBot();
            }

            if (_botKeeper.GetFree(out Bot carrier))
            {
                if (TryBringBox(carrier) == false)
                {
                    _botKeeper.SetFree(carrier);
                }
            }

            yield return delay;
        }
    }

    private bool TrySpawnBase(Bot bot)
    {
        if (_creatorTasksBuildBase.TryCreateTask(out ITask buildBase, _flag))
        {
            _botKeeper.RemoveBot(bot);
            bot.DoJob(buildBase);
            _flag = null;
            IsBaseBuild = false;

            return true;
        }

        return false;
    }

    private bool TryBringBox(Bot bot)
    {
        if (_creatorTasksBringBox.CreateTask(out ITask bringBox))
        {
            bot.DoJob(bringBox);

            return true;
        }

        return false;
    }

    private bool TryCreateBot()
    {
        if (_botKeeper.TryCreateNewBot())
        {
            return true;
        }

        return false;
    }
}