using Bang.Components;
using Bang.Entities;
using Bang.Unity;


namespace GameJam {

	[RuntimeOnly]
	[Requires( typeof( BulletLauncherComponent ) )]
	public readonly struct BulletLauncherCurrentBulletComponent : IComponent {

		public readonly Entity Entity;
		
		public BulletLauncherCurrentBulletComponent( Entity entity ) {
			Entity = entity;
		}

	}

}
