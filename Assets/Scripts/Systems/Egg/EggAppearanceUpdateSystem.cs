using System.Collections.Immutable;
using Bang;
using Bang.Entities;
using Bang.Systems;
using UnityEngine;


namespace GameJam {

	[Watch( typeof( CharComponent ) )]
	[Filter( typeof( EggComponent ), typeof( EggAppearanceComponent ) )]
	public class EggAppearanceUpdateSystem : IReactiveSystem {

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
			var bulletAppearance = entity.GetEggAppearance();
			var charComponent = entity.GetChar();

			switch ( charComponent.Char ) {
				case EChar.Na:
					bulletAppearance.SpriteRenderer.color = Color.black;
					bulletAppearance.Text.text = "な";
					bulletAppearance.Text.color = Color.white;
					break;
				
				case EChar.I:
					bulletAppearance.SpriteRenderer.color = Color.white;
					bulletAppearance.Text.text = "い";
					bulletAppearance.Text.color = Color.black;
					break;
			}
		}
		
	}

}
