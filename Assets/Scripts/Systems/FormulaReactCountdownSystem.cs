using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using Bang.Unity;


namespace GameJam {

	[Filter( typeof( FormulaReactDisabledCountdownComponent ), typeof( FormulaReactDisabledComponent ) )]
	public class FormulaReactCountdownSystem : IUpdateSystem {

		public void Update( Context context ) {
			foreach ( var entity in context.Entities ) {
				var time = entity.GetFormulaReactDisabledCountdown().Time;
				time -= Game.DeltaTime;

				if ( time < 0f ) {
					entity.RemoveFormulaReactDisabledCountdown();
					entity.RemoveFormulaReactDisabled();
				}
				else {
					entity.SetFormulaReactDisabledCountdown( time );
				}
			}
		}
		
	}

}
