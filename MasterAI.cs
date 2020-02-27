using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAI : MonoBehaviour 
{
	private static MasterAI mInstance;

	public static MasterAI Instance
	{
		get
		{
			if(mInstance == null)
			{
				if(GameObject.FindWithTag("MasterAI") != null)
				{
					mInstance = GameObject.FindWithTag("MasterAI").GetComponent<MasterAI>();
				}
				else
				{
					GameObject masterAI = new GameObject("_MasterAI");
					masterAI.AddComponent<MasterAI>();
					masterAI.tag = "MasterAI";
				}
			}

			return mInstance;
		}
	}

	public GameObject player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
