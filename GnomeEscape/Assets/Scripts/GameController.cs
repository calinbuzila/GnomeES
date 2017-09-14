using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject finishGamePanel;
    public GameObject player;
	public GameObject movementInstructionsPanel;
	public GameObject avoidBeingSeenPanel;
	public GameObject goodJobPanel;
	public GameObject goodJobPanelCollectingGold;
	bool showAgainMovementInstructions = true;
	bool showAgainBeingSeen = true;
	public bool controlTimer = false;

	public Text countGoldText;
	public Text gameTimer;
	// counter variable used for counting gold coins collected
	private static int counter;

	public float startingTimer;
	public static int gamesPlayed;

    void Awake()
    {
        finishGamePanel.SetActive(false);
		goodJobPanel.SetActive (false);
		goodJobPanelCollectingGold.SetActive (false);
		//SetGoodJobTextAndDisplay ("TESTARE");
		SetAvoidBeingSeenToInvisible ();
    }
    // Use this for initialization
    void Start()
    {
		
		//Debug.Log (gamesPlayed);
		if (gamesPlayed < 1) {
			counter = 0;
			gameTimer.text = "";
		} else {
			Debug.Log (startingTimer);
			if (PlayerPrefs.HasKey ("timer"))
				startingTimer = startingTimer;
			else
			startingTimer = startingTimer * counter;
			PlayerPrefs.SetFloat ("timer", startingTimer);
			Debug.Log (startingTimer + "!!!!!!!");
			counter = 0;
			gameTimer.text = "Timer:";
		}
		if (gamesPlayed >= 5) {
			counter = 0;
			startingTimer = 60;
		}
		showAgainBeingSeen = true;
		countGoldText.text += counter;
    }

    // Update is called once per frame
    void Update()
    {
		if (controlTimer) {
			if (gamesPlayed >= 1 && gamesPlayed < 5) {
				startingTimer -= Time.deltaTime;
				gameTimer.text = startingTimer.ToString ();
				if (startingTimer < 0) {
					gameTimer.text = "";
					CaughtAndStopGame ();
				}
			}
		}

    }

    public void CaughtAndStopGame()
    {
        SceneManager.LoadScene(SelectionCodes.GameScenes.MainScene.ToString());
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SelectionCodes.GameScenes.MainScene.ToString());
    }

    public void FinishedGame()
    {
        finishGamePanel.SetActive(true);
        if (player != null)
        {
            var playerScript = player.GetComponent<dwarfMain_Controller>();
            playerScript.Escaped = true;
			gamesPlayed++;
			controlTimer = false;
        }

        //TODO canvas menu

    }

	public void SetInstructionPanelToInvisible()
	{
		movementInstructionsPanel.SetActive (false);
	}

	public void SetAvoidBeingSeenToVisible()
	{
		//Debug.Log (showAgainBeingSeen);
		if(showAgainBeingSeen == true)
		avoidBeingSeenPanel.SetActive (true);
	}

	public void SetAvoidBeingSeenToInvisible()
	{
		avoidBeingSeenPanel.SetActive (false);
		showAgainBeingSeen = false;
	}

	public void SetGoodJobToVisibleAndSetText(string textToDisplay)
	{
		goodJobPanel.SetActive (true);
		Text textFromPanel = goodJobPanel.GetComponentInChildren<Text> ();

		if(!string.IsNullOrEmpty(textToDisplay)) textFromPanel.text = textToDisplay;

	}

	public void SetGoodJobPanelToInvisible()
	{
		goodJobPanel.SetActive (false);

	}

	public void CountGoldCoins()
	{
		counter++;
		countGoldText.text = "Gold:" + counter;
	}

	public void SetGoodJobCollectingGoldToVisible()
	{
		goodJobPanelCollectingGold.SetActive (true);

	}

	public void SetGoodJobCollectingGoldToInvisible()
	{
		goodJobPanelCollectingGold.SetActive (false);

	}
}
