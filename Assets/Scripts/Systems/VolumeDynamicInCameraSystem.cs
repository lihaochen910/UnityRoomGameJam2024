using Bang;
using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using Bang.Unity;
using UnityEngine;


namespace GameJam {

	[Filter( typeof( VolumeComponent ) )]
	public class VolumeDynamicInCameraSystem : IUpdateSystem {

		public void Update( Context context ) {

			if ( context.World.TryGetUniqueEntityMainCamera() is not {} mainCamera ) {
				return;
			}

			var camera = mainCamera.GetGameObjectReference().GameObject.GetComponent< Camera >();
			
			foreach ( var entity in context.Entities ) {
				var volumeComponent = entity.GetVolume();
				if ( !volumeComponent.Collider ) {
					Debug.Log( $"{Game.Frame} null Collider detected. {entity.EntityId}" );
					continue;
				}
				
				// 获取圆形碰撞体的边界
				Bounds bounds = volumeComponent.Collider.bounds;
        
				// 获取相机的视口边界
				float screenAspect = (float)Screen.width / Screen.height;
				float cameraHeight = camera.orthographicSize * 2;
				float cameraWidth = cameraHeight * screenAspect;
        
				// 计算相机视口的边界
				Vector2 cameraPosition = camera.transform.position;
				float leftBound = cameraPosition.x - cameraWidth/2;
				float rightBound = cameraPosition.x + cameraWidth/2;
				float bottomBound = cameraPosition.y - cameraHeight/2;
				float topBound = cameraPosition.y + cameraHeight/2;

				// 检查碰撞体是否与视口相交
				var inCamera = bounds.max.x >= leftBound && 
					   bounds.min.x <= rightBound &&
					   bounds.max.y >= bottomBound && 
					   bounds.min.y <= topBound;
				if ( inCamera ) {
					entity.SetInCamera();
				}
				else {
					entity.RemoveInCamera();
				}
			}
			
		}
		
	}

}
