using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class InstructionsTrigger : MonoBehaviour {

	GameController mainGameController = null;

	// Use this for initialization
	void Start()
	{
		mainGameController = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag (SelectionCodes.GameTags.Player.ToString ())) {
			mainGameController.SetInstructionPanelToInvisible ();
		}
	}
}
