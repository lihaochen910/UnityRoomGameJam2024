using Bang.Components;


namespace GameJam {
	
	public enum FormulaReactType : byte {
		CreateSevenCopies,
		VolumeIncrease,
		Score
	}
	

	public readonly struct FormulaReactionCommandMessage : IMessage {

		public readonly FormulaReactType ReactType;
		
		public FormulaReactionCommandMessage( FormulaReactType reactType ) {
			ReactType = reactType;
		}

	}

}
