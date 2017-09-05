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
    void Awake()
    {
        finishGamePanel.SetActive(false);
		goodJobPanel.SetActive (false);
		SetGoodJobTextAndDisplay ("TESTARE");
		SetAvoidBeingSeenToInvisible ();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        }

        //TODO canvas menu

    }

	public void SetInstructionPanelToInvisible()
	{
		movementInstructionsPanel.SetActive (false);
	}

	public void SetAvoidBeingSeenToVisible()
	{
		avoidBeingSeenPanel.SetActive (true);
	}

	public void SetAvoidBeingSeenToInvisible()
	{
		avoidBeingSeenPanel.SetActive (false);
	}

	public void SetGoodJobTextAndDisplay(string textToDisplay)
	{
		goodJobPanel.SetActive (true);
		Text textFromPanel = goodJobPanel.GetComponentInChildren<Text> ();
		textFromPanel.text = textToDisplay;

	}

	public void SetGoodJobPanelToInvisible()
	{
		goodJobPanel.SetActive (false);

	}
}
