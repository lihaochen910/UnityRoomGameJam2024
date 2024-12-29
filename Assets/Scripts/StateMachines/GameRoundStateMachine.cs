using System;
using System.Collections.Generic;
using Bang;
using Bang.Components;
using Bang.Entities;
using Bang.StateMachines;


namespace GameJam {

	public class GameRoundStateMachine : StateMachine {

		public GameRoundStateMachine() {
			State( Setup );
		}

		private void SetActiveGameRoundSystems( bool active ) {
			if ( active ) {
				World.ActivateSystem< EggGeneratorSystem >();
				World.ActivateSystem< BulletLauncherSystem >();
				World.ActivateSystem< FormulaReactSystem >();
				World.AddEntity( new EggGeneratorComponent() );
			}
			else {
				World.DeactivateSystem< EggGeneratorSystem >();
				World.DeactivateSystem< BulletLauncherSystem >();
				World.DeactivateSystem< FormulaReactSystem >();
				if ( World.TryGetUniqueEntityEggGenerator() is {} eggGenerator ) {
					eggGenerator.Destroy();
				}
			}
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

		private IEnumerator< Wait > Setup() {
			SetActiveGameRoundSystems( false );
			Entity.SetGameRoundScore( 0 );
			Entity.SetGameRoundCountdown( 60f );
			yield return GoTo( BeginCountdown );
		}

		private IEnumerator< Wait > BeginCountdown() {
			var gameRoundUI = Entity.GetGameRoundUI();
			gameRoundUI.Mask.enabled = true;
			gameRoundUI.TextBig.gameObject.SetActive( true );
			gameRoundUI.TextBig.text = "Ready";
			gameRoundUI.TextTimer.text = "00:60";
			gameRoundUI.TextScore.gameObject.SetActive( false );
			yield return Wait.ForSeconds( 1f );
			gameRoundUI.TextBig.text = "3";
			yield return Wait.ForSeconds( 1f );
			gameRoundUI.TextBig.text = "2";
			yield return Wait.ForSeconds( 1f );
			gameRoundUI.TextBig.text = "1";
			yield return Wait.ForSeconds( 1f );
			gameRoundUI.TextBig.gameObject.SetActive( false );
			gameRoundUI.Mask.enabled = false;
			yield return GoTo( Play );
		}
		
		private IEnumerator< Wait > Play() {
			SetActiveGameRoundSystems( true );
			var gameRoundUI = Entity.GetGameRoundUI();
			var time = Entity.GetGameRoundCountdown().Time;
			var timeSpan = TimeSpan.FromSeconds( time );
			while ( time > 0f ) {
				yield return Wait.ForSeconds( 1f );
				time -= 1f;
				timeSpan = TimeSpan.FromSeconds( time );
				Entity.SetGameRoundCountdown( time );
				gameRoundUI.TextTimer.text = string.Format( "{0:D2}:{1:D2}", ( int )timeSpan.TotalMinutes, timeSpan.Seconds );
			}
			
			gameRoundUI.TextTimer.text = "00:00";
			yield return GoTo( Finish );
		}
		
		private IEnumerator< Wait > Finish() {
			SetActiveGameRoundSystems( false );
			
			var gameRoundUI = Entity.GetGameRoundUI();
			gameRoundUI.Mask.enabled = true;
			gameRoundUI.TextBig.gameObject.SetActive( true );
			gameRoundUI.TextBig.text = "Finish!";
			
			yield return Wait.ForSeconds( 1f );
			gameRoundUI.TextScore.gameObject.SetActive( true );
			gameRoundUI.TextScore.text = $"Score: {Entity.GetGameRoundScore().Score.ToString()}";
			yield return Wait.ForSeconds( 2f );
			
			// gameRoundUI.TextBig.text = "Press Escape to Reset Game.";
			yield return Wait.Stop;
		}
		
	}

}
