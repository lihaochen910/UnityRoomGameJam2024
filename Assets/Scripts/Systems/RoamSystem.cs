using Bang.Contexts;
using Bang.Systems;
using Bang.Unity.Conversion;


namespace GameJam {

	[Filter( typeof( RoamComponent ), typeof( GameObjectReferenceComponent ) )]
	public class RoamSystem : IUpdateSystem {

		public void Update( Context context ) {
			
		}
		
	}

}
