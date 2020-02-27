using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPoolManager
{
	private static volatile ObjectPoolManager mInstance;

	private static object syncRoot = new System.Object();

	private Dictionary<String, ObjectPool> objectPoolList;


	public static ObjectPoolManager Instance
	{
		get
		{
			if (mInstance == null)
			{
				lock (syncRoot)
				{
					if (mInstance == null)
					{
						mInstance = new ObjectPoolManager();
					}
				}
			}

			return mInstance;
		}
	}
		

	private ObjectPoolManager()
	{
		objectPoolList = new Dictionary<String, ObjectPool>();
	}


	public bool CreatePool(GameObject objectForPool, int startPoolSize, int maxPoolSize)
	{
		if(ObjectPoolManager.Instance.objectPoolList.ContainsKey(objectForPool.name))
		{
			return false;
		}
		else
		{
			ObjectPool objPool = new ObjectPool(objectForPool, startPoolSize, maxPoolSize);
			ObjectPoolManager.Instance.objectPoolList.Add(objectForPool.name, objPool);
			return true;
		}
	}


	public bool DestroyPool(GameObject objectForPool)
	{
		if(!ObjectPoolManager.Instance.objectPoolList.ContainsKey(objectForPool.name))
		{
			return false;
		}
		else
		{
			ObjectPoolManager.Instance.objectPoolList[objectForPool.name].DestroyAllObject();
			ObjectPoolManager.Instance.objectPoolList.Remove(objectForPool.name);
			return true;
		}
	}

	public GameObject GetObject(string objectName)
	{
		return ObjectPoolManager.Instance.objectPoolList[objectName].GetObject();
	}

}
