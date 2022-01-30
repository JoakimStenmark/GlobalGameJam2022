using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }

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
            playerInstance.allowControls = true;
        }

        if (!playerInstance && PlayerPrefab)
        {
            playerInstance = Instantiate(PlayerPrefab,Vector3.zero,Quaternion.identity).GetComponent<PlayerControllerTest>();
        }

        cameraFollow = Camera.main.gameObject.GetComponent<TestCameraFollow>();
        cameraFollow.followTarget = playerInstance.transform;

    }

    public void GameOver()
    {
        if (gameOverScreen)
        {
            gameOverScreen.alpha = 1;
            playerInstance.allowControls = false;
        }
    }

}
