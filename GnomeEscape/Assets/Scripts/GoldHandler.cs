using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class GoldHandler : MonoBehaviour {
	public AudioClip audioClip;
    AudioSource audioSource;
	private bool play;
	GameController mainGameController = null;

	// Use this for initialization
	void Start () {
		play = true;
		audioSource = this.GetComponent<AudioSource> ();
		mainGameController = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (new Vector3 (0, 70, 0) * Time.deltaTime);
	}
	// when the player collects the coin a sound is played and also in time a rotation is applied to the gold coin(Update() method)
	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag (SelectionCodes.GameTags.Player.ToString ())) 
		{
			//Debug.Log ("sound");
			if (play) audioSource.PlayOneShot (audioClip);
			play = false;
			this.GetComponent<MeshRenderer> ().enabled = false;
			if (mainGameController != null)
			{
				mainGameController.CountGoldCoins();
				mainGameController.SetGoodJobCollectingGoldToVisible ();
				StartCoroutine (GoodJobCollectingGold());
			}

		}
	}

	IEnumerator GoodJobCollectingGold()
	{
		yield return new WaitForSeconds (1);
		mainGameController.SetGoodJobCollectingGoldToInvisible();
	}
		
}
