using System.Collections.Immutable;
using Bang;
using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;


namespace GameJam {

	[Watch( typeof( PlayerInputComponent ) )]
	[Filter( typeof( PlayerComponent ) )]
	public class BulletLauncherSystem : IReactiveSystem, IUpdateSystem {

		public void OnAdded( World world, ImmutableArray< Entity > entities ) {
			
		}

		public void OnRemoved( World world, ImmutableArray< Entity > entities ) {
			
		}

		public void OnModified( World world, ImmutableArray< Entity > entities ) {
			
		}

		public void Update( Context context ) {
			
		}
		
	}

}
