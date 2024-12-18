using Bang.Components;


namespace GameJam {

	public readonly struct HelloWorldComponent : IComponent {

		public readonly int Value;
		
		public HelloWorldComponent( int value ) {
			Value = value;
		}

	}

}
