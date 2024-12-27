using Bang.Components;


namespace GameJam {

	[Requires( typeof( EggComponent ) )]
	public readonly struct EggVolumeIncrementComponent : IComponent {

		public readonly int Increment;
		
		public EggVolumeIncrementComponent( int increment ) {
			Increment = increment;
		}

	}

}
