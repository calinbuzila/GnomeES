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
		// locks the cursor of the mouse, it is not shown
		Screen.lockCursor = true;
    }

    // Update is called once per frame
    void Update()
    {
		// get the users mouse vertical movement and it is inverted so that when the user moves the mouse forward he we look down or vice-versa
		cameraRotationOnYAxis -= Input.GetAxis ("Mouse Y");

		// limit the rotation by using the transform rotation component in the inspector to +-50 units
		cameraRotationOnYAxis = Mathf.Clamp(cameraRotationOnYAxis, -50, 50);

		//if escape key is pressed then the cursor is shown
		if (Input.GetKey (KeyCode.Escape))
			Screen.lockCursor = false;

		//only on the vertical axis the rotation is applied using euler angles and the vertical mouse movement
		transform.rotation = Quaternion.Euler (new Vector3 (cameraRotationOnYAxis, this.transform.eulerAngles.y , this.transform.eulerAngles.z));

    }
}
