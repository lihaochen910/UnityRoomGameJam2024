using System.Collections.Immutable;
using Bang.Contexts;
using Bang.Entities;
using Bang.StateMachines;
using Bang.Systems;


namespace Bang.Unity {

	[Filter(kind: ContextAccessorKind.Read, typeof(IStateMachineComponent))]
	[Watch(typeof(IStateMachineComponent))]
	public class StateMachineSystem : IUpdateSystem, IReactiveSystem, IExitSystem
	{
		public void OnAdded(World world, ImmutableArray<Entity> entities) { }

		public void OnModified(World world, ImmutableArray<Entity> entities) { }

		public void OnRemoved(World world, ImmutableArray<Entity> entities)
		{
			Clean(entities, force: false);
		}

		public void Exit(Context context)
		{
			Clean(context.Entities, force: true);
		}

		public void Update(Context context)
		{
			foreach (Entity e in context.Entities)
			{
				if (e.TryGetStateMachine() is {} routine)
				{
					// float deltaTime = e.HasUnscaledDeltaTime() ? 
					// 	Game.UnscaledDeltaTime : Game.DeltaTime;
					float deltaTime = Game.DeltaTime;

					// if (Game.Instance.IsSkippingDeltaTimeOnUpdate)
					// {
					// 	deltaTime = 100;
					// }

					routine.Tick(deltaTime);
				}
			}
		}

		private void Clean(ImmutableArray<Entity> entities, bool force)
		{
			foreach (Entity e in entities)
			{
				if (e.IsDestroyed || force)
				{
					IStateMachineComponent? routine = e.TryGetStateMachine();
					routine?.OnDestroyed();
				}
			}
		}
	}

}
