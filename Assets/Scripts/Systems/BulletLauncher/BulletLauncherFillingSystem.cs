using System.Collections.Immutable;
using Bang;
using Bang.Components;
using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using GameJam.Utilities;
using UnityEngine;
using Random = DigitalRune.Mathematics.Random;


namespace GameJam {

	[Watch( typeof( BulletLauncherComponent ) )]
	[Messager( typeof( BulletFiredMessage ) )]
	[Filter( typeof( BulletLauncherComponent ) )]
	public class BulletLauncherFillingSystem : IStartupSystem, IReactiveSystem, IMessagerSystem {
		
		private GameObject _prefabBullet;
		
		public void Start( Context context ) {
			_prefabBullet = Resources.Load< GameObject >( "Prefabs/Bullet" );
		}
		
		public void OnAdded( World world, ImmutableArray< Entity > entities ) {
			foreach ( var entity in entities ) {
				TakeNext( world, entity );
			}
		}

		public void OnRemoved( World world, ImmutableArray< Entity > entities ) {
			foreach ( var entity in entities ) {
				entity.RemoveBulletLauncherCurrentBullet();
				entity.RemoveBulletLauncherNextBulletChar();
			}
		}

		public void OnModified( World world, ImmutableArray< Entity > entities ) {}

		public void OnMessage( World world, Entity entity, IMessage message ) {
			if ( message is BulletFiredMessage ) {
				TakeNext( world, entity );
			}
		}

		private void TakeNext( World world, Entity entity ) {
			var bulletEntity = world.SpawnEntityPrefab( _prefabBullet );
			bulletEntity.GetGameObjectReference().GameObject.transform.position =
				entity.GetGameObjectReference().GameObject.transform.position;
			if ( entity.TryGetBulletLauncherNextBulletChar() is {} nextBulletChar ) {
				bulletEntity.SetChar( nextBulletChar.Char );
			}
			else {
				bulletEntity.SetChar( Random.Chance( 50 ) ? EChar.Na : EChar.I );
			}
			bulletEntity.SetDestroyDuringOutOfScreen();
			bulletEntity.SetInCamera();
			
			entity.SetBulletLauncherCurrentBullet( bulletEntity );
			entity.SetBulletLauncherNextBulletChar( Random.Chance( 50 ) ? EChar.Na : EChar.I );
		}
		
	}

}
