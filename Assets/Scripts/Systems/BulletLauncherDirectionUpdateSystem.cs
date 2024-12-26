using Bang;
using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using Bang.Unity.Conversion;
using UnityEngine;


namespace GameJam {

	[Filter( typeof( BulletLauncherComponent ), typeof( GameObjectReferenceComponent ) )]
	public class BulletLauncherDirectionUpdateSystem : IUpdateSystem {

		public void Update( Context context ) {
			if ( context.World.TryGetUniqueEntityMainCamera() is not {} mainCamera ) {
				return;
			}

			var camera = mainCamera.GetGameObjectReference().GameObject.GetComponent< Camera >();
			foreach ( var entity in context.Entities ) {
				var launcherTransform = entity.GetGameObjectReference().GameObject.transform;
				
				// 获取鼠标在屏幕上的位置
				Vector2 mouseScreenPosition = Input.mousePosition;

				// 将屏幕坐标转换为世界坐标
				// 使用 Camera.main.ScreenToWorldPoint，注意需要设置一个合适的 z 值
				// 这里假设我们希望在与 Transform 相同的 z 平面上计算
				// mouseScreenPosition.z = camera.nearClipPlane; // 或者使用 transform.position.z
				Vector2 mouseWorldPosition = camera.ScreenToWorldPoint( mouseScreenPosition );

				// 计算方向向量
				Vector2 direction = ( mouseWorldPosition - ( Vector2 )launcherTransform.position ).normalized;
				
				// // 计算角度
				// float angle = Mathf.Atan2( direction.y, direction.x ) * Mathf.Rad2Deg;
				entity.SetBulletLauncherDirection( direction );
			}
		}
		
	}

}
