namespace SpaceShooterProject.Data 
{
    using UnityEngine;
    [CreateAssetMenu(fileName = "SpaceShipNameData", menuName = "ScriptableObjects/SpaceShipNameObject", order = 1)]
    public class SpaceShipNameContainer : ScriptableObject
    {
        [SerializeField]
        private string[] spaceShipNameArray;

        public string GetSpaceShipName(int spaceShipId) 
        {
            if (spaceShipId < -1) 
            {
                return "empty";
            }

            if (spaceShipId > spaceShipNameArray.Length - 1) 
            {
                return "empty";
            }

            return spaceShipNameArray[spaceShipId];
        }
    }
}

