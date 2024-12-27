using System.Collections.Immutable;
using Bang;
using Bang.Entities;
using Bang.Systems;


namespace GameJam.GameRound {

	[Watch( typeof( GameRoundScoreComponent ) )]
	[Filter( typeof( GameRoundComponent ), typeof( GameRoundScoreUIComponent ) )]
	public class GameRoundScoreUISystem : IReactiveSystem {

		public void OnAdded( World world, ImmutableArray< Entity > entities ) {
			foreach ( var entity in entities ) {
				UpdateScoreUI( entity );
			}
		}

		public void OnRemoved( World world, ImmutableArray< Entity > entities ) {}

		public void OnModified( World world, ImmutableArray< Entity > entities ) {
			foreach ( var entity in entities ) {
				UpdateScoreUI( entity );
			}
		}

		private void UpdateScoreUI( Entity entity ) {
			var scoreComponent = entity.GetGameRoundScore();
			entity.GetGameRoundScoreUI().Text.text = $"スコア: {scoreComponent.Score}";
		}
		
	}

}
