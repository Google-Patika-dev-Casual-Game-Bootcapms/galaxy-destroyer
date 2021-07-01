using System.Collections;
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
}
