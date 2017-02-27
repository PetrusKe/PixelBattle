using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class EditPivot 
{
	/// <summary>
	/// Simple Error string, of what went wrong at last EditPivot call.
	/// </summary>	
	public static string Errormsg { get; private set; }
	
	/// <summary>
	/// Moves the pivot of the GameObject to the targetposition.
	/// </summary>	
	public static void MovePivot( GameObject go, Vector3 worldPosition, Quaternion rotation, bool updateCollider=true, bool updatePrefab=true )
	{
		//avoid moving if already at worldPosition
		if( go.transform.position == worldPosition && rotation == Quaternion.identity )
		{
			return;
		}	
		
		// try to grab the shared mesh - otherwise deepcopy the mesh, results in loss of link to the imported model		
		Mesh mesh;
		if ( updatePrefab )
		{			
			mesh = GetMesh( go, false );
		}
		else
		{
			mesh = GetMesh( go, true );	
		}		
		
		PrefabType prefabType = PrefabUtility.GetPrefabType( go );
		// get primitve meshes - ie assets but not in project directly
		if( AssetDatabase.GetAssetPath( mesh ) == "" )
		{
			mesh = GetMesh( go, true );				
			switch ( prefabType )
			{
				case PrefabType.None:
					Debug.Log( "EditPivot: Attention - the mesh will be lost if the gameobject is changed to a prefab! \nMake the gameobject a prefab before using EditPivot. It works in the scene though.");
					break;
				case PrefabType.Prefab:
				case PrefabType.PrefabInstance:
					Debug.Log( "EditPivot: Mesh seems to be a primitive - creating mesh asset" );
					string path = AssetDatabase.GetAssetPath( PrefabUtility.GetPrefabParent( go ) );
					path = AssetDatabase.GenerateUniqueAssetPath( PathTail( path ) + "/" + mesh.name + ".asset" );				
					AssetDatabase.CreateAsset( mesh, path );
					
					((GameObject)PrefabUtility.GetPrefabParent( go )).GetComponent<MeshFilter>().sharedMesh = mesh;				
						
					AssetDatabase.SaveAssets();
					AssetDatabase.Refresh();
					break;
				case PrefabType.DisconnectedModelPrefabInstance:
				case PrefabType.DisconnectedPrefabInstance:	
				case PrefabType.MissingPrefabInstance:
					Debug.LogError( "EditPivot: Cleanup Prefab Hirarchy of Object: " + go.name + " - aborting action");
					return;						
			}			
		}		
		
		List<Transform> children = GetChildren( go );	
		if ( !mesh && children == null )
		{
			return;
		}			
		Errormsg = "";	
		
		// unparent children to avoid to keep em in place 		
		if ( children != null)
		{
			foreach ( var child in children )
			{				
				child.parent = null;
			}
		}		
		
		// get offset vector
		Vector3 offset = go.transform.position - worldPosition;	
		
		// only import external ModelData like ".FBX"
		if( AssetDatabase.Contains( mesh ) && !AssetDatabase.GetAssetPath( mesh ).EndsWith(".asset"))
		{				
			EditPivotModelPostProcessor.Reimport( ref mesh, offset, rotation, go.transform );
		}
		else
		{
			MoveMesh( ref mesh, offset, rotation, go.transform );
		}
		
		//set transform to targetposition			
		go.transform.rotation *= rotation;
		go.transform.position = worldPosition;
		
		// do offset on collider if desired
		if(updateCollider)
		{		
			MoveCollider( go, offset, rotation );
		}	
		
		//reparent children again
		if ( children != null)
		{
			foreach ( var child in children )
			{				
				child.parent = go.transform;
			}
		}
		
		EditorUtility.UnloadUnusedAssets();		
	}
	/// <summary>
	/// Centers the pivot to the center of the attached mesh.
	/// </summary>	
	public static void CenterToMesh( GameObject go, bool updateCollider = true, bool updatePrefab=true )
	{		
		Mesh mesh = GetMesh( go, false );
		
		if ( !mesh )
		{
			return;
		}	
	
		// get center of mesh
		Vector3 center = go.transform.TransformPoint( mesh.bounds.center );
		
		// avoid moving if were already centered -> messes up collider
		if( center != go.transform.position )
		{
			MovePivot( go, center, Quaternion.identity, updateCollider, updatePrefab );					
		}
	}
	/// <summary>
	/// Centers the pivot to the center of all child transforms.
	/// </summary>	
	public static void CenterToChildren( GameObject go, bool updateCollider = true, bool updatePrefab=true )
	{
		List<Transform> children = GetChildren ( go, false );
		
		if( children == null )
		{
			return;
		}
					
		// get center of children
		Vector3 center = Vector3.zero;
		for ( int i=0; i<children.Count; i++ )
		{
			center +=  children[i].position;
		}
		center /= children.Count;
		
		// avoid moving if were already centered -> messes up collider
		if( center != go.transform.position )
		{
			MovePivot( go, center, Quaternion.identity, updateCollider );					
		}
	}
		
	static void MoveCollider( GameObject go, Vector3 offset, Quaternion rotation )
	{
		bool sharedColliderMesh = false;
		if( go.GetComponent<MeshFilter>() != null && go.GetComponent<MeshCollider>() != null )
		{
			sharedColliderMesh = go.GetComponent<MeshFilter>().sharedMesh == go.GetComponent<MeshCollider>().sharedMesh; 
		}		
		
		Collider collider = go.GetComponent<Collider>();	
		Mesh mesh = go.GetComponent<MeshFilter>().sharedMesh;
		
		if( collider )
		{			
			if ( collider is BoxCollider )
			{
				BoxCollider boxcollider = (BoxCollider)collider;
				
				boxcollider.center = mesh.bounds.center;				
				boxcollider.size = mesh.bounds.size;
				return;
			}
			if ( collider is SphereCollider )
			{
				SphereCollider spherecollider = (SphereCollider)collider;
				
				spherecollider.center = mesh.bounds.center;				
				return;
			}
			if ( collider is CapsuleCollider )
			{
				CapsuleCollider capsulecollider = (CapsuleCollider)collider;
				
				capsulecollider.center = mesh.bounds.center;				
				return;
			}
			if ( collider is WheelCollider )
			{
				WheelCollider wheelcollider = (WheelCollider)collider;
				
				wheelcollider.center = mesh.bounds.center;				
				return;
			}
			if ( collider is TerrainCollider )
			{
				Debug.Log( "EditPivot: Currently not supporting TerrainCollider" );
				return;
			}
			if ( collider is MeshCollider )
			{
				if( sharedColliderMesh )
				{
					((MeshCollider)collider).sharedMesh = go.GetComponent<MeshFilter>().sharedMesh;
				}
				else
				{
					Mesh copymesh = DeepCopyMesh( ((MeshCollider)collider).sharedMesh );
					// delete mesh if its a copy - ie. not in AssetDatabase
					if ( !AssetDatabase.Contains( ((MeshCollider)collider).sharedMesh.GetInstanceID() ))
					{
						Object.DestroyImmediate( ((MeshCollider)collider).sharedMesh );
					}
					MoveMesh( ref copymesh, offset, rotation, go.transform );
					((MeshCollider)collider).sharedMesh = copymesh;
				}				
			}
		}
	}
	
	
	// grab all vertices and offset/rotate
	public static void MoveMesh( ref Mesh mesh, Vector3 offset, Quaternion rotation, Transform transf )
	{
		Vector3[] vertices = mesh.vertices;
		
		Vector3 localoffset = transf.InverseTransformDirection( offset );
		localoffset = new Vector3( localoffset.x/transf.localScale.x, localoffset.y/transf.localScale.y, localoffset.z/transf.localScale.z);
			
		Quaternion rot = Quaternion.Inverse(rotation);
				
		for( int i=0; i< mesh.vertexCount; i++ )
		{
			vertices[i] =  rot * (vertices[i] + localoffset);
		}
		mesh.vertices = vertices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}
	
	public static void MoveMesh( ref Mesh mesh, Matrix4x4 matrix )
	{
		Vector3[] vertices = mesh.vertices;

		for( int i=0; i< mesh.vertexCount; i++ )
		{
			vertices[i] = matrix.MultiplyPoint( vertices[i] );
		}
		mesh.vertices = vertices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}
		
		
	// deep copy a mesh
	static Mesh DeepCopyMesh( Mesh source )
	{	
		if ( !source )
		{
			Debug.LogError( "EditPivot: source is null!");
			return null;
		}
		
		Mesh mesh = (Mesh)Mesh.Instantiate( source );
		
		if ( source.name.EndsWith( ".copy" ))
		{
			mesh.name =  source.name;
		}
		else
		{	
			mesh.name =  source.name + ".copy";
		}
		
		return mesh;
	}
		
	static List<Transform> GetChildren( GameObject go, bool onlyFirstLevel = true )
	{
		if ( go.transform.GetChildCount() == 0 )
		{
			Errormsg = "EditPivot: '" + go.name + "' has no children!";			
			return null;
		}
		List<Transform> children = new List<Transform>();
		if ( onlyFirstLevel )
		{
			for ( int i=0; i< go.transform.GetChildCount(); i++ )
			{
				children.Add( go.transform.GetChild(i) );
			}
		}
		else
		{
			children = go.transform.GetComponentsInChildren<Transform>().ToList();
			children.Remove( go.transform );
		}
		return children;
	}
	
	static Mesh GetMesh( GameObject go, bool copy = true )
	{
		Errormsg = "";
		
		if ( go )
		{			
			MeshFilter meshfilter = go.GetComponent<MeshFilter>();
			if ( meshfilter )
			{				
				if ( meshfilter.sharedMesh )
				{
					if ( copy )
					{
						Mesh mesh = DeepCopyMesh( meshfilter.sharedMesh );
						// delete mesh if its a copy
						if ( !AssetDatabase.Contains( meshfilter.sharedMesh.GetInstanceID() ))
						{
							Object.DestroyImmediate( meshfilter.sharedMesh );
						}
						meshfilter.mesh = mesh;
						return mesh;
					}
					else
					{
						return meshfilter.sharedMesh;
					}
				}
				else
				{
					Errormsg = "EditPivot: '" + go.name + "' has no mesh assigned!";
					Debug.Log( Errormsg );
					return null;
				}
			}
			else
			{
				Errormsg = "EditPivot: '" + go.name + "' does not have a mesh!";
				Debug.Log( Errormsg );
				return null;
			}
		}
		else
		{
			Errormsg = "EditPivot: Gameobject is null!";
			Debug.Log( Errormsg );
			return null;
		}
	}
				
	static string PathTail(string path )
	{	
	    // Get the directory separation character (i.e. '\').
	    string separator = "/";
	
	    // Trim any separators at the end of the path
	    string lastCharacter = path.Substring(path.Length - 1);
	    if (separator == lastCharacter)
	    {
	        path = path.Substring(0, path.Length - 1);
	    }
	
	    int lastSeparatorIndex = path.LastIndexOf(separator);
	
	    return path.Substring(0, lastSeparatorIndex);

	}
}
