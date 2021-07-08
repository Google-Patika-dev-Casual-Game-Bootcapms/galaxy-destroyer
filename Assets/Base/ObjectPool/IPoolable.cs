using System.Collections;
using System.Collections.Generic;
using SpaceShooterProject.Component;
using SpaceShooterProject.AI.Projectiles;
using UnityEngine;

namespace Devkit.Base.Pattern.ObjectPool 
{
    public interface IPoolable
    {
        void Activate();
        void Deactivate();
        void Initialize();
        void InjectBulletCollector(BulletCollector bulletCollector);

    }

}

