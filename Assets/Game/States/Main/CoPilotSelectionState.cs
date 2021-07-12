using Devkit.Base.Component;
using Devkit.HSM;
using SpaceShooterProject.Component;
using SpaceShooterProject.Component.CoPilot;
using SpaceShooterProject.State;
using SpaceShooterProject.UserInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoPilotSelectionState : StateMachine
{
    private UIComponent uiComponent;
    private CoPilotCanvas coPilotCanvas;
    private CoPilotComponent coPilotComponent;

    public CoPilotSelectionState(ComponentContainer componentContainer)
    {
        coPilotComponent = componentContainer.GetComponent("CoPilotComponent") as CoPilotComponent;
        uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
        coPilotCanvas = uiComponent.GetCanvas(UIComponent.MenuName.CO_PILOT) as CoPilotCanvas;
    }

    protected override void OnEnter()
    {
        uiComponent.EnableCanvas(UIComponent.MenuName.CO_PILOT);
        coPilotCanvas.OnReturnToMainMenu += OnReturnToMainMenu;
        coPilotComponent.OnCurrentCoPilotSelectedEvent += OnCoPilotSelected;
        coPilotCanvas.OnCoPilotSelected += OnCoPilotSelected;
        coPilotCanvas.OnNextCoPilotRequest += OnNextCoPilotButtonClick;
        coPilotCanvas.OnPreviousCoPilotRequest += OnPreviousCoPilotButtonClick;
        coPilotCanvas.OnCoPilotSelected += OnSelectCoPilotButtonClick;
    }

    private void OnReturnToMainMenu()
    {
        SendTrigger((int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
    }

    private void OnCoPilotSelected(CoPilotBase coPilotBase)
    {
        coPilotCanvas.SelectCoPilot(coPilotBase);
    }

    private void OnCoPilotSelected(int coPilotType)
    {
        coPilotComponent.SelectCoPilot((CoPilotBase.CoPilotType)coPilotType);
    }

    private void OnNextCoPilotButtonClick()
    {
        coPilotCanvas.NextCopilot();
    }

    private void OnPreviousCoPilotButtonClick()
    {
        coPilotCanvas.PreviousPilot();
    }

    private void OnSelectCoPilotButtonClick(int coPilotIndex)
    {
        coPilotComponent.SelectCoPilot((CoPilotBase.CoPilotType)coPilotIndex);
    }

    protected override void OnExit()
    {
        coPilotCanvas.OnReturnToMainMenu -= OnReturnToMainMenu;
        coPilotComponent.OnCurrentCoPilotSelectedEvent -= OnCoPilotSelected;
        coPilotCanvas.OnCoPilotSelected -= OnCoPilotSelected;
        coPilotCanvas.OnNextCoPilotRequest -= OnNextCoPilotButtonClick;
        coPilotCanvas.OnPreviousCoPilotRequest -= OnPreviousCoPilotButtonClick;
        coPilotCanvas.OnCoPilotSelected -= OnSelectCoPilotButtonClick;
    }

    protected override void OnUpdate()
    {
        
    }
}