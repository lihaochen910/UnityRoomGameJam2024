using System.Collections.Immutable;
using Bang;
using Bang.Entities;
using Bang.Systems;
using UnityEngine;


namespace GameJam {

	[Watch( typeof( HelloWorldComponent ) )]
	public class HelloWorldReactSystem : IReactiveSystem {

		public void OnAdded( World world, ImmutableArray< Entity > entities ) {
			Debug.Log( "Any HelloWorldComponent Added!" );

			foreach ( var entity in entities ) {
				entity.SendMessage< HelloWorldMessage >();
			}
		}
		
		public void OnRemoved( World world, ImmutableArray< Entity > entities ) {
			Debug.Log( "Any HelloWorldComponent Removed!" );
		}
		
		public void OnModified( World world, ImmutableArray< Entity > entities ) {
			Debug.Log( "Any HelloWorldComponent Modified!" );
		}
		
	}

}
