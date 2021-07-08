namespace Devkit.Base.Pattern.ObjectPool 
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Pool<ObjectType> 
        where ObjectType : IPoolable
    {
        protected Queue<ObjectType> pool;
        [SerializeField]
        private GameObject sourcePrefab;
        private string sourceObjectPath;

        private Pool() { }

        public Pool(string sourceObjectPath) 
        {
            this.sourceObjectPath = sourceObjectPath;
        }

        public void PopulatePool(int initialPoolSize)
        {
            pool = new Queue<ObjectType>();

            for (int i = 0; i < initialPoolSize; i++)
            {
                GenerateObject();
            }
        }

        public void AddObjectToPool(ObjectType poolable)
        {
            poolable.Deactivate();
            pool.Enqueue(poolable);
        }

        public ObjectType GetObjectFromPool() 
        {
            if (pool.Count == 0) 
            {
                GenerateObject();
            }

            var pooledObject = pool.Dequeue();
            pooledObject.Activate();
            return pooledObject;
        }

        private void GenerateObject() 
        {
            var sourceObject = Resources.Load<GameObject>(sourceObjectPath);
            var poolableObject = GameObject.Instantiate(sourceObject).GetComponent<ObjectType>();

            if (poolableObject == null)
            {
                //TODO: apply nullable design pattern here!!!
                Debug.LogError("Source prefab does not contain any IPoolable");
                return;
            }

            poolableObject.Initialize();
            poolableObject.Deactivate();

            pool.Enqueue(poolableObject);
        }

        public Queue<ObjectType> GetPool => pool;
    }
}


