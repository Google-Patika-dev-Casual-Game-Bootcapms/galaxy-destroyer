namespace SpaceShooterProject.UserInterface
{
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using System;
    using UnityEngine.UI;

    public class GarageUIHexagonUpgrader : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI majorLevelText;
        [SerializeField]
        private List<Image> minorLevelList;
        [SerializeField]
        private TextMeshProUGUI upgradeCost;
        [SerializeField]
        private Sprite activeLinkSprite;
        [SerializeField]
        private Sprite activeCircleSprite;
        [SerializeField]
        private Sprite passiveLinkSprite;
        [SerializeField]
        private Sprite passiveCircleSprite;

        private int minorLevel;
        private int majorLevel;

        public void UpdateMinorAndMajorLevels(int level)
        {
            UpdateMajorLevels(level);
            UpdateMinorLevel(level);
        }

        private void UpdateMinorLevel(int level)
        {
            minorLevel = level % 7;

            for (int i = 0; i < 6; i++)
            {
                if (i < minorLevel)
                    minorLevelList[i].enabled = true;
                else
                    minorLevelList[i].enabled = false;
            }
        }

        private void UpdateMajorLevels(int level)
        {
            majorLevel = level / 7;
            majorLevelText.text = majorLevel.ToString();
        }

        public void UpdateCost(int cost)
        {
            upgradeCost.text = cost.ToString();
        }

        public void SetImages(int cost, int ownedGold)
        {
            if (cost <= ownedGold)
            {
                this.transform.GetChild(1).GetComponent<Image>().sprite = activeLinkSprite;
                this.transform.GetChild(0).GetComponent<Image>().sprite = activeCircleSprite;
            }
            else
            {
                this.transform.GetChild(1).GetComponent<Image>().sprite = passiveLinkSprite;
                this.transform.GetChild(0).GetComponent<Image>().sprite = passiveCircleSprite;
            }
        }
    }

}