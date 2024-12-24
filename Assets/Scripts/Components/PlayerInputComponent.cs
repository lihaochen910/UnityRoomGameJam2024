using Bang.Components;


namespace GameJam {

	[Unique]
	public readonly struct PlayerInputComponent : IComponent {

		public readonly DefaultPlayerInput PlayerInput;
		
		public PlayerInputComponent( DefaultPlayerInput playerInput ) {
			PlayerInput = playerInput;
		}

	}

}
