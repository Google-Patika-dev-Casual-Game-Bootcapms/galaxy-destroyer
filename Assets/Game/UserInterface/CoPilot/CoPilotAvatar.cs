namespace SpaceShooterProject.UserInterface 
{
    using Devkit.Base.Object;
    using SpaceShooterProject.UserInterface;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class CoPilotAvatar : MonoBehaviour, IInitializable
    {
        [SerializeField]
        private Image coPilotAvatarImage;

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Init()
        {
#if UNITY_EDITOR
            if (coPilotAvatarImage == null) 
            {
                Debug.LogError("Co pilot image reference is null!!!");
            }
#endif
        }


        public void PreInit()
        {

        }

        //TODO: set co pilot avatar data!!!
    }

}

