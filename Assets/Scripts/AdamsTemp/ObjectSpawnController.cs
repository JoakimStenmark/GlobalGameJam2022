using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnController : MonoBehaviour
{
	private static ObjectSpawnController _instance;
	public static ObjectSpawnController Instance { get { return _instance; } }

	[SerializeField] private ObjectPoolAsset collectibles;
	[SerializeField] private ObjectPoolAsset obstacles;
	[SerializeField] private Transform collectiblesPoolContainer;
	[SerializeField] private Transform obstaclesPoolContainer;
	[SerializeField] private GameObject[] collectiblesPool;
	[SerializeField] private GameObject[] obstaclesPool;
	[SerializeField] private SpawnArea spawnArea;
	[Header("Spawn Settings")]
	[SerializeField] private float minDelay;
	[SerializeField] private float maxDelay;
	[SerializeField] private bool doSpawn = false;


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

	//private void Update()
	//{
	//	Spawner();
	//}

	private void Initialize()
	{
		collectiblesPool = new GameObject[collectibles.PoolObjects.Length];
		obstaclesPool = new GameObject[obstacles.PoolObjects.Length];

		InitializePool(collectibles, collectiblesPoolContainer, collectiblesPool);
		InitializePool(obstacles, obstaclesPoolContainer, obstaclesPool);
		StartSpawnerRoutine();
	}

	private void InitializePool(ObjectPoolAsset prefabPool, Transform parent, GameObject[] pool)
	{
		for (int i = 0; i < prefabPool.PoolObjects.Length; i++)
		{
			var obj = Instantiate(prefabPool.PoolObjects[i], parent);
			pool[i] = obj;
			obj.SetActive(false);
		}
	}

	private void StartSpawnerRoutine()
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

		Transform[] availablePools = { collectiblesPoolContainer, obstaclesPoolContainer };
		GameObject[] pool = Random.Range(0, availablePools.Length) == 0 ? collectiblesPool : obstaclesPool;

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

		StartSpawnerRoutine();
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}
}
