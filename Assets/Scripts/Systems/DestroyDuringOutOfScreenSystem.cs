using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using Bang.Unity;
using UnityEngine;


namespace GameJam {

	[Filter( typeof( DestroyDuringOutOfScreenComponent ), typeof( VolumeComponent ) )]
	public class DestroyDuringOutOfScreenSystem : ILateUpdateSystem {

		public void LateUpdate( Context context ) {
			foreach ( var entity in context.Entities ) {
				if ( !entity.HasInCamera() ) {
					entity.RemoveVolume();
					entity.Destroy();
					Debug.Log( $"{Game.Frame} destroy entity: {entity.EntityId}" );
				}
			}
		}
		
	}

}
