using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Devkit.Base.Pattern.ObjectPool 
{
    public interface IPoolable
    {
        void Activate();
        void Deactivate();
        void Initialize();
    }

}

