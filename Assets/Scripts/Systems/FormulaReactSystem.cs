using Bang;
using Bang.Components;
using Bang.Entities;
using Bang.Systems;
using Bang.Unity;
using Bang.Unity.Utilities;
using UnityEngine;


namespace GameJam {
	
	[Messager( typeof( FormulaReactionTriggeredMessage ), typeof( FormulaReactionCommandMessage ) )]
	[Filter( typeof( EggComponent ), typeof( CharComponent ) )]
	public class FormulaReactSystem : IMessagerSystem {

		public void OnMessage( World world, Entity entity, IMessage message ) {
			if ( message is FormulaReactionTriggeredMessage formulaReactionTriggeredMessage ) {
				var thisChar = entity.GetChar().Char;
				var inComingChar = formulaReactionTriggeredMessage.InComingChar;

				var formulaReactType = default( FormulaReactType );
				switch ( thisChar ) {
					case EChar.Na:
						switch ( inComingChar ) {
							case EChar.Na:
								formulaReactType = FormulaReactType.CreateSevenCopies;
								break;
							case EChar.I:
								formulaReactType = FormulaReactType.Score;
								break;
						}
						break;
					case EChar.I:
						switch ( inComingChar ) {
							case EChar.Na:
								formulaReactType = FormulaReactType.Score;
								break;
							case EChar.I:
								formulaReactType = FormulaReactType.VolumeIncrease;
								break;
						}
						break;
				}
				
				Debug.Log( $"{Game.Frame} triggered FormulaReact: {thisChar} + {inComingChar}" );
				entity.SendMessage( new FormulaReactionCommandMessage( formulaReactType ) );
			}

			if ( message is FormulaReactionCommandMessage formulaReactionCommandMessage ) {
				Debug.Log( $"{Game.Frame} apply FormulaReactionCommand: {formulaReactionCommandMessage.ReactType}" );
				switch ( formulaReactionCommandMessage.ReactType ) {
					case FormulaReactType.CreateSevenCopies:
						for ( var i = 0; i < 7; i++ ) {
							entity.Clone( world );
						}
						break;
					case FormulaReactType.VolumeIncrease:
						var currentIncrement = entity.GetEggVolumeIncrement().Increment;
						entity.SetEggVolumeIncrement( currentIncrement * 2 );
						break;
					case FormulaReactType.Score:
						if ( world.TryGetUniqueEntityGameRound() is {} gameRoundEntity ) {
							// TODO: score caculation
							var score = 1;
							gameRoundEntity.SendMessage( new GameScoreMessage( score ) );
						}
						
						// destroy after score!
						entity.Destroy();
						break;
				}
			}
		}
		
	}

}
