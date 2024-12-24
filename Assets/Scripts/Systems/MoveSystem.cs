using Bang.Contexts;
using Bang.Systems;
using Bang.Unity.Conversion;


namespace GameJam {

	[Filter( typeof( MoveComponent ), typeof( GameObjectReferenceComponent ) )]
	public class MoveSystem : IUpdateSystem {

		public void Update( Context context ) {
			
		}
		
	}

}