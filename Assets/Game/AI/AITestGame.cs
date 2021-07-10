namespace SpaceShooterProject.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;
    using SpaceShooterProject.AI.State;
    using UnityEngine;

    public class AITestGame : MonoBehaviour
    {

        [SerializeField]
        Helicopter helicopter;

        [SerializeField]
        StraightRoadTracker straightDummy;

        [SerializeField]
        WaveRoadTracker waveDummy;

        [SerializeField]
        SimpleBoss boss;
        // Start is called before the first frame update
        void Start()
        {
            helicopter.Initialize();
            straightDummy.Initialize();
            waveDummy.Initialize();
            boss.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            helicopter.OnUpdate();
            straightDummy.OnUpdate();
            waveDummy.OnUpdate();
            boss.OnUpdate();
        }
    }
}

