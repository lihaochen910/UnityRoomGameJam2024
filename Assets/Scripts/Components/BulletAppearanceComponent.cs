using Bang.Components;
using TMPro;
using UnityEngine;


namespace GameJam {

	[Requires( typeof( BulletComponent ) )]
	public readonly struct BulletAppearanceComponent : IComponent {

		public readonly SpriteRenderer OutterSprite;
		public readonly SpriteRenderer InnerSprite;
		public readonly TextMeshPro Text;
		
		public BulletAppearanceComponent( SpriteRenderer outterSprite, SpriteRenderer innerSprite, TextMeshPro text ) {
			OutterSprite = outterSprite;
			InnerSprite = innerSprite;
			Text = text;
		}

	}

}
