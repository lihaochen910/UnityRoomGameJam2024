using System.Collections.Generic;
using System.Collections.Immutable;
using Bang;
using Bang.Contexts;
using Bang.Entities;
using Bang.StateMachines;
using Bang.Systems;
using Bang.Unity.Services;
using GameJam.Utilities;
using UnityEngine;
using Random = DigitalRune.Mathematics.Random;


namespace GameJam {

	[Watch( typeof( EggGeneratorComponent ) )]
	[Filter( typeof( EggGeneratorComponent ) )]
	public class EggGeneratorSystem : IStartupSystem, IExitSystem, IReactiveSystem {

		private GameObject _prefabEgg;
		
		public void Start( Context context ) {
			_prefabEgg = Resources.Load< GameObject >( "Prefabs/Egg" );
		}
		
		public void Exit( Context context ) {
			// Resources.UnloadAsset( _prefabEgg );
			_prefabEgg = null;
		}

		public void OnAdded( World world, ImmutableArray< Entity > entities ) {
			foreach ( var entity in entities ) {
				entity.RunCoroutine( CRGenerateEgg( world ) );
			}
		}

		public void OnRemoved( World world, ImmutableArray< Entity > entities ) {
			foreach ( var entity in entities ) {
				entity.RemoveCoroutine();
			}
		}

		public void OnModified( World world, ImmutableArray< Entity > entities ) {}

		private IEnumerator< Wait > CRGenerateEgg( World world ) {

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

			while ( true ) {
				yield return Wait.ForSeconds( 1f );

				var eggs = world.GetEntitiesWith( ContextAccessorFilter.AnyOf, typeof( EggComponent ) );
				if ( eggs.Length < 100 ) {
					var eggEntity = world.SpawnEntityPrefab( _prefabEgg );
					eggEntity.SetChar(Random.Chance( 50 ) ? EChar.Na : EChar.I );
					eggEntity.SetEggVolumeIncrement( 1 );
					
					if ( world.TryGetUniqueEntityMainCamera() is {} cameraEntity &&
						 cameraEntity.TryGetUnityComponent< Camera >() is {} camera ) {
						eggEntity.TrySetTransformPosition( GetRandomWorldPosition( camera ) );
					}
				}
				
			}
			
		}
		
	}

}
