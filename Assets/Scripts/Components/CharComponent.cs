using Bang.Components;


namespace GameJam {

	public enum EChar : byte {
		Na, // な
		I　// い
	}

	public readonly struct CharComponent : IComponent {

		public readonly EChar Char;
		
		public CharComponent( EChar c ) {
			Char = c;
		}

	}

}
