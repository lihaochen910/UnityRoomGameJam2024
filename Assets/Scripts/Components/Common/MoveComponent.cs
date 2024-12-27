using Bang.Components;
using UnityEngine;


namespace GameJam {

	public readonly struct MoveComponent : IComponent {

		public readonly float MoveSpeed;
		public readonly Vector2 Dir;
		
		public MoveComponent( float moveSpeed, Vector2 dir ) {
			MoveSpeed = moveSpeed;
			Dir = dir;
		}

	}

}
