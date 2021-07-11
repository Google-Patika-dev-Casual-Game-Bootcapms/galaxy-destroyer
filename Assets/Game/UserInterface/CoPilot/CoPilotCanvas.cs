namespace SpaceShooterProject.UserInterface 
{
    using UnityEngine;
    using System;
    using SpaceShooterProject.Component.CoPilot;
    using TMPro;

    public class CoPilotCanvas : BaseCanvas
    {
        public delegate void RequestNextCoPilotDelegate();
        public delegate void SelectCoPilotDelegate(CoPilotBase.CoPilotType coPilotType);
        public delegate void  RequestMainMenuDelegate();
        public event RequestNextCoPilotDelegate OnNextCoPilotRequest;
        public event RequestNextCoPilotDelegate OnPreviousCoPilotRequest;
        public event SelectCoPilotDelegate OnCoPilotSelected;

        [SerializeField]
        private CoPilotAvatar[] coPilotAvatarList;
        private CoPilotAvatar selectedCoPilot;
        
        [SerializeField]
        private TextMeshProUGUI coPilotNameContainer;
        [SerializeField]
        private TextMeshProUGUI coPilotInfoContainer;


        protected override void Init()
        {
            ActivateCoPilot(0);
            coPilotNameContainer.text = "Success!!!";
        }

        private void ActivateCoPilot(int selectedCoPilotIndex)
        {
            for (int coPilotIndex = 0; coPilotIndex < coPilotAvatarList.Length; coPilotIndex++)
            {
                coPilotAvatarList[coPilotIndex].Deactivate();
            }

            coPilotAvatarList[selectedCoPilotIndex].Activate();
        }

        public void SetCurrentCoPilotData(/*CoPilotData data*/) //TODO Pass co pilot data into this method!!!
        {
            
        }

        public void OnNextCoPilotButtonClick() 
        {
            if (OnNextCoPilotRequest != null) 
            {
                OnNextCoPilotRequest();
            }
        }

        public void OnPreviousCoPilotButtonClick()
        {
            if (OnPreviousCoPilotRequest != null)
            {
                OnPreviousCoPilotRequest();
            }
        }

        public void OnCoPilotSelectButtonClick(int coPilotId)
        {
            if (OnCoPilotSelected != null)
            {
                OnCoPilotSelected((CoPilotBase.CoPilotType)coPilotId);
            }
        }

        public void SelectCoPilot(CoPilotBase coPilotBase)
        {
            //TODO seçilen co pilot'ı UI'da gösterir!!!
        }

        public void SelectCoPilot(int type)
        {

        }
    }
}


