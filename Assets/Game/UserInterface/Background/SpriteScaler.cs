using System;
using UnityEngine;

namespace Game.UserInterface.Background
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteScaler : MonoBehaviour
    {
        private SpriteRenderer mySpriteRenderer;
        private void Awake()
        {
            
            Scale();
            
        }
        
        public void Scale()
        {
            mySpriteRenderer = GetComponent<SpriteRenderer>();
            var spriteWidth = mySpriteRenderer.sprite.bounds.size.x;
            var spriteHeight = mySpriteRenderer.sprite.bounds.size.y;

            var worldScreenHeight = Camera.main.orthographicSize * 2f;
            var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;


            var localPos = transform.localPosition;
            localPos.x = -Screen.width / 2f;
            localPos.y = -Screen.height / 2f;
            transform.localPosition = localPos;
            
            var localScale = transform.localScale;
            localScale.x = worldScreenWidth / spriteWidth;
            localScale.y = worldScreenHeight / spriteHeight;
            //localScale.x *= 2f;
            transform.localScale = localScale;

        }
        
        
    }
}
