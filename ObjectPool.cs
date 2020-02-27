using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
	public List<GameObject> objectPoolObject;

	private GameObject pooledObject;
	private int maxPoolSize;
	//private int startPoolSize;

	public ObjectPool(GameObject pooledObject, int startPoolSize, int maxPoolSize)
	{
		objectPoolObject = new List<GameObject>();

		for(int i = 0; i < startPoolSize; i++)
		{
			GameObject obj = GameObject.Instantiate(pooledObject, Vector3.zero, Quaternion.identity);
			obj.SetActive(false);
			objectPoolObject.Add(obj);
		}

		this.pooledObject = pooledObject;
		this.maxPoolSize = maxPoolSize;
		//this.startPoolSize = startPoolSize;
	}

	//! Retrieve a gameobject from the pool.
	public GameObject GetObject()
	{
		for(int i = 0; i < objectPoolObject.Count; i++)
		{
			if(objectPoolObject[i].activeSelf == false)
			{
				objectPoolObject[i].SetActive(true);
				return objectPoolObject[i];
			}
		}

		if(this.maxPoolSize > objectPoolObject.Count)
		{
			GameObject obj = GameObject.Instantiate(pooledObject, Vector3.zero, Quaternion.identity);
			obj.SetActive(true);
			objectPoolObject.Add(obj);
			return obj;
		}

		return null;
	}

	//! Destroy all objects in the pool.
	public void DestroyAllObject()
	{
		for(int i = 0; i < objectPoolObject.Count; i++)
		{
			Object.Destroy(objectPoolObject[i]);
		}

		objectPoolObject.Clear();
		objectPoolObject = null;
	}
}
