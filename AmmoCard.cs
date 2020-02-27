using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCard : CardClass {

	[Space]
	public bool explosion;
	public bool chain;
	public bool slow;

	[HideInInspector]
	public float chainRadius;
	[HideInInspector]
	public int chainAmount;
	int chainRefresh;

	[HideInInspector]
	public float explosionRadius;
	[HideInInspector]
	public GameObject explosionPrefab;

	public AmmoCard() 
	{
		cardType = CardType.Ammo;
	}

	void OnEnable() 
	{
		chainRefresh = chainAmount;
	}
		
	void Explode(RaycastHit hit, GameObject bullet) 
	{

		Collider[] colliders = Physics.OverlapSphere(hit.point, explosionRadius);

		for (int i = 0; i < colliders.Length; i++) 
		{
			if (colliders[i].gameObject.tag == "Target") 
			{
				Destroy(colliders[i].gameObject);
			}
		}
	
	}

	void Chain(RaycastHit hit, GameObject bullet) 
	{

		if (chainRefresh > 0)
		{
			Collider[] colliders = Physics.OverlapSphere(hit.point, chainRadius);

			Transform closestEnemy = null;
			float closestDist = 0;

			if (colliders.Length > 0) 
			{
				for (int i = 0; i < colliders.Length; i++) 
				{

					if (colliders[i].gameObject.tag == "Target") 
					{
						if (colliders[i].gameObject != hit.collider.gameObject) 
						{
							float tempDist = Vector3.Distance(hit.collider.gameObject.transform.position, colliders[i].gameObject.transform.position);

							if (closestEnemy == null) 
							{
								closestEnemy = colliders[i].transform;
								closestDist = tempDist;
							} 
							else 
							{
								if (tempDist < closestDist) 
								{
									closestEnemy = colliders[i].transform;
									closestDist = tempDist;
								}
							}
						}

					}
				}

				if (closestEnemy != null) 
				{

					Vector3 chainDirection = new Vector3();
					chainDirection = (closestEnemy.transform.position - hit.point).normalized;

					bullet.GetComponent<Rigidbody>().position = hit.point;
					bullet.GetComponent<Rigidbody>().velocity = chainDirection * bullet.GetComponent<BulletCollision>().bulletSpeed;
					chainRefresh--;
					return;

				} 

				/*else
				{
					bullet.GetComponent<TrailRenderer>().Clear();
					bullet.gameObject.SetActive(false);
					chainRefresh = chainAmount;
				}*/
			}

			/*else 
			{
				bullet.GetComponent<TrailRenderer>().Clear();
				bullet.gameObject.SetActive(false);
				chainRefresh = chainAmount;
			}*/

		}

		/*else 
		{
			bullet.GetComponent<TrailRenderer>().Clear();
			bullet.gameObject.SetActive(false);
			chainRefresh = chainAmount;
		}*/

		bullet.GetComponent<TrailRenderer>().Clear();
		bullet.gameObject.SetActive(false);
		chainRefresh = chainAmount;
			
	}

	void Slow(RaycastHit hit, GameObject bullet) 
	{
		hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
	}



	public void Action(RaycastHit hit, GameObject bullet) 
	{
		if (explosion) 
		{
			GameObject go = GameObject.Instantiate(explosionPrefab, hit.point, Quaternion.identity);
			Destroy(go, 1f);
			Explode(hit, bullet);
			bullet.GetComponent<TrailRenderer>().Clear();
			bullet.gameObject.SetActive(false);
			Destroy(hit.collider.gameObject);
		} 

		else if (chain) 
		{
			Chain(hit, bullet); 
			Destroy(hit.collider.gameObject);
		} 

		else if (slow) 
		{
			Slow(hit, bullet);
			bullet.GetComponent<TrailRenderer>().Clear();
			bullet.gameObject.SetActive(false);
		} 

	}
}
