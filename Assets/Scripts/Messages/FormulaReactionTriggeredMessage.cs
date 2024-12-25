using Bang.Components;


namespace GameJam {

	public readonly struct FormulaReactionTriggeredMessage : IMessage {

		public readonly EChar InComingChar;
		
		public FormulaReactionTriggeredMessage( EChar inComingChar ) {
			InComingChar = inComingChar;
		}

	}

}
