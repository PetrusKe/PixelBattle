using UnityEngine;
using UnityEditor;

public class EditPivotMenu : Editor 
{	
	
	[MenuItem ("GameObject/Center Pivot", false, 48)]
	public static void CenterPivot()
	{
		Undo.RegisterSceneUndo("Center Pivot: " + Selection.activeGameObject.name );
		EditPivot.CenterToMesh( Selection.activeGameObject, EditPivotSettings.UpdateCollider, EditPivotSettings.UpdatePrefab );	
		CheckError();
	}
	
	[MenuItem ("GameObject/Center Pivot", true)]
	public static bool ValidateCenterPivot()
	{
		return Validate();
	}
	
	[MenuItem ("GameObject/Edit Pivot", false, 49)]
	public static void Editpivot()
	{
		if( !Selection.activeGameObject.GetComponent<EditPivotBehaviour>() )
		{
			Undo.RegisterSceneUndo("Edit Pivot: " + Selection.activeGameObject.name );		
			Selection.activeGameObject.AddComponent<EditPivotBehaviour>();
		}
	}
	
	[MenuItem ("GameObject/Edit Pivot", true)]
	public static bool ValidateEditPivot()
	{
		return Validate();
	}
	
#region Update prefab
	[MenuItem ("GameObject/Options/Update Prefab - Switch On",false, 50)]
	public static void UpdatePrefabOn()
	{
		EditPivotSettings.Instance.updatePrefab = true;
		EditorUtility.SetDirty( EditPivotSettings.Instance );
		Debug.Log( "EditPivot: Prefab gets updated." );
	}
	
	[MenuItem ("GameObject/Options/Update Prefab - Switch On", true)]
	public static bool ValidateUpdatePrefabOn()
	{
		return (! EditPivotSettings.UpdatePrefab );
	}
	
	[MenuItem ("GameObject/Options/Update Prefab - Switch Off",false, 51)]
	public static void UpdatePrefabOff()
	{
		EditPivotSettings.Instance.updatePrefab = false;
		EditorUtility.SetDirty( EditPivotSettings.Instance );
		Debug.Log( "EditPivot: Does not Update Prefabs anymore, Meshes are copied - loss of link to mesh. Use with caution" );
	}
	
	[MenuItem ("GameObject/Options/Update Prefab - Switch Off", true)]
	public static bool ValidateUpdatePrefabOff()
	{
		return EditPivotSettings.UpdatePrefab;
	}
#endregion
	
#region Collider Update Toggle 
	[MenuItem ("GameObject/Options/UpdateCollider - Switch On",false, 53)]
	public static void ColliderUpdateOn()
	{
		EditPivotSettings.Instance.updateCollider = true;
		EditorUtility.SetDirty( EditPivotSettings.Instance );
		Debug.Log( "EditPivot: Collider update on pivot change." );
	}
	
	[MenuItem ("GameObject/Options/UpdateCollider - Switch On", true)]
	public static bool ValidateColliderUpdateOn()
	{
		return (!EditPivotSettings.UpdateCollider);
	}
	
	[MenuItem ("GameObject/Options/UpdateCollider - Switch Off",false, 54)]
	public static void ColliderUpdateOff()
	{
		EditPivotSettings.Instance.updateCollider = false;
		EditorUtility.SetDirty( EditPivotSettings.Instance );
		Debug.Log( "EditPivot: Collider does not update on pivot change." );
	}
	
	[MenuItem ("GameObject/Options/UpdateCollider - Switch Off", true)]
	public static bool ValidateColliderUpdateOff()
	{
		return EditPivotSettings.UpdateCollider;
	}
#endregion
	
#region 3ds Max Fix toggle
[MenuItem ("GameObject/Options/3ds Max Fix - Switch On",false, 55)]
public static void MaxFixOn()
{
	EditPivotSettings.Instance.maxFix = true;
	EditorUtility.SetDirty( EditPivotSettings.Instance );
	Debug.Log( "EditPivot: 3ds max model import axis fix enabled." );
}

[MenuItem ("GameObject/Options/3ds Max Fix - Switch On", true)]
public static bool ValidateMaxFixOn()
{
	return (!EditPivotSettings.MaxFix);
}

[MenuItem ("GameObject/Options/3ds Max Axis Fix - Switch Off",false, 56)]
public static void MaxFixOff()
{
	EditPivotSettings.Instance.maxFix = false;
	EditorUtility.SetDirty( EditPivotSettings.Instance );
	Debug.Log( "EditPivot: 3ds max model import axis fix disabled." );
}

[MenuItem ("GameObject/Options/3ds Max Axis Fix - Switch Off", true)]
public static bool ValidateMaxFixOff()
{
	return EditPivotSettings.MaxFix;
}
#endregion	
	
	static bool Validate()
	{	
		if ( Selection.activeGameObject )
		{
			bool hasMesh =  Selection.activeGameObject.GetComponent<MeshFilter>() != null;			
			if ( hasMesh )
			{
				return true;
			}
		}
		return false;
	}
	
	static void CheckError()
	{			
		if ( EditPivot.Errormsg != "" )
		{
			Debug.Log( EditPivot.Errormsg );
		}
	}
}
