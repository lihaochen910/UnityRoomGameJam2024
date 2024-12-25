using Bang.Components;


namespace GameJam {

	public readonly struct GameScoreMessage : IMessage {

		public readonly int Score;
		
		public GameScoreMessage( int score ) {
			Score = score;
		}

	}

}
