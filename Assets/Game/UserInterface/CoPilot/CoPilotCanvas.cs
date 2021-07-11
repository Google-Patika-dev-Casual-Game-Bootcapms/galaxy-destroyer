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
            coPilotAvatarList[0].Activate();
            coPilotAvatarList[1].Deactivate();
            coPilotAvatarList[2].Deactivate();
            coPilotAvatarList[3].Deactivate();
            coPilotAvatarList[4].Deactivate();
            
            coPilotNameContainer.text = "Success!!!";
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


