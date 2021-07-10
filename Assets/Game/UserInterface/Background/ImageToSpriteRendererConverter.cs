



using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace Game.UserInterface.Background
{
    using UnityEngine;
    using UnityEngine.UI;
    [RequireComponent(typeof(Image), typeof(CanvasRenderer))]
    public class ImageToSpriteRendererConverter : MonoBehaviour
    {

        public bool destroyAtFinish;
        public Vector3 offset = new Vector3(-540, -960, 0);
        public Vector3 scale = new Vector3(0.044f, 0.044f, 0.044f);
        
        private void Awake()
        {
            var targetImage = GetComponent<Image>();
            ConvertToSprite(targetImage);
        }



        public void ConvertToSprite(Image targetImage)
        {
            var targetSprite = targetImage.sprite;
            var newObject = new GameObject();
            newObject.name = targetImage.name+"_Sprite";
            newObject.transform.SetParent(targetImage.transform.parent);
            newObject.transform.localPosition = offset;
            newObject.transform.localScale = scale;
            var newSpriteRenderer =newObject.AddComponent<SpriteRenderer>();
            newSpriteRenderer.sprite = targetSprite;
            if (destroyAtFinish)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
            
        }
        
    }
    
    [CustomEditor(typeof(ImageToSpriteRendererConverter))]
    public class ConverterEditor : Editor
    {
        const string resourceFilename = "custom-editor-uie";
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ImageToSpriteRendererConverter converter = (ImageToSpriteRendererConverter) target;

            //DrawDefaultInspector();
            GUILayout.Space(25);
            if (GUILayout.Button("Convert"))
            {
                converter.ConvertToSprite(converter.GetComponent<Image>());
            }

        }
    }
    
}
