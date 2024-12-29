using Bang.Components;
using Bang.Entities;


namespace GameJam {

	public readonly struct FormulaReactionTriggeredMessage : IMessage {

		public readonly EChar InComingChar;
		public readonly Entity A;
		public readonly Entity B;
		
		public FormulaReactionTriggeredMessage( EChar inComingChar, Entity a, Entity b ) {
			InComingChar = inComingChar;
			A = a;
			B = b;
		}

	}

}
