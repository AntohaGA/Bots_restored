using System.Collections;
using UnityEngine;

public class ManagerTasks : MonoBehaviour
{
    private const int CountBoxesForNewBot = 3;
    private const int CountBoxesForNewBase = 5;
    private const float SecondsDelayBetweenTasks = 0.1f;
    private const int MinCountBotsOnBase = 1;

    [SerializeField] private BoxStorage _baseStorage;
    [SerializeField] private FlagPlacer _flagPlacer;

    private Flag _flag;
    private BotKeeper _botKeeper;
    private CreatorTasksBringBox _creatorTasksBringBox;
    private CreatorTasksBuildBase _creatorTasksBuildBase;

    private bool _isBaseBuild = false;

    private void OnDisable()
    {
        _flagPlacer.FlagPlaced -= ToggleBuildStatus;
    }

    public void Init(BotKeeper botKeeper, BoxKeeper boxKeeper, BaseSpawner baseSpawner)
    {
        _flagPlacer.FlagPlaced += ToggleBuildStatus;
        _botKeeper = botKeeper;
        _creatorTasksBringBox = new CreatorTasksBringBox(boxKeeper, transform);
        _creatorTasksBuildBase = new CreatorTasksBuildBase(baseSpawner);
    }

    private void ToggleBuildStatus(Flag flag)
    {
        _isBaseBuild = true;
        _flag = flag;
    }

    public IEnumerator DoTasks()
    {
        WaitForSeconds delay = new(SecondsDelayBetweenTasks);

        while (enabled)
        {
            if (_botKeeper.GetFree(out Bot bot))
            {
                if (_isBaseBuild)
                {
                    if (_botKeeper.CountBots <= MinCountBotsOnBase)
                    {
                        TryCreateBot();
                    }
                    else
                    {
                        if (TrySpawnBase(bot))
                        {
                            _botKeeper.Remove(bot);
                        }
                    }

                    if (_botKeeper.GetFree(out bot))
                    {
                        TryBringBox(bot);
                    }
                }
                else
                {
                    TryBringBox(bot);
                    TryCreateBot();
                }
            }

            yield return delay;
        }
    }

    private bool TrySpawnBase(Bot bot)
    {
        if (_botKeeper.CountBots > MinCountBotsOnBase && _baseStorage.TryGetBoxes(CountBoxesForNewBase))
        {
            _creatorTasksBuildBase.CreateTask(out ITaskable buildBase, _flag);
            bot.DoJob(buildBase);
            _flag = null;
            _isBaseBuild = false;

            return true;
        }

        return false;
    }

    private bool TryBringBox(Bot bot)
    {
        if (_creatorTasksBringBox.CreateTask(out ITaskable bringBox))
        {
            bot.DoJob(bringBox);

            return true;
        }

        return false;
    }

    private bool TryCreateBot()
    {
        if (_baseStorage.TryGetBoxes(CountBoxesForNewBot))
        {
            _botKeeper.CreateNewBot();

            return true;
        }

        return false;
    }
}