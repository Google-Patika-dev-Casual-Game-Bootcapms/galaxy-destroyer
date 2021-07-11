namespace SpaceShooterProject.Component.CoPilot
{
    using System.Collections;
    using Devkit.Base.Component;
    using UnityEngine;

    public class CoPilotBase : MonoBehaviour, IComponent
    {
        public GamePlayComponent gamePlayComponent;

        //todo CoPilot isimleri değişecek
        public enum CoPilotType
        {
            CoPilot1,
            CoPilot2,
            CoPilot3,
            CoPilot4,
            CoPilot5,
            COUNT
        }

        public CoPilotBase(CoPilotType targetType)
        {
            coPilotType = targetType;
        }

        public CoPilotType coPilotType;
        public virtual void CoPilotUpdate()
        {
            
        }

        public void Initialize(ComponentContainer componentContainer)
        {
            gamePlayComponent = componentContainer.GetComponent("GamePlayComponent") as GamePlayComponent;
        }
    }
    
    public class CoPilot1 : CoPilotBase
    {
        public CoPilot1(CoPilotType targetType) : base(targetType)
        {
            
        }

        public IEnumerator Copilot1PowerOn(float timer){
            
            yield return new WaitForSeconds(timer);
            //gamePlayComponent.weaponUpgradeComponent.UpgradeWeaponLevel();
            Debug.Log("I Am Alive!!");
        }

        public override void CoPilotUpdate()
        {
            float timer = 5f;
            base.CoPilotUpdate();
            Debug.Log("CO1");
            //TODO Eğer gemi vurulursa süre sıfırlanacak
        	StartCoroutine(Copilot1PowerOn(timer));
        }
    }

    public class CoPilot2 : CoPilotBase
    {
        public CoPilot2(CoPilotType targetType) : base(targetType)
        {
            
        }

        public override void CoPilotUpdate()
        {
            base.CoPilotUpdate();
            Debug.Log("CO2");
        	
        }
    }

    public class CoPilot3 : CoPilotBase
    {
        public CoPilot3(CoPilotType targetType) : base(targetType)
        {
            
        }

        public override void CoPilotUpdate()
        {
            base.CoPilotUpdate();
            Debug.Log("CO3");
        	
        }
    }

    public class CoPilot4 : CoPilotBase
    {
        public CoPilot4(CoPilotType targetType) : base(targetType)
        {
            
        }

        public override void CoPilotUpdate()
        {
            base.CoPilotUpdate();
            Debug.Log("CO4");
        	
        }
    }

    public class CoPilot5 : CoPilotBase
    {
        public CoPilot5(CoPilotType targetType) : base(targetType)
        {
            
        }

        public override void CoPilotUpdate()
        {
            base.CoPilotUpdate();
            Debug.Log("CO5");
        	
        }
    }
}