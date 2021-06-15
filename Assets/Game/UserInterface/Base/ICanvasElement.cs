using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterProject.UserInterface 
{
    public interface ICanvasElement
    {
        void Init();
        void Activate();
        void Deactivate();
    }
}


