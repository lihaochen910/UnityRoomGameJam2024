using System.Collections.Immutable;
using Bang;
using Bang.Entities;
using Bang.Systems;
using UnityEngine;


namespace GameJam {

	[Watch( typeof( EggVolumeIncrementComponent ) )]
	[Filter( typeof( EggComponent ) )]
	public class EggVolumeIncreaseSystem : IReactiveSystem {

		public void OnAdded( World world, ImmutableArray< Entity > entities ) {
			foreach ( var entity in entities ) {
				UpdateEntity( entity );
			}
		}

		public void OnRemoved( World world, ImmutableArray< Entity > entities ) {}

		public void OnModified( World world, ImmutableArray< Entity > entities ) {
			foreach ( var entity in entities ) {
				UpdateEntity( entity );
			}
		}

		private void UpdateEntity( Entity entity ) {
			if ( entity.TryGetGameObjectReference() is {} gameObjectReference ) {
				gameObjectReference.GameObject.transform.localScale = Vector3.one * entity.GetEggVolumeIncrement().Increment;
			}
		}
		
	}

}
