using Bang;
using Bang.Entities;
using Bang.Unity;
using UnityEngine;


namespace GameJam.Utilities {

	public static class BangWorldExtensions {
		
		public static Entity? SpawnEntityPrefab( this World world, GameObject prefab ) => Object.Instantiate( prefab ).TryGetEntity();

	}

}
