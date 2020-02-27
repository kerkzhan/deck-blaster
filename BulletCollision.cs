using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour {

	public AmmoCard ammoEffect;
	public float bulletSpeed;
	public float bulletLifetime = 2.0f;

	public TrailRenderer trailRenderer;
	public Rigidbody rb;
	Vector3 previousPosition;

	//int richochetAmount = 2;
	void Awake () {
		rb = this.GetComponent<Rigidbody>();
		trailRenderer = this.GetComponent<TrailRenderer>();
	}

	void Update () {
		
	}

	void FixedUpdate () {

		RaycastHit hit;
			
		if (Physics.Linecast (previousPosition, this.transform.position, out hit)) {
			if (hit.transform.gameObject.tag == "Target") {
				//Destroy (hit.transform.gameObject);

				ammoEffect.Action(hit, this.gameObject);
			}
		}

		previousPosition = this.transform.position;
	}

	public void BulletSetup()
	{
		previousPosition = this.transform.position;
		rb.velocity = rb.velocity * bulletSpeed;
		StartCoroutine(DeactivateSelf());
	}

	IEnumerator DeactivateSelf()
	{
		yield return new WaitForSeconds(bulletLifetime);
		gameObject.GetComponent<TrailRenderer>().Clear();
		gameObject.SetActive(false);
	}

}
		

		
