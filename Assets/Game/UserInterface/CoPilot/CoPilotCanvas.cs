namespace SpaceShooterProject.UserInterface 
{
    using UnityEngine;
    using System;
    using SpaceShooterProject.Component.CoPilot;

    public class CoPilotCanvas : BaseCanvas
    {
        public delegate void RequestNextCoPilotDelegate();
        public delegate void SelectCoPilotDelegate(CoPilotBase.CoPilotType coPilotType);
        public event RequestNextCoPilotDelegate OnNextCoPilotRequest;
        public event RequestNextCoPilotDelegate OnPreviousCoPilotRequest;
        public event SelectCoPilotDelegate OnCoPilotSelected;

        [SerializeField]
        private CoPilotAvatar[] coPilotAvatarList;
        private CoPilotAvatar selectedCoPilot;

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
                OnCoPilotSelected((CoPilotBase.CoPilotType) coPilotId);
            }
        }

        public void SelectCoPilot(CoPilotBase coPilotBase)
        {
            //Seçilen Copilot UI'da gösterilecek
        }

        public void SelectCoPilot(int type)
        {
            
        }
    }
}


