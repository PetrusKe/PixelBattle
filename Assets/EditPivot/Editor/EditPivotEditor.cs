using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor( typeof(EditPivotBehaviour) )]
public class EditPivotEditor : Editor  
{
	SerializedProperty pivotposition;
	SerializedProperty pivotrotation;
	SerializedProperty lasttool;
	SerializedProperty reference;	
	SerializedProperty originalPosition;
	SerializedProperty originalRotation;
	SerializedProperty vertexsnap;
	
	Tool backup = Tool.Move;
	Vector3 snappedPos;
	Vector3[] vertices;
	
	void OnEnable()
	{
		pivotposition = serializedObject.FindProperty( "pivotposition" );
		pivotrotation = serializedObject.FindProperty( "pivotrotation" );
		originalPosition = serializedObject.FindProperty( "originalposition" );
		originalRotation = serializedObject.FindProperty( "originalrotation" );
		lasttool = serializedObject.FindProperty( "lasttool" );
		vertexsnap = serializedObject.FindProperty( "vertexsnap" );
		reference = serializedObject.FindProperty( "refGO" );		
		
		Transform transform = ((EditPivotBehaviour)target).gameObject.GetComponent<Transform>();
		snappedPos = ((EditPivotBehaviour)target).Pivotposition;
		
		//cache vertices in worldspace
		Mesh mesh = ((EditPivotBehaviour)target).gameObject.GetComponent<MeshFilter>().sharedMesh;
		vertices = new Vector3[mesh.vertexCount];
		for ( int i=0; i<mesh.vertexCount; i++)
		{
			vertices[i] =  transform.TransformPoint( mesh.vertices[i] );
		}	
	}
	
	public override void OnInspectorGUI ()
	{
		serializedObject.Update();		
		
		pivotposition.vector3Value = EditorGUILayout.Vector3Field( "Global Pivot Position", pivotposition.vector3Value );		
		pivotrotation.vector3Value = EditorGUILayout.Vector3Field( "Local Pivot Rotation", pivotrotation.vector3Value );
		
		if ( GUILayout.Button( "Apply Pivot" ))
		{
			ApplyPivot();
			return;
		}
		
		GUILayout.Space( 20 );
		// options
		GUILayout.BeginHorizontal();
		GUI.enabled = true;
		if ( ((EditPivotBehaviour)target).GetComponent<MeshFilter>() == null )
		{
			GUI.enabled = false;
		}
		if ( GUILayout.Button( "Center to Mesh" , GUILayout.MaxWidth( Screen.width * 0.5f) ))
		{
			EditPivot.CenterToMesh( ((EditPivotBehaviour)target).gameObject, EditPivotSettings.UpdateCollider, EditPivotSettings.UpdatePrefab );
			pivotposition.vector3Value = ((EditPivotBehaviour)target).transform.position;
		}
		GUI.enabled = true;
		if ( ((EditPivotBehaviour)target).transform.GetChildCount() == 0 )
		{
			GUI.enabled = false;
		}
		if ( GUILayout.Button( "Center to Children" ))
		{
			EditPivot.CenterToChildren( ((EditPivotBehaviour)target).gameObject,  EditPivotSettings.UpdateCollider, EditPivotSettings.UpdatePrefab );
			pivotposition.vector3Value = ((EditPivotBehaviour)target).transform.position;
		}
		GUI.enabled = true;
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		if ( GUILayout.Button( "Reset Pivot", GUILayout.MaxWidth( Screen.width * 0.5f) ))
		{
			pivotposition.vector3Value = originalPosition.vector3Value;
			pivotrotation.vector3Value = originalRotation.vector3Value;
			EditPivot.MovePivot( ((EditPivotBehaviour)target).gameObject, pivotposition.vector3Value, Quaternion.Euler( pivotrotation.vector3Value ), EditPivotSettings.UpdateCollider, EditPivotSettings.UpdatePrefab );
		}
		
		if ( GUILayout.Button( "Cancel" ))
		{				
			pivotposition.vector3Value = originalPosition.vector3Value;
			ApplyPivot();
			return;
		}
		GUILayout.EndHorizontal();
		
		GUILayout.Space( 20 );
		
		// move pivot to object reference
		GUILayout.BeginHorizontal();
		reference.objectReferenceValue = EditorGUILayout.ObjectField( "Pick Reference Object", reference.objectReferenceValue , typeof(GameObject), true ) as GameObject;		
 		if ( reference.objectReferenceValue == null )
		{
			 GUI.enabled = false;
		}
		if ( GUILayout.Button( "x", EditorStyles.miniButton, GUILayout.MaxWidth(24f) ) )
		{
			reference.objectReferenceValue = null;
		}
		GUILayout.EndHorizontal();
		if ( GUILayout.Button( "Move Pivot to Reference" ) )
		{
			pivotposition.vector3Value = ((GameObject)reference.objectReferenceValue).transform.position;
			
			EditPivot.MovePivot( ((EditPivotBehaviour)target).gameObject, pivotposition.vector3Value , Quaternion.Euler( pivotrotation.vector3Value ), EditPivotSettings.UpdateCollider, EditPivotSettings.UpdatePrefab );
		}
		
		serializedObject.ApplyModifiedProperties();
	}
	
	void ApplyPivot()
	{
		serializedObject.ApplyModifiedProperties();
		
		Quaternion q = Quaternion.Euler( ((EditPivotBehaviour)target).Pivotrotation );
		EditPivot.MovePivot( ((EditPivotBehaviour)target).gameObject, pivotposition.vector3Value, q,  EditPivotSettings.UpdateCollider, EditPivotSettings.UpdatePrefab );
		
		Tools.current = (Tool)lasttool.intValue;
		DestroyImmediate( target, true );
	}
	
	void OnSceneGUI ()
	{
		// hide normal move/scale/rotate handle
		if ( Tools.current != Tool.None )
		{
			lasttool.intValue = (int)Tools.current;
			backup = Tools.current;
			serializedObject.ApplyModifiedProperties();			
		}
		
		Tools.current = Tool.None;
		Tools.pivotMode = PivotMode.Pivot;
				
		Transform t = ((EditPivotBehaviour)target).transform;		
		Vector3 pos = ((EditPivotBehaviour)target).Pivotposition;		
		
		// vertex snapping
		Event current = Event.current;
		switch(current.type)
		{			
			case EventType.mouseDrag:					
				if( !current.alt && current.button == 0 && vertexsnap.boolValue)
				{
					float minDistance = Mathf.Infinity;
					int vertexToSelect = 0;
					for( int i=0; i<vertices.Length; i++ )
					{
						float distance = Vector2.Distance( HandleUtility.WorldToGUIPoint( vertices[i] ), current.mousePosition );
						minDistance = Mathf.Min( minDistance, distance );
						if( distance == minDistance )
						{
							vertexToSelect = i;	
						}
					}					
					snappedPos = vertices[ vertexToSelect ];
					
				}
				break;
		}
		
		if( vertexsnap.boolValue )
		{			
			pos = snappedPos;			
		}	
		
		pos = Handles.PositionHandle( pos, t.rotation );
		((EditPivotBehaviour)target).Pivotposition = pos;
		serializedObject.ApplyModifiedProperties();
		
		Handles.color = Color.cyan;
		Handles.SphereCap( 0, pos, Quaternion.identity, HandleUtility.GetHandleSize(pos)*0.2f );
			
		GUIStyle pivotstyle = new GUIStyle();
		pivotstyle.alignment = TextAnchor.LowerLeft;
		pivotstyle.normal.textColor = Color.cyan;

		Handles.Label( pos, "\nPivot",  pivotstyle);

		// show reference point in view
		if ( reference.objectReferenceValue != null )
		{
			GUIStyle refstyle = new GUIStyle( pivotstyle );
			refstyle.normal.textColor = Color.magenta;
			
			Handles.color = Color.magenta;
			Vector3 refpos = ((GameObject)reference.objectReferenceValue).transform.position;	
			
			Handles.SphereCap( 0, refpos, Quaternion.identity, HandleUtility.GetHandleSize(pos)*0.1f );
			Handles.Label( refpos, "\nReference",  refstyle);
		}		
		
		//in scene view gui (really nice)
		Handles.BeginGUI();			
		
		Rect guirect = new Rect(Screen.width - 160, Screen.height - 160, 150,120);
		GUILayout.BeginArea( guirect );
				
		GUI.enabled = true;
		if ( ((EditPivotBehaviour)target).GetComponent<MeshFilter>() == null )
		{
			GUI.enabled = false;
		}
		
		bool lastVertexsnap = vertexsnap.boolValue;
		vertexsnap.boolValue = GUILayout.Toggle( vertexsnap.boolValue, "Vertex Snap Mode" , GUILayout.MaxWidth( Screen.width * 0.5f));
		if( lastVertexsnap != vertexsnap.boolValue )
		{			
			snappedPos = pivotposition.vector3Value;
			serializedObject.ApplyModifiedProperties();
		}		
	
		if ( GUILayout.Button( "Center to Mesh" , GUILayout.MaxWidth( Screen.width * 0.5f) ))
		{			
			EditPivot.CenterToMesh( ((EditPivotBehaviour)target).gameObject,  EditPivotSettings.UpdateCollider, EditPivotSettings.UpdatePrefab );
			pivotposition.vector3Value = ((EditPivotBehaviour)target).transform.position;
			snappedPos = pivotposition.vector3Value;
			serializedObject.ApplyModifiedProperties();
		}		
		
		if ( ((EditPivotBehaviour)target).transform.GetChildCount() > 0 )
		{		
			if ( GUILayout.Button( "Center to Children" ))
			{			
				EditPivot.CenterToChildren( ((EditPivotBehaviour)target).gameObject, EditPivotSettings.UpdateCollider, EditPivotSettings.UpdatePrefab );
				pivotposition.vector3Value = ((EditPivotBehaviour)target).transform.position;
				snappedPos = pivotposition.vector3Value;
				serializedObject.ApplyModifiedProperties();
			}
		}
		
		GUI.enabled = true;
		GUILayout.Space( 20 );
		if(GUILayout.Button("Apply Pivot", EditorStyles.miniButton))
		{
			ApplyPivot();
		}
		if(GUILayout.Button("Cancel", EditorStyles.miniButton))
		{
			pivotposition.vector3Value = originalPosition.vector3Value;
			ApplyPivot();
		}
		GUILayout.EndArea();
		Handles.EndGUI();
		
		if (GUI.changed && target != null)
		{
        	EditorUtility.SetDirty (target);
		}	
	}
	
	// restore last tool
	void OnDestroy()
	{
		if( backup != Tool.None )
		{
			Tools.current = backup;
		}
		else
		{
			Tools.current = Tool.Move;
		}

	}


}