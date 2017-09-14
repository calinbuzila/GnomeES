using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class SpikeTriggerDeath : MonoBehaviour {

	bool isPlayerDead;
	GameController mainGameController = null;
	// Use this for initialization
	void Start () {
		isPlayerDead = false;
		mainGameController = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (!isPlayerDead) 
		{
			if (other.CompareTag (SelectionCodes.GameTags.Player.ToString ())) 
			{
				isPlayerDead = true;
				if (mainGameController != null)
				{
					mainGameController.CaughtAndStopGame();
				}
			}
		}

	}
}
