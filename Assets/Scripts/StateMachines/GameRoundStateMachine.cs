using System.Collections.Generic;
using Bang.Components;
using Bang.Entities;
using Bang.StateMachines;


namespace GameJam {

	public class GameRoundStateMachine : StateMachine {

		public GameRoundStateMachine() {
			State( BeginCountdown );
		}

		protected override void OnMessage( IMessage message ) {
			if ( message is GameScoreMessage gameScoreMessage ) {
				if ( !Entity.HasGameRoundScore() ) {
					Entity.SetGameRoundScore( 0 );
				}

				var currentScore = Entity.GetGameRoundScore().Score;
				Entity.SetGameRoundScore( currentScore + gameScoreMessage.Score );
			}

			if ( message is GameRoundTimesUpMessage gameRoundTimesUpMessage ) {
				// TODO:
				
			}
		}

		private IEnumerator< Wait > BeginCountdown() {
			yield return Wait.ForSeconds( 1f );
			yield return GoTo( Play );
		}
		
		private IEnumerator< Wait > Play() {
			Entity.SetGameRoundScore( 0 );
			yield return Wait.Stop;
		}
		
		private IEnumerator< Wait > Finish() {
			yield return Wait.Stop;
		}
		
	}

}
