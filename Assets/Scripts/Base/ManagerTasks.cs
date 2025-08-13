using System.Collections;
using UnityEngine;

public class ManagerTasks : MonoBehaviour
{
    private const int CountBoxesForNewBot = 30;
    private const int CountBoxesForNewBase = 2;
    private const float SecondsDelayBetweenTasks = 0.1f;
    private const int MinCountBotsOnBase = 0;

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
        WaitForSeconds delayBetweenTasks = new(SecondsDelayBetweenTasks);

        while (enabled)
        {
            if (_botKeeper.GetFree(out Bot bot))
            {
                if (_isBaseBuild)
                {
                    if (_botKeeper.CountBots > MinCountBotsOnBase && _baseStorage.TryGetBoxes(CountBoxesForNewBase))
                    {
                        _creatorTasksBuildBase.CreateTask(out ITaskable buildBase, _flag);
                        bot.DoJob(buildBase);
                        _flag = null;
                        _isBaseBuild = false;
                    }
                    else
                    if (_creatorTasksBringBox.CreateTask(out ITaskable bringBox))
                    {
                        bot.DoJob(bringBox);

                        if (_baseStorage.TryGetBoxes(CountBoxesForNewBot))
                        {
                            _botKeeper.CreateNewBot();
                        }
                    }
                }
                else
                if (_creatorTasksBringBox.CreateTask(out ITaskable bringBox))
                {
                    bot.DoJob(bringBox);

                    if (_baseStorage.TryGetBoxes(CountBoxesForNewBot))
                    {
                        _botKeeper.CreateNewBot();
                    }
                }
            }

            yield return delayBetweenTasks;
        }
    }
}