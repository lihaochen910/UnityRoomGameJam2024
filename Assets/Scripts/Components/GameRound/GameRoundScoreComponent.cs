using Bang.Components;


namespace GameJam {

	[Unique]
	[Requires( typeof( GameRoundComponent ) )]
	public readonly struct GameRoundScoreComponent : IComponent {

		public readonly int Score;
		
		public GameRoundScoreComponent( int score ) {
			Score = score;
		}

	}

}
