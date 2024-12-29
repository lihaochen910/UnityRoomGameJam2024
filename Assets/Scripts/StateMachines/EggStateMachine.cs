using System.Collections.Generic;
using Bang.Entities;
using Bang.StateMachines;
using GameJam.Utilities;
using UnityEngine;
using Random = DigitalRune.Mathematics.Random;


namespace GameJam {

	public class EggStateMachine : StateMachine {

		public EggStateMachine() {
			State( ToScreen );
		}

		private IEnumerator< Wait > ToScreen() {
			yield return GoTo( Boost );
		}

		private IEnumerator< Wait > Boost() {
			var corgiController = Entity.GetCorgiController().CorgiController;
			
			yield return Wait.ForSeconds( 1f );
			
			while ( true ) {
				var angle = Random.NextFloat( 360f );
				var force = Random.Range( 1f, 3f );
				var vector = Vector2.right.Rotate( angle ).normalized;
				corgiController.AddForce( vector * force );
				Entity.SetFormulaReactDisabled();
				Entity.SetFormulaReactDisabledCountdown( 0.2f );
				
				yield return Wait.ForSeconds( Random.Range( 1f, 5f ) );
			}
		}
		
	}

}
