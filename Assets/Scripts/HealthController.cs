using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
	private static HealthController _instance;
	public static HealthController Instance { get { return _instance; } }

	[SerializeField] private GameObject UIHealthContainer;
	[SerializeField] private GameObject[] healthPointsSlots = new GameObject[10];
	[SerializeField] private int startHealth = 3;
	[SerializeField] private float healthPointDistance = 40f;

	public int CurrentHealth;


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

	public void Initialize()
	{
		CurrentHealth = startHealth;

		var newPosition = Vector3.zero;
		for (int i = 0; i < healthPointsSlots.Length; i++)
		{
			healthPointsSlots[i] = UIHealthContainer.transform.GetChild(i).gameObject;
			var rectTransform = healthPointsSlots[i].GetComponent<RectTransform>();
			
			if (i == 0)
			{
				newPosition = rectTransform.position;
				continue;
			}

			if (i == healthPointsSlots.Length * .5f)
			{
				newPosition = healthPointsSlots[0].GetComponent<RectTransform>().position;
				newPosition.y -= healthPointDistance;
			}
			else
			{
				newPosition.x += healthPointDistance;
			}

			rectTransform.position = newPosition;
		}
		for (int i = 0; i < startHealth; i++)
		{
			SetActiveHealthPoints(i, true);
		}

		if (startHealth == healthPointsSlots.Length)
			return;

		for (int i = startHealth; i < healthPointsSlots.Length; i++)
		{
			SetActiveHealthPoints(i, false);
		}
	}

	public void SetActiveHealthPoints(int index, bool active)
	{
		healthPointsSlots[index].SetActive(active);
	}

	/// <summary>
	/// Subtracts One Health Point(Heart).
	/// </summary>
	public void SubtractHealth()
	{
		for (int i = 0; i < healthPointsSlots.Length; i++)
		{
			if (!healthPointsSlots[i].activeSelf)
			{
				healthPointsSlots[i - 1].SetActive(false);
				return;
			}
		}
	}
}