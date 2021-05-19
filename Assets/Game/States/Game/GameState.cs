namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameState : StateMachine
    {
        private InGameState inGameState;
        private PrepareGameState prepareGameState;
        private PauseGameState pauseGameState;
        private EndGameState endGameState;

        public GameState(ComponentContainer componentContainer)
        {
            prepareGameState = new PrepareGameState(componentContainer);
            inGameState = new InGameState(componentContainer);
            pauseGameState = new PauseGameState(componentContainer);
            endGameState = new EndGameState(componentContainer);

            AddSubState(prepareGameState);
            AddSubState(inGameState);
            AddSubState(pauseGameState);
            AddSubState(endGameState);

            AddTransition(prepareGameState, inGameState, (int)StateTriggers.PLAY_GAME_REQUEST);
            AddTransition(inGameState, pauseGameState, (int)StateTriggers.PAUSE_GAME_REQUEST);
            AddTransition(pauseGameState, inGameState, (int)StateTriggers.RESUME_GAME_REQUEST);
            AddTransition(inGameState, endGameState, (int)StateTriggers.GAME_OVER);
            AddTransition(endGameState, prepareGameState, (int)StateTriggers.REPLAY_GAME_REQUEST);

        }

        protected override void OnEnter()
        {
            
        }

        protected override void OnExit()
        {
            
        }

        protected override void OnUpdate()
        {
            
        }
    }
}
