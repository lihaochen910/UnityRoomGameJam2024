using System.Collections.Immutable;
using Bang;
using Bang.Entities;
using Bang.Systems;
using UnityEngine;


namespace GameJam {

	[Watch( typeof( CharComponent ) )]
	[Filter( typeof( BulletAppearanceComponent ) )]
	public class BulletAppearanceUpdateSystem : IReactiveSystem {

		public void OnAdded( World world, ImmutableArray< Entity > entities ) {
			foreach ( var entity in entities ) {
				UpdateAppearance( entity );
			}
		}

		public void OnRemoved( World world, ImmutableArray< Entity > entities ) {}

		public void OnModified( World world, ImmutableArray< Entity > entities ) {
			foreach ( var entity in entities ) {
				UpdateAppearance( entity );
			}
		}

		private void UpdateAppearance( Entity entity ) {
			var bulletAppearance = entity.GetBulletAppearance();
			var charComponent = entity.GetChar();

			switch ( charComponent.Char ) {
				case EChar.Na:
					bulletAppearance.InnerSprite.color = Color.black;
					bulletAppearance.OutterSprite.color = Color.white;
					bulletAppearance.Text.text = "な";
					bulletAppearance.Text.color = Color.white;
					break;
				
				case EChar.I:
					bulletAppearance.InnerSprite.color = Color.white;
					bulletAppearance.OutterSprite.color = Color.black;
					bulletAppearance.Text.text = "い";
					bulletAppearance.Text.color = Color.black;
					break;
			}
		}
		
	}

}
