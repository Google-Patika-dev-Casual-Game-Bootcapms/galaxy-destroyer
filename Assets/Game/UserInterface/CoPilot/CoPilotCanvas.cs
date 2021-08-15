namespace SpaceShooterProject.UserInterface 
{
    using UnityEngine;
    using System;
    using SpaceShooterProject.Component.CoPilot;
    using TMPro;

    public class CoPilotCanvas : BaseCanvas
    {
        public delegate void RequestChangeCoPilotDelegate();
        public delegate void SelectCoPilotDelegate(int coPilotIndex);
        public delegate void  RequestMainMenuDelegate();
        public event RequestChangeCoPilotDelegate OnNextCoPilotRequest;
        public event RequestChangeCoPilotDelegate OnPreviousCoPilotRequest;
        public event SelectCoPilotDelegate OnCoPilotSelected;

        [SerializeField]
        private CoPilotAvatar[] coPilotAvatarList;
        private int activeCoPilotIndex;
        private int currentOnCanvasCoPilot;
        
        [SerializeField]
        private TextMeshProUGUI coPilotNameContainer;
        [SerializeField]
        private TextMeshProUGUI coPilotInfoContainer;


        protected override void Init()
        {
            ActivateCoPilot(0);
            coPilotNameContainer.text = "SOON!!";
            coPilotInfoContainer.text = "It will be amazing things here";
        }

        private void ActivateCoPilot(int selectedCoPilotIndex)
        {
            for (int coPilotIndex = 0; coPilotIndex < coPilotAvatarList.Length; coPilotIndex++)
            {
                coPilotAvatarList[coPilotIndex].Deactivate();
            }

            coPilotAvatarList[selectedCoPilotIndex].Activate();
            activeCoPilotIndex = selectedCoPilotIndex;
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

        public void OnCoPilotSelectButtonClick()
        {
            if (OnCoPilotSelected != null)
            {
                OnCoPilotSelected(activeCoPilotIndex);
            }
        }

        public void SelectCoPilot(CoPilotBase coPilotBase)
        {
            //TODO seçilen co pilot'ı UI'da gösterir!!!
        }

        public void SelectCoPilot(int type)
        {
            activeCoPilotIndex = type;
        }

        public void NextCopilot()
        {
            coPilotAvatarList[activeCoPilotIndex].Deactivate();
            activeCoPilotIndex++;
            if(activeCoPilotIndex == (int)CoPilotBase.CoPilotType.COUNT)
            {
                activeCoPilotIndex = 0;
                coPilotAvatarList[activeCoPilotIndex].Activate();
            }
            else
            {
                coPilotAvatarList[activeCoPilotIndex].Activate();
            }
        }

        public void PreviousPilot()
        {
            coPilotAvatarList[activeCoPilotIndex].Deactivate();
            activeCoPilotIndex--;
            if(activeCoPilotIndex < 0)
            {
                activeCoPilotIndex = (int)CoPilotBase.CoPilotType.COUNT - 1;
                coPilotAvatarList[activeCoPilotIndex].Activate();
            }
            else
            {
                coPilotAvatarList[activeCoPilotIndex].Activate();
            }
        }
    }
}


