using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject finishGamePanel;
    public GameObject player;
	public GameObject movementInstructions;
    void Awake()
    {
        finishGamePanel.SetActive(false);
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
		movementInstructions.SetActive (false);
	}
}
