using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnController : MonoBehaviour
{
	private static ObjectSpawnController _instance;
	public static ObjectSpawnController Instance { get { return _instance; } }

	[SerializeField] private ObjectPoolAsset[] pools;
	[SerializeField] private SpawnArea spawnArea;
	[Header("Spawn Settings")]
	[SerializeField] private float minDelay;
	[SerializeField] private float maxDelay;
	[SerializeField] private bool doSpawn = false;

	[SerializeField] public float minBound = -13;
	[SerializeField] public float maxBound = 13;
	[SerializeField] private GameObject obstaclePrefab;
	[SerializeField] private Transform obstaclesContainer;

	private bool allowSpawning = true;

	private void Awake()
	{
		if (_instance != null && _instance != this)
			Destroy(this);
		else
			_instance = this;
	}

	private void Start()
	{
		Initialize();
	}

	private void Initialize()
	{
		for (int i = 0; i < pools.Length; i++)
		{
			pools[i].Objects = new GameObject[pools[i].Prefabs.Length];
			//pools[i].ContainerName = pools[i].Container.gameObject.name;
			pools[i].Container = GameObject.Find(pools[i].ContainerName).transform;
			InitializePool(pools[i]);
		}
		
		StartSpawnerRoutine();
	}

	private void InitializePool(ObjectPoolAsset pool)
	{
		pool.Objects = new GameObject[pool.Prefabs.Length];
		for (int i = 0; i < pool.Prefabs.Length; i++)
		{
			GameObject obj = Instantiate(pool.Prefabs[i], pool.Container);
			pool.Objects[i] = obj;
			obj.SetActive(false);
		}
	}

	public void StartSpawnerRoutine()
	{
		StartCoroutine(TimeBetweenSpawnLoopsRoutine(Random.Range(minDelay, maxDelay)));
	}

	private IEnumerator TimeBetweenSpawnLoopsRoutine(float delay)
	{
		yield return new WaitForSeconds(delay);
		SpawnRandomObject();
	}

	public void SpawnRandomObject()
	{
		if (!doSpawn) return;

		int availablePoolsCount = pools.Length;
		int choiceOfPool = Random.Range(0, availablePoolsCount + 1);

        if (choiceOfPool != 0 && choiceOfPool != availablePoolsCount) // thus doubling the chance for platforms
        {
			print($"choiceOfPool is: {choiceOfPool}");
			GameObject[] pool = pools[choiceOfPool].Objects;

			List<GameObject> inactiveObjects = new List<GameObject>();
			foreach (GameObject item in pool)
			{
				if (!item.activeSelf)
				{
					inactiveObjects.Add(item);
				}
			}
			// Check whether we actually already have all of the objects out of the pool, then something is likely to be wrong.
			GameObject obj;
			if (inactiveObjects.Count == 0)
			{
				Debug.LogWarning($"No available pool-object to spawn, all are already spawned.");
				return;
			}
			else if (inactiveObjects.Count == 1)
			{
				obj = inactiveObjects[0];
			}
			else
			{
				obj = inactiveObjects[Random.Range(0, inactiveObjects.Count)];
			}

			obj.transform.position = spawnArea.GetRandomPosition();
			obj.SetActive(true);
        }
		else
        {
			print("platform");	
			GameObject obs = Instantiate(obstaclePrefab, obstaclesContainer);
			obs.transform.position = spawnArea.GetRandomPosition();
			if (obs.TryGetComponent<BoxCollider>(out BoxCollider c))
            {
				c.size = new Vector3(Random.Range(2, 8),1f , 1f);
            }
        }
		
		if (allowSpawning)
			StartSpawnerRoutine();
	}

	public void StopSpawner()
	{
		StopAllCoroutines();
		allowSpawning = false;
	}

	public void ReturnAllObjectsToPool()
	{
		print("Removing all pooled objects, returning them to Pool");
		foreach (var item in pools)
		{
			foreach (var obj in item.Objects)
			{
				if (obj.activeSelf)
					obj.SetActive(false);
			}
		}
		foreach (Transform obj in obstaclesContainer)
		{
			Destroy(obj.gameObject);
		}
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
		foreach (var item in pools)
		{
			for (int i = 0; i < item.Objects.Length; i++)
			{
				item.Objects[i] = null;
			}
		}
	}
}
