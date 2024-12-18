using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Systems;
using Bang.Unity.Utilities;
#if GAME_CREATOR
using GameCreator.Editor.Common;
#endif
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


namespace Bang.Unity.Editor {

	[CustomEditor(typeof(FeatureAsset))]
	public class FeatureAssetEditor : UnityEditor.Editor {

		private FeatureAsset _featureAsset;
		private ReorderableList _systemsReorderableList;
		private SerializedProperty _systemsProp;
		private SerializedProperty _featuresProp;
		private SerializedProperty _isDiagnosticsProp;

		private Lazy< Dictionary< string, Type > > _candidateSystems = new(() => CollectionHelper.ToStringDictionary(
			ReflectionHelper.SafeGetAllTypesInAllAssemblies()
							.Where( type => type.GetInterfaces().Contains( typeof( Bang.Systems.ISystem ) ) && !type.IsInterface ),
			s => s.Name,
			s => s )
		);
		// private string[] _systemNames;
		// private Type[] _systemTypes;
		private int _selectSystemIndex;

		private void OnEnable () {
			_featureAsset = ( FeatureAsset )target;

			// _systemNames = _candidateSystems.Value.Keys.ToArray();
			// _systemTypes = _candidateSystems.Value.Values.ToArray();
			_selectSystemIndex = -1;
			
			_systemsProp = serializedObject.FindProperty( "Systems" );
			_systemsReorderableList = new ReorderableList( serializedObject, _systemsProp );
			_systemsReorderableList.displayAdd = false;
			_systemsReorderableList.displayRemove = true;
			_systemsReorderableList.elementHeight = 20;
			_systemsReorderableList.drawHeaderCallback = rect =>
				EditorGUI.LabelField( rect, "ISystem" );
			_systemsReorderableList.drawElementCallback =
				(rect, index, isActive, isFocused) => {
					var element = _systemsProp.GetArrayElementAtIndex( index );
					var value = ( TypeBooleanTuple )element.boxedValue;
					var isActived = value.Item2;
					isActived = EditorGUI.Toggle( rect, value.Item2 );
					rect.x += 20;
					
					var contentColor = GUI.contentColor;
					if ( value.Item1.SystemType is null ) {
						GUI.contentColor = Color.red;
					}

					var systemTypeLabelContent = new GUIContent( $"{value.Item1?.Name}" );
					systemTypeLabelContent.tooltip = value.Item1.SystemType != null ? value.Item1.SystemType.FullName : $"(missing) {value.Item1?.Name}";
					EditorGUI.LabelField( rect, systemTypeLabelContent );
					GUI.contentColor = contentColor;
					
					element.boxedValue = new TypeBooleanTuple( value.Item1, isActived );
				};
			
			_featuresProp = serializedObject.FindProperty( "Features" );
			_isDiagnosticsProp = serializedObject.FindProperty( "IsDiagnostics" );
		}

		public override void OnInspectorGUI() {
			
			serializedObject.Update();
			
			GUILayout.BeginVertical();

			_isDiagnosticsProp.boolValue = EditorGUILayout.Toggle( "Is Diagnostics", _isDiagnosticsProp.boolValue );
			EditorGUILayout.Separator();
			
			EditorGUILayout.LabelField( "Systems", EditorStyles.boldLabel );
			
			// if ( !_featureAsset.HasSystems ) {
			// 	GUILayout.BeginVertical ( "box" );
			// 	EditorGUILayout.LabelField ( "The System List is Empty." );
			// 	GUILayout.EndVertical ();
			// }
			
			_systemsReorderableList.DoLayoutList();
			
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "SystemToAdd" );
			var popupRect = EditorGUILayout.GetControlRect(true);
			using ( var check = new EditorGUI.ChangeCheckScope() ) {
				
				var systemTypes = _candidateSystems.Value.Values
													.Where( t => !_featureAsset.ContainsSystem( t ) )
													.ToArray();
				var systemNames = systemTypes
									  // .Select( t => new GUIContent( t.Name ) { tooltip = t.FullName } )
									  .Select( t => t.FullName.Replace( '.', '/' ) )
									  .ToArray();
				
				_selectSystemIndex = EditorGUI.Popup( popupRect, _selectSystemIndex, systemNames, EditorStyles.popup );
				if ( check.changed ) {
					var newIndex = _systemsProp.arraySize is 0 ? 0 : _systemsProp.arraySize;
					_systemsProp.InsertArrayElementAtIndex( newIndex );
					var newObj = _systemsProp.GetArrayElementAtIndex( newIndex );
					// newObj.boxedValue = new TypeBooleanTuple( _systemTypes[ _selectSystemIndex ], true );
					newObj.boxedValue = new TypeBooleanTuple( systemTypes[ _selectSystemIndex ], true );
					_selectSystemIndex = -1;
				}
				
			}
			
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Separator();
			EditorGUILayout.Space();
			
#if GAME_CREATOR
			if ( GUILayout.Button( "Add System(new)" ) ) {
				TypeSelectorFancyPopup.Open( "Add Component", typeof( ISystem ), componentType => {
					var typeToAdd = componentType;
					if ( typeToAdd != null ) {
						var newIndex = _systemsProp.arraySize is 0 ? 0 : _systemsProp.arraySize;
						_systemsProp.InsertArrayElementAtIndex( newIndex );
						var newObj = _systemsProp.GetArrayElementAtIndex( newIndex );
						newObj.boxedValue = new TypeBooleanTuple( typeToAdd, true );
						_selectSystemIndex = -1;
						
						EditorUtility.SetDirty( _featureAsset );
						serializedObject.ApplyModifiedProperties();
					}
				} );
			}
#endif
			
			EditorGUILayout.PropertyField( _featuresProp );

			GUILayout.EndVertical();
			
			EditorUtility.SetDirty( _featureAsset );
			serializedObject.ApplyModifiedProperties();
			
		}

	}

}
