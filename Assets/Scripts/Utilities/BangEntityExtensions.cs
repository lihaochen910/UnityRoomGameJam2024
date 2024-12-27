using Bang.Entities;
using UnityEngine;


namespace GameJam.Utilities {

	public static class BangEntityExtensions {
		
		public static T? TryGetUnityComponent< T >( this Entity e ) where T : Component {
			if ( e.TryGetGameObjectReference() is { GameObject: not null } gameObjectReference ) {
				return gameObjectReference.GameObject.GetComponent< T >();
			}

			return default( T );
		}

		public static void TrySetTransformPosition( this Entity e, Vector3 position ) {
			if ( e.TryGetGameObjectReference() is { GameObject: not null } gameObjectReference ) {
				gameObjectReference.GameObject.transform.position = position;
			}
		}
		
	}

}