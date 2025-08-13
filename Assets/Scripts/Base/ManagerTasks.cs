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
    private BaseSpawner _baseSpawner;
    private CreatorTasksBringBox _creatorTasksBringBox;
    private CreatorTasksBuildBase _creatorTasksBuildBase;

    private Vector3 _baseSpawnPosition;

    private bool _isBotCreating = true;

    private void OnDisable()
    {
        _flagPlacer.FlagPlaced -= ToggleBuildStatus;
    }

    public void Init(BotKeeper botKeeper, BoxKeeper boxKeeper, BaseSpawner baseSpawner)
    {
        _flagPlacer.FlagPlaced += ToggleBuildStatus;

        _botKeeper = botKeeper;
        _boxKeeper = boxKeeper;
        _baseSpawner = baseSpawner;
        _creatorTasksBringBox = new CreatorTasksBringBox(_boxKeeper, transform);
        _creatorTasksBuildBase = new CreatorTasksBuildBase();
    }

    private void ToggleBuildStatus(Vector3 spawnPostion)
    {
        _isBotCreating = false;
        _baseSpawnPosition = spawnPostion;
    }

    public IEnumerator DoTasks()
    {
        WaitForSeconds delayBetweenTasks = new WaitForSeconds(SecondsDelayBetweenTasks);

        while (enabled)
        {
            if (_botKeeper.GetFree(out Bot bot) && _creatorTasksBringBox.CreateTask(out ITaskable task))
            {
                bot.DoJob(task);

                if (_isBotCreating)
                {
                    if (_baseStorage.GetBoxes(CountBoxesForNewBot))
                    {
                        _botKeeper.CreateNewBot();
                    }
                }

                else
                {
                    if (_botKeeper.CountBots > MinCountBotsOnBase && _baseStorage.GetBoxes(CountBoxesForNewBase))
                    {
                        _creatorTasksBuildBase.CreateTask(_baseSpawner, out ITaskable task2, _baseSpawnPosition);
                        bot.DoJob(task2);
                        Debug.Log("отправился строить базу");
                        _isBotCreating = true;
                    }
                }
            }


            yield return delayBetweenTasks;
        }
    }
}