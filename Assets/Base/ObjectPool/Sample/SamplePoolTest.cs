namespace Devkit.Base.Pattern.ObjectPool 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SamplePoolTest : MonoBehaviour
    {
        [SerializeField]
        private Pool<SampleObject> pool;

        private const string SOURCE_OBJECT_PATH = "Prefabs/SourceObject";
        // Start is called before the first frame update
        void Start()
        {
            pool = new Pool<SampleObject>(SOURCE_OBJECT_PATH);
            pool.PopulatePool(10);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) 
            {
                var sampleObject = pool.GetObjectFromPool();
            }
        }
    }
}


