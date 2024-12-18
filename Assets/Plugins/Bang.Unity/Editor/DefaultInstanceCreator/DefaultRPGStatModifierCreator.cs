// using System;
// using Pixpil.RPGStatSystem;
//
//
// namespace Bang.Unity.Editor {
// 	
// 	public class DefaultRPGStatModifierCreator : IDefaultInstanceCreator {
//
// 		public bool CanHandlesType( Type type ) => type.IsAbstract && type == typeof( RPGStatModifier );
//
// 		public object CreateDefault( Type type ) => new RPGStatModBaseAdd( 0f );
// 	}
//
// }