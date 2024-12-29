using Bang;
using Bang.Components;
using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using Bang.Unity.Utilities;
using GameJam.Utilities;
using UnityEngine;
using Random = DigitalRune.Mathematics.Random;


namespace GameJam {
	
	[Messager( typeof( FormulaReactionTriggeredMessage ), typeof( FormulaReactionCommandMessage ) )]
	[Filter( typeof( EggComponent ), typeof( CharComponent ) )]
	[Filter( ContextAccessorFilter.NoneOf, typeof( FormulaReactDisabledComponent ) )]
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
				
				// Debug.Log( $"{Game.Frame} triggered FormulaReact: {thisChar} + {inComingChar}" );
				entity.SendMessage( new FormulaReactionCommandMessage( formulaReactType, formulaReactionTriggeredMessage.A, formulaReactionTriggeredMessage.B ) );
			}

			if ( message is FormulaReactionCommandMessage formulaReactionCommandMessage ) {
				// Debug.Log( $"{Game.Frame} apply FormulaReactionCommand: {formulaReactionCommandMessage.ReactType}" );
				switch ( formulaReactionCommandMessage.ReactType ) {
					case FormulaReactType.CreateSevenCopies:
						for ( var i = 0; i < 7; i++ ) {
							var cloned = entity.Clone( world );
							cloned.SetFormulaReactDisabled();
							cloned.SetFormulaReactDisabledCountdown( 1f );
							cloned.SetChar( Random.Chance( 50 ) ? EChar.Na : EChar.I );

							if ( cloned.TryGetCorgiController() is {} corgiController ) {
								var angle = Random.NextFloat( 360f );
								var force = Random.Range( 1f, 3f );
								var vector = Vector2.right.Rotate( angle ).normalized;
								corgiController.CorgiController.AddForce( vector * force );
							}
						}
						break;
					case FormulaReactType.VolumeIncrease:
						if ( entity.TryGetEggVolumeIncrement() is {} eggVolumeIncrement ) {
							var currentIncrement = eggVolumeIncrement.Increment;
							entity.SetEggVolumeIncrement( currentIncrement * 2 );
						}
						break;
					case FormulaReactType.Score:
						if ( world.TryGetUniqueEntityGameRound() is {} gameRoundEntity ) {
							// score caculation
							var score = 1;

							if ( formulaReactionCommandMessage.A.TryGetEggVolumeIncrement() is {} eggVolumeIncrement_1 ) {
								score *= eggVolumeIncrement_1.Increment;
							}
							
							if ( formulaReactionCommandMessage.B.TryGetEggVolumeIncrement() is {} eggVolumeIncrement_2 ) {
								score *= eggVolumeIncrement_2.Increment;
							}
							
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
