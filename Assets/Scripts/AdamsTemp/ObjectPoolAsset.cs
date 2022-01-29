using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object Pool Asset", menuName = "ScriptableObjects/ObjectPoolAsset")]
public class ObjectPoolAsset : ScriptableObject
{
	[SerializeField] private GameObject[] poolObjects = new GameObject[5];

	public GameObject[] PoolObjects => poolObjects;

}
