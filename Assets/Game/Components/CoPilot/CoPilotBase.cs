using Devkit.Base.Component;
using UnityEngine;

namespace SpaceShooterProject.Component.CoPilot
{
    public class CoPilotBase
    {
        //todo CoPilot isimleri değişecek
        public enum CoPilotType
        {
            CoPilot1,
            CoPilot2,
            CoPilot3,
            CoPilot4,
            CoPilot5
        }

        public CoPilotBase(CoPilotType targetType)
        {
            coPilotType = targetType;
        }
        
        public CoPilotType coPilotType;
        public virtual void CoPilotUpdate()
        {
            
        }
    }
    
    public class CoPilot1 : CoPilotBase
    {
        public CoPilot1(CoPilotType targetType) : base(targetType)
        {
            
        }

        public override void CoPilotUpdate()
        {
            base.CoPilotUpdate();
            Debug.Log("CO1");
        }
    }

    
}