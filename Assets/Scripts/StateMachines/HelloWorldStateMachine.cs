using System.Collections.Generic;
using Bang.StateMachines;
using UnityEngine;


namespace GameJam {

	public class HelloWorldStateMachine : StateMachine {

		public HelloWorldStateMachine() {
			State( Hello );
		}

		private IEnumerator< Wait > Hello() {
			Debug.Log( "HelloWorldStateMachine is waiting!" );
			yield return Wait.ForMessage< HelloWorldMessage >();
			
			Debug.Log( "HelloWorldStateMachine say: hello!" );
			yield return Wait.ForSeconds( 1f );
			
			Debug.Log( "HelloWorldStateMachine say: hi!" );
			yield return Wait.Stop;
		}
		
	}

}
