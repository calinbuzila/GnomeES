using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class GoodJobFeedback : MonoBehaviour {

	GameController mainGameController = null;
	bool wasShown = false;
	// Use this for initialization
	void Start()
	{
		mainGameController = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other) 
	{
		if(!wasShown)
		{
			if (other.CompareTag (SelectionCodes.GameTags.Player.ToString ())) 
			{
				mainGameController.SetGoodJobToVisibleAndSetText("");
				StartCoroutine (GooJobToInvisible());
			}
		}
	}

	void OnTriggerExit(Collider other) 
	{
		wasShown = true;
	}

	IEnumerator GooJobToInvisible()
	{
		yield return new WaitForSeconds (2);
		mainGameController.SetGoodJobPanelToInvisible();
	}
	
}
