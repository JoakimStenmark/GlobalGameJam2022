using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI highscoreTxt;
	private int highscore;

	private void Awake()
	{
		PlayerPrefs.GetInt("Highscore", highscore);
		highscoreTxt.text = $"Highscore: {highscore}";
	}

	public void LoadScene(int scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
