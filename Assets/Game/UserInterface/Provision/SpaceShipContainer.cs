namespace SpaceShooterProject.Data 
{
    using UnityEngine;
    [CreateAssetMenu(fileName = "SpaceShipData", menuName = "ScriptableObjects/SpaceShipObject", order = 1)]
    public class SpaceShipContainer : ScriptableObject
    {
        [SerializeField]
        private string[] spaceShipNameArray;

        [SerializeField] private Sprite[] spaceShipImageArray;

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

        public Sprite GetSpaceShipImage(int spaceShipId)
        {
            if (spaceShipId < -1) 
            {
                return null;
            }

            if (spaceShipId > spaceShipNameArray.Length - 1) 
            {
                return null;
            }

            return spaceShipImageArray[spaceShipId];
        }
    }
}

