using System.Collections;
using UnityEngine;

public class ManagerTasks : MonoBehaviour
{
    private const int CountBoxesForNewBot = 3;
    private const int CountBoxesForNewBase = 5;
    private const float SecondsDelayBetweenTasks = 0.1f;
    private const int MinCountBotsOnBase = 1;

    [SerializeField] private BaseStorage _baseStorage;
    [SerializeField] private FlagPlacer _flagPlacer;

    private BotKeeper _botKeeper;
    private BoxKeeper _boxKeeper;
    private CreatorTasksBringBox _creatorTasksBringBox;
    private CreatorTasksBuildBase _creatorTasksBuildBase;

    private Vector3 _baseSpawnPosition;

    private bool _isBotCreating = true;

    private void OnDisable()
    {
        _flagPlacer.FlagPlaced -= ToggleBuildStation;
    }

    public void Init(BotKeeper botKeeper, BoxKeeper boxKeeper)
    {
        _flagPlacer.FlagPlaced += ToggleBuildStation;

        _botKeeper = botKeeper;
        _boxKeeper = boxKeeper;
        _creatorTasksBringBox = new CreatorTasksBringBox(_boxKeeper, transform);
        _creatorTasksBuildBase = new CreatorTasksBuildBase();
    }

    private void ToggleBuildStation(Vector3 spawnPostion)
    {
        _isBotCreating = false;
        _baseSpawnPosition = spawnPostion;
    }

    public IEnumerator DoTasks()
    {
        WaitForSeconds delayBetweenTasks = new WaitForSeconds(SecondsDelayBetweenTasks);

        while (enabled)
        {
            if (_isBotCreating)
            {
                if (_botKeeper.GetFree(out Bot bot) && _creatorTasksBringBox.CreateTask(out ITaskable _task))
                {
                    bot.DoJob(_task);

                    if (_baseStorage.GetBoxes(CountBoxesForNewBot))
                    {
                        _botKeeper.CreateNewBot();
                    }
                }
            }

            else
            {
                if (_botKeeper.CountBots > MinCountBotsOnBase && _baseStorage.GetBoxes(CountBoxesForNewBase))
                {
                    _creatorTasksBuildBase.CreateTask(out ITaskable _task, new Vector3(10, 0, 10));
                    _isBotCreating = true;
                }
            }

            yield return delayBetweenTasks;
        }
    }
}