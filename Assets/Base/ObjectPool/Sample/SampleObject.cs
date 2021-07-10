namespace Devkit.Base.Pattern.ObjectPool 
{
    using System.Collections;
    using System.Collections.Generic;
    using SpaceShooterProject.Component;
    using UnityEngine;

    public class SampleObject : MonoBehaviour, IPoolable
    {
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Initialize()
        {
            //TODO get components
            //TODO set initial colors
        }

        public void InjectBulletCollector(BulletCollector bulletCollector)
        {
            throw new System.NotImplementedException();
        }
    }
}


