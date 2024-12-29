using System;
using DigitalRune.Mathematics;
using UnityEngine;
using Mathf = UnityEngine.Mathf;


namespace GameJam.Utilities {

	public static class Vector2Extensions {
		
		/// <summary>
		/// 旋转一个向量
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="aDegree"></param>
		/// <returns></returns>
		public static Vector2 Rotate( this Vector2 vector, float aDegree ) {
			float rad = aDegree * Mathf.Deg2Rad;
			float s   = Mathf.Sin( rad );
			float c   = Mathf.Cos( rad );
			return new Vector2(
				vector.x * c - vector.y * s,
				vector.y * c + vector.x * s
			);
		}
		
		
		public static bool IsZeroApprox( this Vector2 vector ) {
			return Numeric.IsZero( vector.x ) &&
				   Numeric.IsZero( vector.y );
		}
		
		public static bool IsZeroApprox( this Vector2 vector, float epsilon ) {
			return Numeric.IsZero( vector.x, epsilon ) &&
				   Numeric.IsZero( vector.y, epsilon );
		}
		
		private const float CMP_EPSILON = 0.00001f;
		
		public static bool IsEqualApprox( this Vector2 vector, in Vector2 otherVector ) {
			return Numeric.AreEqual( vector.x, otherVector.x, CMP_EPSILON ) &&
				   Numeric.AreEqual( vector.y, otherVector.y, CMP_EPSILON );
		}

		public static bool IsNaN( this Vector2 vector ) {
			return Numeric.IsNaN( vector.x ) || Numeric.IsNaN( vector.y );
		}
		
		public static Vector2 Absolute( this Vector2 vector ) {
			return new Vector2( Math.Abs( vector.x ), Math.Abs( vector.y ) );
		}

		public static Vector2 Sign( this Vector2 vector ) {
			return new Vector2( Math.Sign( vector.x ), Math.Sign( vector.y ) );
		}
		
		public static Vector2 Floor( this Vector2 vector ) {
			return new Vector2( UnityEngine.Mathf.Floor( vector.x ), UnityEngine.Mathf.Floor( vector.y ) );
		}
		
		public static Vector2 Ceil( this Vector2 vector ) {
			return new Vector2( UnityEngine.Mathf.Ceil( vector.x ), UnityEngine.Mathf.Ceil( vector.y ) );
		}
		
		public static Vector2 Round( this Vector2 vector ) {
			return new Vector2( UnityEngine.Mathf.Round( vector.x ), UnityEngine.Mathf.Round( vector.y ) );
		}

		public static Vector2 Clamp( this Vector2 vector, in Vector2 min, in Vector2 max ) {
			return new Vector2(
				Math.Clamp( vector.x, min.x, max.x ),
				Math.Clamp( vector.y, min.y, max.y )
			);
		}
		
	}

}
