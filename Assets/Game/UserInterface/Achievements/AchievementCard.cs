using System.Collections;
using System.Collections.Generic;
using Devkit.Base.Object;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SpaceShooterProject.Component;

public class AchievementCard : MonoBehaviour, IInitializable
{
    [SerializeField] private Image icon;
    [SerializeField] public TMP_Text header;
    [SerializeField] private TMP_Text description;
    [SerializeField] public TMP_Text currentCount;
    [SerializeField] public TMP_Text goalCount;
    [SerializeField] private Image smallIcon;
    [SerializeField] public Button collectButton;

    private Achievement data = null;

    public Achievement Data
    {
        get => data;
        set => data = value;
    }

    public void PreInit()
    {
    }

    public void Init()
    {
        icon.sprite = data.Icon;
        header.text = data.Name;
        description.text = data.Descrption;
        currentCount.text = data.CurrentCount.ToString();
        goalCount.text = data.GoalCount.ToString();
        smallIcon.sprite = data.SmallIcon;
    }
}
