using UnityEngine;

namespace SpaceShooterProject.Component.CoPilot
{
    [CreateAssetMenu(fileName = "CoPilot",menuName = "CoPilots/CoPilotBase")]
    public class CoPilotBase : ScriptableObject
    {
        public enum CoPilotType
        {
            CoPilot1,
            CoPilot2,
            CoPilot3,
            CoPilot4,
            CoPilot5
        }

        public CoPilotType coPilotType;
        
    }
}