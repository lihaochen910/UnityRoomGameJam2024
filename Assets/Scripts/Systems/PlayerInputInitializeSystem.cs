using Bang;
using Bang.Contexts;
using Bang.Systems;


namespace GameJam {

	[Filter]
	public class PlayerInputInitializeSystem : IStartupSystem, IExitSystem {
		
		private DefaultPlayerInput _playerInput;
		
		public void Start( Context context ) {
			_playerInput = new DefaultPlayerInput();
			// _playerInput.Default.AddCallbacks( this );
			_playerInput.Gameplay.Enable();
			
			context.World.AddEntity( new PlayerInputComponent( _playerInput ) );
		}
		
		public void Exit( Context context ) {
			_playerInput.Dispose();

			if ( context.World.TryGetUniqueEntityPlayerInput() is {} playerInputEntity ) {
				playerInputEntity.Destroy();
			}
		}
		
	}

}
