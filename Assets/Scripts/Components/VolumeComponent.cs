using Bang.Components;
using UnityEngine;


namespace GameJam {

	public readonly struct VolumeComponent : IComponent {

		public readonly Collider2D Collider;
		
		public VolumeComponent( Collider2D collider ) {
			Collider = collider;
		}

	}

}
