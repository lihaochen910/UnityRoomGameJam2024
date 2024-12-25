using Bang.Components;


namespace GameJam {

	[Unique]
	[Requires( typeof( GameRoundComponent ) )]
	public readonly struct GameRoundCountdownComponent : IComponent {

		public readonly float Time;
		
		public GameRoundCountdownComponent( float time ) {
			Time = time;
		}

	}

}
