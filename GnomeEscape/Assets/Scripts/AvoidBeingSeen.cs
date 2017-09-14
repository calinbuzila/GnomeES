using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class AvoidBeingSeen : MonoBehaviour {

	GameController mainGameController = null;

	// Use this for initialization
	void Start()
	{
		mainGameController = GameObject.FindObjectOfType<GameController>();
	}

	// Update is called once per frame
	void Update () {

	}

	// on trigger enter if the object that entered the box collider is the player then trigger the help text for avoid being seen
	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag (SelectionCodes.GameTags.Player.ToString ())) 
		{
			mainGameController.SetAvoidBeingSeenToVisible();
		}
	}
	// on trigger exit resets the text and set's it to invisible
	void OnTriggerExit(Collider other) 
	{
		if (other.CompareTag (SelectionCodes.GameTags.Player.ToString ())) 
		{
			mainGameController.SetAvoidBeingSeenToInvisible();
		}
	}
}
