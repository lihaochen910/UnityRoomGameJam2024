using Bang;
using Bang.Components;
using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using Bang.Unity;
using UnityEngine;


namespace GameJam {

	[Filter( ContextAccessorFilter.AnyOf, ContextAccessorKind.Write, typeof( HelloWorldComponent ) )]
	[Messager( typeof( HelloWorldMessage ) )]
	public class HelloWorldSystem : IStartupSystem, IUpdateSystem, IMessagerSystem {

		public void Start( Context context ) {
			Debug.Log( "Hello World!" );
		}
		
		public void Update( Context context ) {
			if ( !context.HasAnyEntity ) {
				return;
			}
			
			// Debug.Log( $"[HelloWorldSystem] update: {Game.Frame}" );
		}

		public void OnMessage( World world, Entity entity, IMessage message ) {
			if ( message is HelloWorldMessage helloWorldMessage ) {
				// do something
				Debug.Log( "HelloWorldMessage Caught!" );
			}
		}
		
	}

}
