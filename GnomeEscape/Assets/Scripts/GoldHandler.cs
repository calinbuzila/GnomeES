using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class GoldHandler : MonoBehaviour {
	public AudioClip audioClip;
    AudioSource audioSource;
	private bool play;
	// Use this for initialization
	void Start () {
		play = true;
		audioSource = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (new Vector3 (0, 70, 0) * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag (SelectionCodes.GameTags.Player.ToString ())) 
		{
			//Debug.Log ("sound");
			if (play) audioSource.PlayOneShot (audioClip);
			play = false;
			this.GetComponent<MeshRenderer> ().enabled = false;
		}
	}
}
