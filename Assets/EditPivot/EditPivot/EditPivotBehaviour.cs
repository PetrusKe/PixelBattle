using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// unity doesnt get the access through scriptable objects
#pragma warning disable 414

// simple data holder class for use as a component
[ExecuteInEditMode][AddComponentMenu ("Mesh/Edit Pivot")]
public class EditPivotBehaviour : MonoBehaviour 
{
#if UNITY_EDITOR
	[SerializeField] Vector3 pivotposition;
	public Vector3 Pivotposition 
	{ 
		get
		{
			return pivotposition;	
		}
		set
		{
			pivotposition = value;	
		}
	}
	
	[SerializeField] Vector3 pivotrotation;
	public Vector3 Pivotrotation 
	{ 
		get
		{
			return pivotrotation;	
		}
		set
		{
			pivotrotation = value;	
		}
	}
	
	[SerializeField] Tool lasttool = Tool.None;
	[SerializeField] bool vertexsnap;
	[SerializeField] Vector3 lastVertexSnapPosition;
	[SerializeField] GameObject refGO;
	[SerializeField] Vector3 originalposition;
	[SerializeField] Vector3 originalrotation;
	
	// unity 3.5 crashes when directly destroying a component at startup, so I chache and destroy at update 
	bool selfdestroy = false;
	
	void Reset()
	{
		Pivotposition = transform.position;
		Pivotrotation = Vector3.zero;
	}
	
	void Awake()
	{	
		if( gameObject.GetComponents<EditPivotBehaviour>().Length > 1 )
		{			
			selfdestroy = true;
		}
		else
		{
			
			Pivotposition = transform.position;
			Pivotrotation = Vector3.zero;
			
			originalposition = transform.position;
			originalrotation = Vector3.zero;
		}
	}
	
	void Update()
	{
		if (selfdestroy)
		{
			DestroyImmediate( this );	
		}
	}
#endif
}
