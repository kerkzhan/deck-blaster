using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Look Angle Variables
	public float minX = -90f;
	public float maxX = 90f;
	public float minY = -360f;
	public float maxY = 360f;

	// Mouse sens
	public float sensitivityX = 15f;
	public float sensitivityY = 15f;

	Camera mainCam;

	float rotationX;
	float rotationY;

	void Start () {
		mainCam = Camera.main;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update () {
		rotationX += Input.GetAxis ("Mouse Y") * sensitivityX;
		rotationY += Input.GetAxis ("Mouse X") * sensitivityY;

		rotationX = Mathf.Clamp(rotationX, minX, maxX);

		this.transform.localEulerAngles = new Vector3 (0f, rotationY, 0f);
		mainCam.transform.localEulerAngles = new Vector3 (-rotationX, rotationY, 0f);

		//Enable Cursor
		if (Input.GetKeyDown(KeyCode.Tab)){
			ToggleCursorLock();
		}
	}

	void ToggleCursorLock () {
		if (Cursor.lockState == CursorLockMode.Locked) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} else {
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
