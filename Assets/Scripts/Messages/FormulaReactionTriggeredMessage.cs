using Bang.Components;
using Bang.Entities;


namespace GameJam {

	public readonly struct FormulaReactionTriggeredMessage : IMessage {

		public readonly Entity A;
		public readonly Entity B;
		
		public FormulaReactionTriggeredMessage( Entity a, Entity b ) {
			A = a;
			B = b;
		}

	}

}
