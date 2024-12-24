using Bang;
using Bang.Components;
using Bang.Entities;
using Bang.Systems;


namespace GameJam {

	[Messager( typeof( FormulaReactionTriggeredMessage ) )]
	public class FormulaReactSystem : IMessagerSystem {

		public void OnMessage( World world, Entity entity, IMessage message ) {
			if ( message is FormulaReactionTriggeredMessage formulaReactionTriggeredMessage ) {
				
			}
		}
		
	}

}
