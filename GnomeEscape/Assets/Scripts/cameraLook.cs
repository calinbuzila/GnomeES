using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraLook : MonoBehaviour
{
	public float mouseLookSpeedY;
	public float minRotation;
	public float maxRotation;
    // Use this for initialization
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		var ax = Input.GetAxis ("Mouse Y") * mouseLookSpeedY;
		var axY =0.0f;
		var axZ = 0.0f;
		this.transform.Rotate(new Vector3(-ax,Mathf.Clamp(axY, -1, 1), axZ));
    }
}
