using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class MainSpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private ColorChanger _colorChanger;

    private Spawner[] _spawners;
    private ObjectPool<Enemy> _pool;

    private void Start()
    {
        _spawners = GetComponentsInChildren<Spawner>();

        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnGet: (enemy) => enemy.gameObject.SetActive(true),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject)
            );

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait;
        Spawner spawner;
        Enemy enemy;
        Transform goal;

        Vector3 position;

        int spawnerIndex;

        int period = 2;
        bool shoudSpawn = true;

        wait = new WaitForSeconds(period);

        while (shoudSpawn)
        {
            spawnerIndex = Random.Range(0, _spawners.Length);
            spawner = _spawners[spawnerIndex];
            position = spawner.transform.position;

            goal = spawner.GetGoal();

            enemy = _pool.Get();

            enemy.transform.position = position;
            _colorChanger.Change(enemy.Renderer, _spawners[spawnerIndex].GetColor());
            enemy.Go(goal);

            enemy.ReachedGoal += ReleaseEnemy;

            yield return wait;
        }
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        enemy.ReachedGoal -= ReleaseEnemy;

        _pool.Release(enemy);
    }
}
