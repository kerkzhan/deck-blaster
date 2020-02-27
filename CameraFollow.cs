using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform player;

	Vector3 offset;

	void Start () {
		offset = this.transform.position - player.position;
	}
	

	void Update () {
		this.transform.position = player.position + offset;
	}
}
