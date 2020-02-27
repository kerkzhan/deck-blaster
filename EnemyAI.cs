using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour 
{
	public GameObject player;

	public float maxMovementSpeed = 2.0f;

	public float distanceToAttack = 20.0f; // Total distance for enemy to start attacking.
	public float distanceToDetect = 30.0f; // Total distance for enemy to start chasing character.

	public float shootRate = 2.0f;
	public float shootAccuracy = 1.0f;
	public float shootChance = 0.75f;// The total chance for enemy to attack the player.

	NavMeshAgent navAgent;
	string coinName = "Coin";
	float health = 100.0f;
	bool inRangeToShoot;

	// Use this for initialization
	void Start ()
	{
		//player = GameObject.FindWithTag("Player");
		//player = MasterAI.Instance.player;
		navAgent = this.GetComponent<NavMeshAgent>();
		navAgent.speed = maxMovementSpeed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void EnemyAIBehavior()
	{
		if(navAgent.enabled)
		{
			float distance = Vector3.Distance(player.transform.position, this.transform.position);
			bool canChase = (distance <= distanceToDetect);

			if(canChase)
			{
				navAgent.SetDestination(player.transform.position);

				if(distance < distanceToAttack)
					inRangeToShoot = true;
				else
					inRangeToShoot = false;
				
			}
		}

		if(inRangeToShoot)
		{
			EnemyShoot();
		}

		//if(health <= 0.0f)

			//Death();
		//}
	}

	void EnemyShoot()
	{
		float chance = Random.Range(0.0f, 1.0f);
		if(chance > 1.0f - shootChance)
		{
			//! Shoot Player
		}
	}

	void DropMoney()
	{
		GameObject coin = ObjectPoolManager.Instance.GetObject(coinName);
		coin.transform.position = this.transform.position;
	}
		

	void Death()
	{
		DropMoney();
		Destroy(gameObject);
			//! Object pool method;
		//gameObject.SetActive(false);
	}


	public void ReceiveDamage(float damage)
	{
		health -= damage;
	}
}
