using System.Collections;
using System.Collections.Generic;
using Devkit.Base.Component;
using UnityEngine;

namespace SpaceShooterProject.Component.CoPilot
{
	public class CoPilotComponent : IComponent {
		
		GamePlayComponent gamePlayComponent;
		GameStatesComponent gameStatesComponent;

		[SerializeField] private List<CoPilotBase> allCoPilotsList = new List<CoPilotBase>();

		private Dictionary<CoPilotBase.CoPilotType, CoPilotBase> coPilotDict =
			new Dictionary<CoPilotBase.CoPilotType, CoPilotBase>();
		
		private CoPilotBase activeCoPilot;

		public delegate void CurrentCoPilotSelectedDelegate(CoPilotBase coPilotBase);
		public event CurrentCoPilotSelectedDelegate OnCurrentCoPilotSelectedEvent;
		
		public void Initialize(ComponentContainer componentContainer)
		{
			gamePlayComponent = componentContainer.GetComponent("GamePlayComponent") as GamePlayComponent;
			gameStatesComponent = componentContainer.GetComponent("GameStatesComponent") as GameStatesComponent;
			
			foreach (var coPilotBase in allCoPilotsList)
			{
				coPilotDict.Add(coPilotBase.coPilotType,coPilotBase);
			}
			
		}

		public void SelectCoPilot(CoPilotBase.CoPilotType targetCoPilotType)
		{
			activeCoPilot = coPilotDict[targetCoPilotType];

			if(OnCurrentCoPilotSelectedEvent != null)
			{
				OnCurrentCoPilotSelectedEvent(activeCoPilot);
			}
		}

		public void CoPilotUpdate()
		{
			if (activeCoPilot == null)
			{
				return;
			}
			activeCoPilot.CoPilotUpdate();
		}

	}
}