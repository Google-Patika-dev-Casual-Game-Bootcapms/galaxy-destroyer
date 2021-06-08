using System.Collections;
using System.Collections.Generic;
using Devkit.Base.Object;
using UnityEngine;

namespace SpaceShooterProject.Component{
    public class ScreenBounds : Player
    {
        public void Bounds(){
            gameObject.transform.position = new Vector2(Mathf.Clamp(gameObject.transform.position.x,0f,600f),
                                                    Mathf.Clamp(gameObject.transform.position.y,0f,1000f));

        }

    }
}