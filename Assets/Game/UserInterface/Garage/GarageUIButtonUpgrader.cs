using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GarageUIButtonUpgrader : MonoBehaviour
{
    [SerializeField]
    
    private TextMeshProUGUI majorLevelText;

    private int minorLevel;
    private int majorLevel;

    public void UpdateMinorAndMajorLevels(int level)
    {
        UpdateMajorLevels(level);
        UpdateMinorLevel(level);
    }

    private void UpdateMinorLevel(int level)
    {
        minorLevel = level - majorLevel * 6;
    }

    private void UpdateMajorLevels(int level)
    {
        majorLevel = level / 6;
        majorLevelText.text = majorLevel.ToString();
    }
}
