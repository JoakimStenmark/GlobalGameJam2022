using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnObject : MonoBehaviour
{
	[SerializeField] private float despawnTime = GeneralRules.DespawnTime;
	private float currentTime;


	private void OnEnable()
	{
		currentTime = 0f;
	}

	private void FixedUpdate()
	{
		if (!gameObject.activeSelf) return;

		currentTime += Time.fixedDeltaTime;
		if (currentTime >= despawnTime)
		{
			gameObject.SetActive(false);
			if (TryGetComponent(out Rigidbody rb))
				rb.velocity = Vector3.zero;
		}
	}
}
