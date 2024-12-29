using Bang.Components;


namespace GameJam {
	
	[Requires( typeof( FormulaReactDisabledComponent ) )]
	public readonly struct FormulaReactDisabledCountdownComponent : IComponent {

		public readonly float Time;
		
		public FormulaReactDisabledCountdownComponent( float time ) {
			Time = time;
		}

	}

}
