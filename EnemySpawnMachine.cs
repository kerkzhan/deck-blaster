using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnMachine : MonoBehaviour 
{
	/*private static EnemySpawnMachine mInstance;

	public static EnemySpawnMachine Instance
	{
		get
		{
			if(mInstance == null)
			{
				if(GameObject.FindWithTag("SpawnMachine") != null)
				{
					mInstance = GameObject.FindWithTag("SpawnMachine").GetComponent<EnemySpawnMachine>();
				}
				else
				{
					GameObject masterAI = new GameObject("_EnemySpawnMachine");
					masterAI.AddComponent<EnemySpawnMachine>();
					masterAI.tag = "SpawnMachine";
				}
			}

			return mInstance;
		}
	}*/

	public GameObject enemy;
	public GameObject coin;
	public Vector3[] spawnPoints;

	public float spawnRate = 10.0f;
	public int spawnAmount = 2;

	int pointNumber;

	void Awake()
	{
		pointNumber = spawnPoints.Length;
	}

	// Use this for initialization
	void Start () 
	{
		ObjectPoolManager.Instance.CreatePool(coin, 30, 50);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
