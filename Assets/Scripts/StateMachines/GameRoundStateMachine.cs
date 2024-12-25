using System.Collections.Generic;
using Bang.StateMachines;


namespace GameJam {

	public class GameRoundStateMachine : StateMachine {

		public GameRoundStateMachine() {
			State( BeginCountdown );
		}

		private IEnumerator< Wait > BeginCountdown() {
			
			yield return Wait.ForSeconds( 1f );
			
		}
		
		private IEnumerator< Wait > Play() {
			yield return Wait.Stop;
		}
		
		private IEnumerator< Wait > Finish() {
			yield return Wait.Stop;
		}
		
	}

}
