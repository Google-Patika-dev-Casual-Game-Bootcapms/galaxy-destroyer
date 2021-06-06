using Devkit.Base.Component;
using Devkit.HSM;
using SpaceShooterProject.Component;
using SpaceShooterProject.State;
using SpaceShooterProject.UserInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoPilotSelectionState : StateMachine
{
    private UIComponent uiComponent;
    private CoPilotCanvas coPilotCanvas;

    public CoPilotSelectionState(ComponentContainer componentContainer)
    {
        uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
        coPilotCanvas = uiComponent.GetCanvas(UIComponent.MenuName.CO_PILOT) as CoPilotCanvas;
    }

    protected override void OnEnter()
    {
        uiComponent.EnableCanvas(UIComponent.MenuName.CO_PILOT);
        coPilotCanvas.OnReturnToMainMenu += OnReturnToMainMenu;
    }

    private void OnReturnToMainMenu()
    {
        SendTrigger((int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
    }

    protected override void OnExit()
    {
        coPilotCanvas.OnReturnToMainMenu -= OnReturnToMainMenu;
    }

    protected override void OnUpdate()
    {
        
    }
}
