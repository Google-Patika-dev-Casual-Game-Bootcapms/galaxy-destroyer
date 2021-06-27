namespace SpaceShooterProject.Component 
{
    using System.Collections;
    using System.Collections.Generic;
    using Devkit.Base.Component;
    using UnityEngine;

    public class GameStatesComponent : IComponent
    {
        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>TutorialComponent is initialized!</color>");
        }
    }

    public enum GameState 
    { 
        NONE = 0,
        SPLASH = 1,
        MAIN_MENU = 2,
        MARKET = 3,
        SPACE_SHIP_SELECTION_SCENE,

    }
}


