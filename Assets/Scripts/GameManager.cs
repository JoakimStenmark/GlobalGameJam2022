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
    [SerializeField] private int currentScore;

    public CanvasGroup gameOverScreen;
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

        if (gameOverScreen)
        {
            gameOverScreen.alpha = 0;
        }

        if (!playerInstance && PlayerPrefab)
        {
            playerInstance = Instantiate(PlayerPrefab,Vector3.zero,Quaternion.identity).GetComponent<PlayerControllerTest>();
        }

        cameraFollow = Camera.main.gameObject.GetComponent<TestCameraFollow>();
        cameraFollow.followTarget = playerInstance.transform;
        playerInstance.allowControls = true;
    }

    public void GameOver()
    {
        if (gameOverScreen)
        {
            gameOverScreen.alpha = 1;
            playerInstance.allowControls = false;
        }
    }

    public void AddScore(int s)
    {
        currentScore += s;
        scoreTxt.text = currentScore.ToString();
    }
}
