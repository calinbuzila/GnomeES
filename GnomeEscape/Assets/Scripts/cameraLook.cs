using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraLook : MonoBehaviour
{
	public float mouseLookSpeedY;
	private float cameraRotationOnYAxis = 0.0f;
    // Use this for initialization
    void Start()
    {
		Screen.lockCursor = true;
    }

    // Update is called once per frame
    void Update()
    {
		cameraRotationOnYAxis -= Input.GetAxis ("Mouse Y");

		cameraRotationOnYAxis = Mathf.Clamp(cameraRotationOnYAxis, -50, 50);

		if (Input.GetKey (KeyCode.Escape))
			Screen.lockCursor = false;

		transform.rotation = Quaternion.Euler (new Vector3 (cameraRotationOnYAxis, this.transform.eulerAngles.y , this.transform.eulerAngles.z));

    }
}
