using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditPivotModelPostProcessor : AssetPostprocessor 
{
	static string modelpath;
	
	static GameObject modelImporterGo;
	static string modelImporterGoName;

	static Dictionary<string, Matrix4x4> matrixMapping = new Dictionary<string, Matrix4x4>();
	static Dictionary<string, Transform> transformMapping = new Dictionary<string, Transform>();
	
	static Vector3 offset;
	static Quaternion rotation;
	static Transform oldTransform;
	
	/// <summary>
	/// Reimport the the Asset, and caching current localcenter.
	/// </param>
	public static void Reimport(ref Mesh currentMesh, Vector3 Offset, Quaternion Rotation, Transform Transf )
	{
		offset = Offset;
		rotation = Rotation;
		oldTransform = Transf;
		
		modelImporterGoName ="";
		matrixMapping.Clear();
		transformMapping.Clear();
		
		modelpath = AssetDatabase.GetAssetPath( currentMesh );
		Object[] assets = AssetDatabase.LoadAllAssetsAtPath( modelpath );		
		foreach( var asset in assets )
		{
			if( asset is GameObject )
			{
				GameObject go = (GameObject)asset;
				MeshFilter mf = go.GetComponent<MeshFilter>();
				
				if( mf == null ) continue;
				
				if( mf.sharedMesh == currentMesh )
				{
					modelImporterGoName = go.name;
				}
				
				Mesh mesh = mf.sharedMesh;		
				matrixMapping.Add( go.name, GetMatrix( ref mesh ) );
				transformMapping.Add( go.name, go.transform );
			}
		}
		
		if( modelImporterGoName == "" )
		{
			throw new System.Exception( "EditPivot: modelprefab not found!" );
		}
		
		AssetDatabase.ImportAsset( modelpath );
	}

	void OnPostprocessModel( GameObject go )
	{		
		if( EditPivotSettings.MaxFix )
		{
			foreach( var mf in go.GetComponentsInChildren<MeshFilter>() )
			{				
				Mesh mesh = mf.sharedMesh;	
				Quaternion maxFixRotation = Quaternion.AngleAxis(-90f, Vector3.right );  
				EditPivot.MoveMesh( ref mesh, Matrix4x4.TRS( Vector3.zero, maxFixRotation , Vector3.one ) );				
				mf.transform.rotation = mf.transform.rotation * Quaternion.Inverse( maxFixRotation );					
			}
		}		
		
		if ( assetPath == modelpath )
		{
			Mesh mesh = new Mesh();
			
			//pull out correct GameObject & reconstruct all edit pivot movements
			foreach( var mf in go.GetComponentsInChildren<MeshFilter>() )
			{			
				mesh = mf.sharedMesh;
				Matrix4x4 newMatrix = GetMatrix( ref mesh );
				Matrix4x4 offsetMatrix = matrixMapping[mf.gameObject.name] * newMatrix.inverse;	
				EditPivot.MoveMesh( ref mesh, offsetMatrix );				
				
				if( mf.gameObject.name == modelImporterGoName )
				{				
					modelImporterGo = mf.gameObject;
				}
				else
				{
					mf.transform.position = transformMapping[mf.gameObject.name].position;
					mf.transform.rotation = transformMapping[mf.gameObject.name].rotation;
				}
			}
			
			if( modelImporterGo == null )
			{
				throw new System.Exception( "error finding correct modelimporter gameobject!" );
			}
			
			mesh = modelImporterGo.GetComponent<MeshFilter>().sharedMesh;			
			
			Vector3 localOffset = oldTransform.InverseTransformDirection( offset );	
			localOffset = new Vector3( localOffset.x/oldTransform.localScale.x, localOffset.y/oldTransform.localScale.y, localOffset.z/oldTransform.localScale.z);
			
			Matrix4x4 T = Matrix4x4.TRS( localOffset, Quaternion.identity, Vector3.one );			
			Matrix4x4 R = Matrix4x4.TRS( Vector3.zero, Quaternion.Inverse(rotation), Vector3.one );	
			
			EditPivot.MoveMesh( ref mesh, R * T);
						
			//modelImporterGo.transform.position -= offset;
			//modelImporterGo.transform.rotation *= rotation;				
		}
		
		modelpath = null;
		modelImporterGo = null;
		modelImporterGoName = "";
		matrixMapping.Clear();
		transformMapping.Clear();
	}

	static Matrix4x4 GetMatrix( ref Mesh mesh )
	{
		Matrix4x4 m = Matrix4x4.zero;

		Vector3 A = mesh.vertices[ mesh.triangles[0] ];
		Vector3 a = mesh.vertices[ mesh.triangles[1] ] - A; 
		Vector3 b = mesh.vertices[ mesh.triangles[2] ] - A;
		Vector3 n = Vector3.Cross( a, b );		
		 
		m.SetColumn(0, new Vector4( a.x, a.y, a.z, 0 ) );
		m.SetColumn(1, new Vector4( b.x, b.y, b.z, 0 ) );
		m.SetColumn(2, new Vector4( n.x, n.y, n.z, 0 ) );
		m.SetColumn(3, new Vector4( A.x, A.y, A.z, 1 ) );
		
		return m;		
	}
	
}
