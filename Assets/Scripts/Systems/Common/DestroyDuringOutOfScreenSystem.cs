using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using Bang.Unity;
using UnityEngine;


namespace GameJam {

	[Filter( typeof( DestroyDuringOutOfScreenComponent ), typeof( VolumeComponent ) )]
	public class DestroyDuringOutOfScreenSystem : IUpdateSystem {

		public void Update( Context context ) {
			foreach ( var entity in context.Entities ) {
				if ( !entity.HasInCamera() ) {
					// entity.RemoveMove();
					// entity.RemoveVolume();
					entity.Destroy();
					Debug.Log( $"{Game.Frame} destroy entity: {entity.EntityId}" );
				}
			}
		}
		
	}

}
