using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }

    [Header("Score Settings")]
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI highscoreTxt;
    [SerializeField] private int currentScore;
    [SerializeField] private int highscore;
    [Space(10)]

    public GameObject gameOverScreen;
    public GameObject PlayerPrefab;

    private PlayerControllerTest playerInstance;
    private TestCameraFollow cameraFollow;

	private void Awake()
	{
		if (_instance != null && _instance != this)
			Destroy(this);
		else
			_instance = this;
	}

    private void Start()
    {
        Init();
    }

    private void Init()
    {

        if (gameOverScreen.activeSelf)
        {
            //gameOverScreen.alpha = 0;
            gameOverScreen.SetActive(false);
        }

        if (!playerInstance && PlayerPrefab)
        {
            playerInstance = Instantiate(PlayerPrefab,Vector3.zero,Quaternion.identity).GetComponent<PlayerControllerTest>();
        }

        cameraFollow = Camera.main.gameObject.GetComponent<TestCameraFollow>();
        cameraFollow.followTarget = playerInstance.transform;
        playerInstance.allowControls = true;

        currentScore = 0;
    }

    public void GameOver()
    {
        if (!gameOverScreen.activeSelf)
        {
            //gameOverScreen.alpha = 1;
            gameOverScreen.SetActive(true);
            playerInstance.allowControls = false;
            ObjectSpawnController.Instance.StopSpawner();
            ObjectSpawnController.Instance.ReturnAllObjectsToPool();

            PlayerPrefs.GetInt("Highscore", highscore);
            if (currentScore > highscore)
			{
                PlayerPrefs.SetInt("Highscore", currentScore);
                highscore = currentScore;
			}
            highscoreTxt.text = $"Highscore: {highscore}";
        }
    }

    public void AddScore(int s)
    {
        currentScore += s;
        scoreTxt.text = currentScore.ToString();
    }
}
