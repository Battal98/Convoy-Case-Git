using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    #region Spawn Prop Jobs

    [SerializeField]
    private int _SpawnTime = 1000;

    [SerializeField]
    private int _spawnEnemyCount = 10;

    [SerializeField]
    private List<Transform> _spawnTargets;

    [SerializeField]
    private List<GameObject> _spawnObjects;

    [HideInInspector]
    public List<GameObject> CreatedEnemies;

    #endregion

    [SerializeField]
    private GameObject _finishLine;

    private int _randomEnemiesValue;
    private int _randomSpawnerValue;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (!GameManager.isGameStarted || GameManager.isGameEnded)
            return;
    }

    public void SetSpawnObjectsPool()
    {
        for (int i = 0; i < _spawnEnemyCount; i++)
        {
            _randomEnemiesValue = Random.Range(0, _spawnObjects.Count);
            _randomSpawnerValue = Random.Range(0, _spawnTargets.Count);

            GameObject _createdEnemyCar = Instantiate(_spawnObjects[_randomEnemiesValue],_spawnTargets[_randomSpawnerValue].transform);
            CreatedEnemies.Add(_createdEnemyCar);
            _createdEnemyCar.SetActive(false);
        }
    }

    public async void StartSpawn()
    {
        int i = 0;

        while (CreatedEnemies.Count > i)
        {
            if (CreatedEnemies[i])
            {
                CreatedEnemies[i].gameObject.SetActive(true);
                CreatedEnemies[i].gameObject.transform.parent = null;
            }

            await Task.Delay(_SpawnTime);

            i++;

        }

        if (_finishLine)
            _finishLine.transform.parent = null;


    }
}
