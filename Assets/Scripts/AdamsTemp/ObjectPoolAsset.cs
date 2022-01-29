using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object Pool Asset", menuName = "ScriptableObjects/ObjectPoolAsset")]
public class ObjectPoolAsset : ScriptableObject
{
	[SerializeField] private GameObject[] poolPrefabs = new GameObject[5];
	[SerializeField] private string containerName;
	[SerializeField] private GameObject[] poolObjects;

	public GameObject[] Prefabs => poolPrefabs;
	public Transform Container { get; set; }
	public string ContainerName { get { return containerName; } set { } }
	public GameObject[] Objects { get { return poolObjects; } set { } }
}
