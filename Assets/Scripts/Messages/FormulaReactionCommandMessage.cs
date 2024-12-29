using Bang.Components;
using Bang.Entities;


namespace GameJam {
	
	public enum FormulaReactType : byte {
		CreateSevenCopies,
		VolumeIncrease,
		Score
	}
	

	public readonly struct FormulaReactionCommandMessage : IMessage {

		public readonly FormulaReactType ReactType;
		public readonly Entity A;
		public readonly Entity B;
		
		public FormulaReactionCommandMessage( FormulaReactType reactType, Entity a, Entity b ) {
			ReactType = reactType;
			A = a;
			B = b;
		}

	}

}
