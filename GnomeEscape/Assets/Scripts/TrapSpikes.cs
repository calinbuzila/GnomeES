using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class TrapSpikes : MonoBehaviour {

	public Vector3 initialPos;
	public Vector3 finalPos;
	public bool moveUp = true;

	// Use this for initialization
	void Start () {
		
	}

	//spikes, the same as gold coins rotation they move up and down over time, dependend on framerate
	void Update () {
		//this.transform.position += new Vector3 (this.transform.position.x, 1, this.transform.position.z) * Time.deltaTime;

		//Debug.Log (finalPos.y);
		if (moveUp) {
			if (this.transform.position.y < finalPos.y) {
				transform.Translate (Vector2.up * 0.2f * Time.deltaTime);
			} else {
				moveUp = false;
			}
		} else 
		{
			if (this.transform.position.y < initialPos.y) {
				moveUp = true;
			}
			transform.Translate (Vector3.down * 0.2f * Time.deltaTime);
		}
	}


}
