namespace SpaceShooterProject.Component 
{
	using System.Collections;
	using System.Collections.Generic;
	using Devkit.Base.Component;
	using UnityEngine;

	public class CoPilotComponent : IComponent {
		GamePlayComponent gamePlayComponent;
		GameStatesComponent gameStatesComponent;

		public void Initialize(ComponentContainer componentContainer)
		{
			gamePlayComponent = componentContainer.GetComponent("GamePlayComponent") as GamePlayComponent;
			gameStatesComponent = componentContainer.GetComponent("GameStatesComponent") as GameStatesComponent;
		}

		public int[] GetCopilotSetting(){
			//TODO: write setting codes
		}

		public IEnumerable WaitfOritPower(){
			float cooldown = 5.0f;
			float timer = 0f;
			int addBulletPower = 1;
			int maxBulletPower = 10;
			int currentBulletPower = ship.bulletPower;

			while(true){
				
				if(gameStatesComponent.CurrentState !=  gameStatesComponent.Gameplay){
					
					break;
				}
				else{
					if(timer >= cooldown ){
						timer = 0f;
						currentBulletPower += addBulletPower;
						if(currentBulletPower>=maxBulletPower){
							break;
						}
					}
				}
				yield return currentBulletPower;
			}
		}

		
		public void FirethRower(){
			//TODO: write second co-pilot codes
		}

		public void GotyoUrback(){
			//TODO: write third co-pilot codes
		}
		
		public void NowyoUdont(){
			//TODO: write fourth co-pilot codes
		}
		
		public void ShocKwave(){
			//TODO: write fifth co-pilot codes
		}

	}
}