using Bang.Components;
using TMPro;


namespace GameJam {

	[Requires( typeof( GameRoundComponent ) )]
	public readonly struct GameRoundScoreUIComponent : IComponent {

		public readonly TextMeshPro Text;
		
		public GameRoundScoreUIComponent( TextMeshPro text ) {
			Text = text;
		}

	}

}
