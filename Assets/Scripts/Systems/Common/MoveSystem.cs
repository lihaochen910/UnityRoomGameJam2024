using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using Bang.Unity.Conversion;
using UnityEngine;


namespace GameJam {

	[Filter( typeof( MoveComponent ), typeof( GameObjectReferenceComponent ) )]
	public class MoveSystem : IUpdateSystem {

		public void Update( Context context ) {
			foreach ( var entity in context.Entities ) {
				var transform = entity.GetGameObjectReference().GameObject.transform;
				var moveComponent = entity.GetMove();
				transform.Translate( moveComponent.Dir * moveComponent.MoveSpeed * Time.deltaTime );
			}
		}
		
	}

}
