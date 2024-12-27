using Bang;
using Bang.Components;
using Bang.Entities;
using Bang.Systems;


namespace GameJam.GameRound {

	[Messager( typeof( GameScoreMessage ) )]
	[Filter( typeof( GameRoundComponent ) )]
	public class GameRoundScoreUpdateSystem : IMessagerSystem {

		public void OnMessage( World world, Entity entity, IMessage message ) {
			if ( message is GameScoreMessage gameScoreMessage ) {
				if ( !entity.HasGameRoundScore() ) {
					entity.SetGameRoundScore( 0 );
				}

				var scoreComponent = entity.GetGameRoundScore();
				entity.SetGameRoundScore( scoreComponent.Score + gameScoreMessage.Score );
			}
		}
		
	}

}
