using Bang.Components;
using TMPro;
using UnityEngine;


namespace GameJam {

	[Requires( typeof( EggComponent ) )]
	public readonly struct EggAppearanceComponent : IComponent {

		public readonly SpriteRenderer SpriteRenderer;
		public readonly TextMeshPro Text;
		
		public EggAppearanceComponent( SpriteRenderer spriteRenderer, TextMeshPro text ) {
			SpriteRenderer = spriteRenderer;
			Text = text;
		}

	}

}
