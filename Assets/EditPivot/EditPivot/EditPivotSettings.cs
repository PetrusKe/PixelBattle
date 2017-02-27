using UnityEngine;
using UnityEditor;
using System.Collections;

[AddComponentMenu("")]
public class EditPivotSettings : MonoBehaviour
{
	static EditPivotSettings instance;
	public static EditPivotSettings Instance 
	{ 
		get
		{
			if( instance == null )
			{
				try
				{
				 	instance = (EditPivotSettings)UnityEngine.Object.FindObjectsOfTypeIncludingAssets( typeof(EditPivotSettings) )[0];
				}
				catch
				{				
					UnityEngine.Object[] allTextAssets = UnityEngine.Object.FindObjectsOfTypeIncludingAssets( typeof(TextAsset) );
					string path ="";
					for( int i=0; i<allTextAssets.Length; i++ )
					{					
						if( allTextAssets[i].name == "EditPivotSettings" && allTextAssets[i].GetType() == typeof(MonoScript) )
						{
							path =  AssetDatabase.GetAssetPath( allTextAssets[i] );
							path = path.Substring(0, path.Length - "EditPivotSettings.cs".Length );
						}
					}
						
					if( path != "" )
					{						
						GameObject go = new GameObject( "EditPivotSettings" );	
						GameObject prefab = PrefabUtility.CreatePrefab( path + "EditPivotSettings.prefab" , go);						
						DestroyImmediate( go );			
						
						instance = prefab.AddComponent<EditPivotSettings>();	
					}
					else
					{
						throw new  System.Exception( "Cannot Create EditPivot Settings!" );
					}
				}			
			}
			
			return instance;
		}
	}
	
	[SerializeField] public bool maxFix = false;
	[SerializeField] public bool updateCollider = true;
	[SerializeField] public bool updatePrefab = true;
	
	public static bool MaxFix { get {return Instance.maxFix;} }
	public static bool UpdateCollider { get {return Instance.updateCollider;} }
	public static bool UpdatePrefab { get {return Instance.updatePrefab;} }
}

