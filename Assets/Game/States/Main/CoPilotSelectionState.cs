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
    }

    private void OnCoPilotSelected(CoPilotBase.CoPilotType coPilotType)
    {
        coPilotComponent.SelectCoPilot(coPilotType);
    }
    private void OnCoPilotSelected(CoPilotBase coPilotBase)
    {
        coPilotCanvas.SelectCoPilot(coPilotBase);
    }

    private void OnReturnToMainMenu()
    {
        SendTrigger((int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
    }

    protected override void OnExit()
    {
        coPilotCanvas.OnReturnToMainMenu -= OnReturnToMainMenu;
        coPilotComponent.OnCurrentCoPilotSelectedEvent -= OnCoPilotSelected;
        coPilotCanvas.OnCoPilotSelected -= OnCoPilotSelected;
    }

    protected override void OnUpdate()
    {
        
    }
}