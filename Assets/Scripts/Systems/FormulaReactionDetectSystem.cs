using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using Bang.Unity;
using UnityEngine;


namespace GameJam {

	[Filter( typeof( FormulaReactSensorComponent ), typeof( BulletComponent ), typeof( VolumeComponent ), typeof( CharComponent ) )]
	[Filter( ContextAccessorFilter.NoneOf, typeof( FormulaReactDisabledComponent ) )]
	public class FormulaReactionDetectSystem : IUpdateSystem {

		private readonly Collider2D[] _collidersCache = new Collider2D[ 10 ];

		public void Update( Context context ) {
			foreach ( var entity in context.Entities ) {
				var volumeComponent = entity.GetVolume();
				var collider = volumeComponent.Collider;

				var hit = false;
				var contactFilter = new ContactFilter2D { useTriggers = true };
				var count = Physics2D.OverlapCollider( collider, contactFilter, _collidersCache );
				for ( var i = 0; i < count; i++ ) {
					var otherCollider = _collidersCache[ i ];
					if ( otherCollider.gameObject &&
						 otherCollider.gameObject.TryGetEntity() is {} otherEntity &&
						 otherEntity.HasEgg() ) {
						
						// Debug.Log( $"react with: {otherCollider.gameObject.name}" );
						otherEntity.SendMessage( new FormulaReactionTriggeredMessage( entity.GetChar().Char, entity, otherEntity ) );
						hit = true;
					}
				}

				if ( hit ) {
					entity.Destroy();
				}
				
			}
		}
		
	}


	[Filter( typeof( EggComponent ), typeof( EggInDashComponent ), typeof( VolumeComponent ), typeof( CharComponent ) )]
	[Filter( ContextAccessorFilter.NoneOf, typeof( FormulaReactDisabledComponent ) )]
	public class EggFormulaReactionDetectSystem : IUpdateSystem {
		
		private readonly Collider2D[] _collidersCache = new Collider2D[ 10 ];

		public void Update( Context context ) {
			
			foreach ( var entity in context.Entities ) {
				var volumeComponent = entity.GetVolume();
				var collider = volumeComponent.Collider;

				var hit = false;
				var contactFilter = new ContactFilter2D { useTriggers = true };
				var count = Physics2D.OverlapCollider( collider, contactFilter, _collidersCache );
				for ( var i = 0; i < count; i++ ) {
					var otherCollider = _collidersCache[ i ];
					if ( otherCollider.gameObject &&
						 otherCollider.gameObject.TryGetEntity() is {} otherEntity &&
						 otherEntity.HasEgg() ) {
						
						// Debug.Log( $"react with: {otherCollider.gameObject.name}" );
						otherEntity.SendMessage( new FormulaReactionTriggeredMessage( entity.GetChar().Char, entity, otherEntity ) );
						hit = true;
					}
				}

				if ( hit ) {
					entity.Destroy();
				}
				
			}
			
		}
		
	}

}
