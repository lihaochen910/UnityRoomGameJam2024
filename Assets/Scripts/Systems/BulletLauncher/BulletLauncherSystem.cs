using Bang;
using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;


namespace GameJam {

	[Watch( typeof( PlayerInputComponent ) )]
	[Filter( typeof( BulletLauncherComponent ), typeof( BulletLauncherDirectionComponent ) )]
	public class BulletLauncherSystem : IStartupSystem, IUpdateSystem {

		private DefaultPlayerInput _playerInput;
		
		public void Start( Context context ) {
			if ( context.World.TryGetUniquePlayerInput() is {} playerInputComponent ) {
				_playerInput = playerInputComponent.PlayerInput;
			}
		}
		
		public void Update( Context context ) {
			if ( _playerInput is null ) {
				return;
			}
			
			if ( _playerInput.Gameplay.Launch.WasPressedThisFrame() ) {
				foreach ( var entity in context.Entities ) {
					var dir = entity.GetBulletLauncherDirection().Dir;
					var bulletEntity = entity.GetBulletLauncherCurrentBullet().Entity;
					bulletEntity.GetVolume().Collider.gameObject.SetActive( true );
					bulletEntity.SetMove( 5f, dir );
					entity.RemoveBulletLauncherCurrentBullet();
					entity.SendMessage( new BulletFiredMessage( bulletEntity ) );
				}
			}
		}

		
	}

}
