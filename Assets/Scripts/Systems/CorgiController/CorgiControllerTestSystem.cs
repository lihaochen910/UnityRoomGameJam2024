using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using GameJam.Utilities;
using UnityEngine;
using Random = DigitalRune.Mathematics.Random;


namespace GameJam.CorgiController {

	[Filter( typeof( CorgiControllerComponent ) )]
	public class CorgiControllerTestSystem : IUpdateSystem {

		public void Update( Context context ) {
#if DEBUG
			foreach ( var entity in context.Entities ) {
				if ( Input.GetKeyDown( KeyCode.W ) ) {
					var angle = Random.NextFloat( 360f );
					var vector = Vector2.right.Rotate( angle ).normalized * 5f;
					// Debug.Log( $"angle: {angle} vector: {vector}" );
					entity.GetCorgiController().CorgiController.AddForce( vector );
				}
			}
#endif
		}
		
	}

}
