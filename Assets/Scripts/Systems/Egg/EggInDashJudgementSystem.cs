using Bang.Contexts;
using Bang.Entities;
using Bang.Systems;
using Bang.Unity.Conversion;
using Bang.Unity.Graphics;
using GameJam.CorgiController;
using GameJam.Utilities;


namespace GameJam {
	
	[Filter( typeof( EggComponent ), typeof( CorgiControllerComponent ), typeof( GameObjectReferenceComponent ) )]
	public class EggInDashJudgementSystem : IUpdateSystem, IDrawSystem {

		public void Update( Context context ) {
			foreach ( var entity in context.Entities ) {
				var corgiController = entity.GetCorgiController().CorgiController;

				if ( corgiController.ExternalForce.IsZeroApprox( epsilon: 0.10f ) ) {
					entity.RemoveEggInDash();
				}
				else {
					entity.SetEggInDash();
				}
			}
		}

		public void Draw( Context context ) {
#if DEBUG
			var draw = Drawing.Draw.ingame;

			foreach ( var entity in context.Entities ) {
				if ( !entity.GetGameObjectReference().GameObject ) {
					continue;
				}
				
				var transform = entity.GetGameObjectReference().GameObject.transform;
				var corgiController = entity.GetCorgiController().CorgiController;
				using var _ = draw.InLocalSpace( transform );
				// draw.xy.Label2D( new Vector2( 0f, 0f ), $"{corgiController.ExternalForce}", 24f, Color.white );
				// draw.xy.Label2D( new Vector2( 0f, -0.24f ), $"force: {corgiController.ForcesApplied}", 24f, Color.white );
			}
#endif
		}
		
	}

}
