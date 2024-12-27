using Bang.Components;
using UnityEngine;


namespace GameJam {

	[Requires( typeof( BulletComponent ) )]
	public readonly struct BulletLauncherDirectionComponent : IComponent {

		public readonly Vector2 Dir;
		
		public BulletLauncherDirectionComponent( Vector2 dir ) {
			Dir = dir;
		}

	}

}
