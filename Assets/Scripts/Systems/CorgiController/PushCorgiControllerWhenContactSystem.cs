using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using Bang.Unity;
using GameJam.MonoBehaviours;
using GameJam.Utilities;
using UnityEngine;
using Random = DigitalRune.Mathematics.Random;


namespace GameJam.CorgiController {

	[Filter( typeof( CorgiControllerComponent ), typeof( FormulaReactDisabledComponent ), typeof( VolumeComponent ) )]
	public class PushCorgiControllerWhenContactSystem : IFixedUpdateSystem {
		
		private readonly Collider2D[] _collidersCache = new Collider2D[ 7 ];

		public void FixedUpdate( Context context ) {

			foreach ( var entity in context.Entities ) {
				// var corgiController = entity.GetCorgiController().CorgiController;
				// if ( corgiController is MyCorgiController myCorgiController ) {
				// 	foreach ( var raycastHit2D in myCorgiController.ContactList ) {
				// 		if ( raycastHit2D.collider.gameObject.TryGetEntity() is {} contactEntity ) {
				// 			if ( contactEntity.TryGetCorgiController() is {} contactCorgiController ) {
				// 				var angle = Random.NextFloat( 360f );
				// 				var force = Random.Range( 0.2f, 1f );
				// 				var vector = Vector2.right.Rotate( angle ).normalized;
				// 				contactCorgiController.CorgiController.AddForce( vector * force );
				// 			}
				// 		}
				// 	}
				// }
				
				var volumeComponent = entity.GetVolume();
				var collider = volumeComponent.Collider;
				
				var contactFilter = new ContactFilter2D { useTriggers = true };
				var count = Physics2D.OverlapCollider( collider, contactFilter, _collidersCache );
				for ( var i = 0; i < count; i++ ) {
					var otherCollider = _collidersCache[ i ];
					if ( otherCollider.gameObject &&
						 otherCollider.gameObject.TryGetEntity() is {} otherEntity &&
						 otherEntity.TryGetCorgiController() is {} contactCorgiController ) {
						var angle = Random.NextFloat( 360f );
						var force = Random.Range( 0.2f, 1f );
						var vector = Vector2.right.Rotate( angle ).normalized;
						contactCorgiController.CorgiController.AddForce( vector * force );
					}
				}
			}

		}
		
	}

}
