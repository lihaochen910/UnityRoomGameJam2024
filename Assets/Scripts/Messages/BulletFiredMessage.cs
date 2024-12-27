using Bang.Components;
using Bang.Entities;


namespace GameJam {

	public readonly struct BulletFiredMessage : IMessage {

		public readonly Entity BulletEntity;
		
		public BulletFiredMessage( Entity bulletEntity ) {
			BulletEntity = bulletEntity;
		}

	}

}
