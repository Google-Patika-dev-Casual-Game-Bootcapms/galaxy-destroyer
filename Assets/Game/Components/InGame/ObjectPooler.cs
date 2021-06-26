
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem 
{
	[SerializeField]
	private GameObject _objectToPool;
	[SerializeField]
	private int _amountToPool;
	[SerializeField]
	private bool _shouldExpand;

	public GameObject objectToPool
	{
		get
		{
			return _objectToPool;
		}
		set
		{
			
		}
	}
	public int amountToPool
	{
		get
		{
			return _amountToPool;
		}
		set
		{
			
		}
	}
	public bool shouldExpand
	{
		get
		{
			return _shouldExpand;
		}
		set  
		{  
			_shouldExpand = true;  
		}
	}

	public ObjectPoolItem(GameObject objectForPool, int amount, bool expand = true)
	{
		_objectToPool = objectForPool;
		_amountToPool = Mathf.Max(amount,2);
		_shouldExpand = expand;
	}
}

public class ObjectPooler : MonoBehaviour
{
	public static ObjectPooler SharedInstance;
	[SerializeField]
	private List<ObjectPoolItem> itemsToPool;
	[SerializeField]
	private List<List<GameObject>> pooledObjectsList;
	[SerializeField]
	private List<GameObject> pooledObjects;
	private List<int> positions;

	void Awake()
	{

		SharedInstance = this;

		pooledObjectsList = new List<List<GameObject>>();
		pooledObjects = new List<GameObject>();
		positions = new List<int>();


		for (int i = 0; i < itemsToPool.Count; i++)
		{
			ObjectPoolItemToPooledObject(i);
		}

	}


	public GameObject GetPooledObject(int index)
	{

		int curSize = pooledObjectsList[index].Count;
		for (int i = positions[index] + 1; i < positions[index] + pooledObjectsList[index].Count; i++)
		{

			if (!pooledObjectsList[index][i % curSize].activeInHierarchy)
			{
				positions[index] = i % curSize;
				return pooledObjectsList[index][i % curSize];
			}
		}

		if (itemsToPool[index].shouldExpand)
		{

			GameObject obj = (GameObject)Instantiate(itemsToPool[index].objectToPool);
			obj.SetActive(false);
			obj.transform.parent = this.transform;
			pooledObjectsList[index].Add(obj);
			return obj;

		}
		return null;
	}

	public List<GameObject> GetAllPooledObjects(int index)
	{
		return pooledObjectsList[index];
	}


	public int AddObject(GameObject GO, int amount = 3, bool expand = true)
	{
		ObjectPoolItem item = new ObjectPoolItem(GO, amount, expand);
		int currLen = itemsToPool.Count;
		itemsToPool.Add(item);
		ObjectPoolItemToPooledObject(currLen);
		return currLen;
	}


	void ObjectPoolItemToPooledObject(int index)
	{
		ObjectPoolItem item = itemsToPool[index];

		pooledObjects = new List<GameObject>();
		for (int i = 0; i < item.amountToPool; i++)
		{
			GameObject obj = (GameObject)Instantiate(item.objectToPool);
			obj.SetActive(false);
			obj.transform.parent = this.transform;
			pooledObjects.Add(obj);
		}
		pooledObjectsList.Add(pooledObjects);
		positions.Add(0);

	}
}
