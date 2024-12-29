using Bang.Components;


namespace GameJam.CorgiController {

	public readonly struct CorgiControllerComponent : IComponent {

		public readonly MoreMountains.CorgiEngine.CorgiController CorgiController;
		
		public CorgiControllerComponent( MoreMountains.CorgiEngine.CorgiController corgiController ) {
			CorgiController = corgiController;
		}

	}

}
