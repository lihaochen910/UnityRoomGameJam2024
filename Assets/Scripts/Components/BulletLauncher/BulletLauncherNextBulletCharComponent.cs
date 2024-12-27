using Bang.Components;


namespace GameJam {

	[Requires(typeof( BulletLauncherComponent ) )]
	public readonly struct BulletLauncherNextBulletCharComponent : IComponent {

		public readonly EChar Char;
		
		public BulletLauncherNextBulletCharComponent( EChar c ) {
			Char = c;
		}

	}

}
