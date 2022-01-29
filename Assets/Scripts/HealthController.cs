using System;
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

		OrganizeHealthPointSlotsUI();

		SetAmountOfActiveHealthPoints(startHealth);

		if (startHealth == healthPointsSlots.Length)
			return;

		for (int i = startHealth; i < healthPointsSlots.Length; i++)
		{
			SetActiveHealthPoint(i, false);
		}
	}

	private void OrganizeHealthPointSlotsUI()
	{
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
	}

	/// <summary>
	/// Set the activation of a HealthPoint specified with index.
	/// </summary>
	/// <param name="index"></param>
	/// <param name="active"></param>
	public void SetActiveHealthPoint(int index, bool active)
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

	public void SetAmountOfActiveHealthPoints(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			healthPointsSlots[i].SetActive(true);
		}
	}

	/// <summary>
	/// Use this to Update the CurrentHealth of the player, including the UI elements/visuals.
	/// </summary>
	public void UpdateCurrentHealth()
	{
		CurrentHealth = 0;

		for (int i = 0; i < healthPointsSlots.Length; i++)
		{
			if (healthPointsSlots[i].activeSelf)
				CurrentHealth++;
			else
				break;
		}

		SetAmountOfActiveHealthPoints(CurrentHealth);
	}
}