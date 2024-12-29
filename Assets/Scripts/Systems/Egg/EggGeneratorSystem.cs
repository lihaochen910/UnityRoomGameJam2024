using Bang;
using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using Bang.Unity;
using GameJam.Utilities;
using UnityEngine;
using Random = DigitalRune.Mathematics.Random;


namespace GameJam {
	
	[Filter( typeof( EggGeneratorComponent ) )]
	public class EggGeneratorSystem : IStartupSystem, IExitSystem, IUpdateSystem {

		private GameObject _prefabEgg;
		private float _timer;
		private const float _generateInterval = 1f;
		
		public void Start( Context context ) {
			_prefabEgg = Resources.Load< GameObject >( "Prefabs/Egg_2" );
			_timer = _generateInterval;
		}
		
		public void Exit( Context context ) {
			// Resources.UnloadAsset( _prefabEgg );
			_prefabEgg = null;
		}
		
		public void Update( Context context ) {
			
			Vector3 GetRandomWorldPosition( Camera camera ) {
				// 获取屏幕的宽度和高度
				float screenWidth = Screen.width;
				float screenHeight = Screen.height;

				// 生成随机的屏幕坐标
				float randomX = Random.Range( 0, screenWidth );
				float randomY = Random.Range( 0, screenHeight );

				// 将屏幕坐标转换为世界坐标
				Vector3 screenPosition = new Vector3( randomX, randomY, camera.nearClipPlane );
				Vector3 worldPosition = camera.ScreenToWorldPoint( screenPosition );

				// 返回生成的世界坐标
				return new Vector3( worldPosition.x, worldPosition.y, 0 ); // z 轴可以根据需要调整
			}
			
			_timer -= Game.DeltaTime;
			if ( _timer < 0 ) {
				_timer = _generateInterval;
				
				var eggs = context.World.GetEntitiesWith( ContextAccessorFilter.AnyOf, typeof( EggComponent ) );
				if ( eggs.Length < 100 ) {
					var eggEntity = context.World.SpawnEntityPrefab( _prefabEgg );
					eggEntity.SetChar( Random.Chance( 50 ) ? EChar.Na : EChar.I );
					eggEntity.SetEggVolumeIncrement( 1 );
					eggEntity.SetInCamera();
					eggEntity.SetDestroyDuringOutOfScreen();
					
					if ( context.World.TryGetUniqueEntityMainCamera() is {} cameraEntity &&
						 cameraEntity.TryGetUnityComponent< Camera >() is {} camera ) {
						eggEntity.TrySetTransformPosition( GetRandomWorldPosition( camera ) );
					}
				}
			}
		}
		
	}

}
