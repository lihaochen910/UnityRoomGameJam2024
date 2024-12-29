using Bang.Components;
using TMPro;
using UnityEngine.UI;


namespace GameJam {

	[Requires( typeof( GameRoundComponent ) )]
	public readonly struct GameRoundUIComponent : IComponent {

		public readonly TextMeshProUGUI TextBig;
		public readonly TextMeshProUGUI TextScore;
		public readonly TextMeshProUGUI TextTimer;
		public readonly Image Mask;
		
		public GameRoundUIComponent( TextMeshProUGUI textBig, TextMeshProUGUI textScore, TextMeshProUGUI textTimer, Image mask ) {
			TextBig = textBig;
			TextScore = textScore;
			TextTimer = textTimer;
			Mask = mask;
		}

	}

}
