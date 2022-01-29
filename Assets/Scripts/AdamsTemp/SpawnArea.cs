using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
	[SerializeField] private Color gizmoColour = new Color(.5f, .5f, .5f, .2f);

	private Transform _transform;


	public Vector3 GetRandomPosition()
	{
		_transform = transform;

		Vector3 origin = _transform.position;
		Vector3 range = _transform.localScale;
		Vector3 randomRange = new Vector3(
			Random.Range(-range.x, range.x),
			Random.Range(-range.y, range.y),
			Random.Range(-range.z, range.z));
		return origin + randomRange;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = gizmoColour;
		Gizmos.DrawCube(transform.position, transform.localScale);
	}
}
