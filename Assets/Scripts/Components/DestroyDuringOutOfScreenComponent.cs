using Bang.Components;


namespace GameJam {

	[Requires( typeof( VolumeComponent ) )]
	public readonly struct DestroyDuringOutOfScreenComponent : IComponent {}

}
