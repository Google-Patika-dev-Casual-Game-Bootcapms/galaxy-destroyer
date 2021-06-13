using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SpaceShooterProject.Component;

public class AchievementCard : MonoBehaviour
{
    public Image icon;
    public TMP_Text header;
    public TMP_Text description;
    public TMP_Text currentCount;
    public TMP_Text goalCount;
    public Image smallIcon;

    public AchievementCard(Achievement achievement)
    {
        icon.sprite = achievement.Icon;
        header.text = achievement.Name;
        description.text = achievement.Descrption;
        currentCount.text = achievement.CurrentCount.ToString();
        goalCount.text = achievement.GoalCount.ToString();
        smallIcon.sprite = achievement.SmallIcon;
    }
}
