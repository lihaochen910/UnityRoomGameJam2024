using System.Collections.Immutable;
using Bang;
using Bang.Entities;
using Bang.Systems;


namespace GameJam {

	[Watch( typeof( EggVolumeIncrementComponent ) )]
	[Filter( typeof( EggComponent ) )]
	public class EggVolumeIncreaseSystem : IReactiveSystem {

		public void OnAdded( World world, ImmutableArray< Entity > entities ) {
			
		}

		public void OnRemoved( World world, ImmutableArray< Entity > entities ) {}

		public void OnModified( World world, ImmutableArray< Entity > entities ) {
			
		}
		
	}

}
