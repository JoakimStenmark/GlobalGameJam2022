using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnController : MonoBehaviour
{
	private static ObjectSpawnController _instance;
	public static ObjectSpawnController Instance { get { return _instance; } }

	[SerializeField] private ObjectPoolAsset collectibles;
	[SerializeField] private ObjectPoolAsset obstacles;
	[SerializeField] private SpawnArea spawnArea;
	[Header("Spawn Settings")]
	[SerializeField] private float minTime;
	[SerializeField] private float maxTime;
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

	private void Update()
	{
		Spawner();
	}

	private void Initialize()
	{
		InitializePool(collectibles);
		InitializePool(obstacles);
	}

	private void InitializePool(ObjectPoolAsset pool)
	{
		foreach (GameObject obj in pool.PoolObjects)
		{
			obj.SetActive(false);
		}
	}

	public void SpawnRandomObject(ObjectPoolAsset pool)
	{
		List<GameObject> inactiveObjects = new List<GameObject>();
		foreach (GameObject item in pool.PoolObjects)
		{
			if (!item.activeSelf)
			{
				inactiveObjects.Add(item);
			}
		}
		// Check whether we actually already have all of the objects out of the pool, then something is likely to be wrong.
		if (inactiveObjects.Count == 0)
		{
			Debug.LogWarning($"No available pool-object to spawn, all are already spawned.");
			return;
		}

		GameObject obj = inactiveObjects[Random.Range(0, pool.PoolObjects.Length)];
		
		obj.transform.position = spawnArea.GetRandomPosition();
		obj.SetActive(true);
		
	}

	private void Spawner()
	{
		if (!doSpawn) return;

		ObjectPoolAsset[] availablePools = { collectibles, obstacles };
		ObjectPoolAsset pool = Random.Range(0, availablePools.Length) == 0 ? collectibles : obstacles;
		SpawnRandomObject(pool);
	}
}
