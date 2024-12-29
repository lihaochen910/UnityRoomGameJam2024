using System.Collections.Generic;
using GameJam.Utilities;
using MoreMountains.Tools;
using UnityEngine;


namespace GameJam.MonoBehaviours {

	public class MyCorgiController : MoreMountains.CorgiEngine.CorgiController {
		
		[Header("Friction")]
		[MMInformation("Set a friction between 0.01 and 0.99 to get a slippery surface (close to 0 is very slippery, close to 1 is less slippery).\nOr set it above 1 to get a sticky surface. The higher the value, the stickier the surface.",MoreMountains.Tools.MMInformationAttribute.InformationType.Info,false)]

		/// the amount of friction to apply to a CorgiController walking over this surface		
		[Tooltip("the amount of friction to apply to a CorgiController walking")]
		public float MyFriction;

		public List<RaycastHit2D> ContactList => _contactList;

		protected override void Initialization() {
			base.Initialization();

			_gravityActive = false;
		}

		protected override void EveryFrame() {
			if ( Time.timeScale == 0f ) {
				return;
			}

			ApplyGravity();
			FrameInitialization();

			// we initialize our rays
			SetRaysParameters();
			HandleMovingPlatforms(_movingPlatform);
			HandleMovingPlatforms(_pusherPlatform, true);

			// we store our current speed for use in moving platforms mostly
			ForcesApplied = _speed;

			// we cast rays on all sides to check for slopes and collisions
			DetermineMovementDirection();
			if (CastRaysOnBothSides)
			{
				CastRaysToTheLeft();
				CastRaysToTheRight();
			}
			else
			{                
				if (_movementDirection == -1)
				{
					CastRaysToTheLeft();
				}
				else
				{
					CastRaysToTheRight();
				}
			}
			CastRaysBelow();
			CastRaysAbove();

			MoveTransform();

			SetRaysParameters();
			ComputeNewSpeed();
			ApplyFriction();
			SetStates();
			ComputeDistanceToTheGround();

			// _externalForce.x = 0;
			// _externalForce.y = 0;

			FrameExit();

			_worldSpeed = Speed;
		}

		private void ApplyFriction() {
			_friction = MyFriction;

			// we pass the horizontal force that needs to be applied to the controller.
			// float groundAcceleration = Parameters.SpeedAccelerationOnGround;
			// float airAcceleration = Parameters.SpeedAccelerationInAir;
			// float movementFactor = State.IsGrounded ? groundAcceleration : airAcceleration;

			// if we are not in instant acceleration mode, we lerp towards our movement speed
			var force = Speed;

			// if we have a friction above 1 (mud, water, stuff like that), we divide our speed by that friction
			if ( MyFriction > 1 ) {
				force = force / MyFriction;
			}

			// if we have a low friction (ice, marbles...) we lerp the speed accordingly
			else if ( MyFriction is < 1f and > 0f ) {
				force.x = Mathf.Lerp( Speed.x, 0, Time.deltaTime * MyFriction * 10 );
				force.y = Mathf.Lerp( Speed.y, 0, Time.deltaTime * MyFriction * 10 );
			}

			if ( !force.IsNaN() ) {
				// we set our newly computed speed to the controller
				SetHorizontalForce( force.x );
				SetVerticalForce( force.y );
			}
			
		}

	}

}
