---------------------------------------------------------------------------------------------------------

Thank you for your interest in EditPivot!

A small helper extension to edit and manipulate mesh pivots right inside unity3d.
It features a complete gui and a simple script interface.

---------------------------------------------------------------------------------------------------------

Useage:
	
	select a gameobject with a mesh attached to it
	go to Menu>GameObject>Edit Pivot - a component editpivotbehaviour will be added to the gameobject
	the inspector features all informations and options regarding editpivot
	hit "apply pivot" to accept the new pivot
	
---------------------------------------------------------------------------------------------------------

Remarks:
	
	this tool does not work with skinned meshes.
	
	moving a mesh pivot, alters all prefabs which are using that mesh
	this can be turned off - results in a copied mesh, losing the link to the imported one
	
	be cautious with unity built-in primitives! edit pivot creates a new mesh when used, but these
	are stored in the scene, so you can't change the gameobject to a prefab. It works fine, if you
	convert your primitive to a prefab first, and then edit the pivot, a new mesh asset will
	be added to the project.
	
	to keep things persistent, edit pivot has to reimport the model each time you change the pivot.
	this can take a moment if the meshes are very complex. This also means, all edit pivot changes are
	undone when reimporting the model itself!
	
	Rotation of Pivots is on the local axis.
	
	3ds Max Fix - toggle under Menu>GameObject>Options>3ds Max Fix - all Models imported get rotated by
	90° on X Axis Vector3( 90,0,0 ) - so no default -90° Angle anymore on directly imported models! 

---------------------------------------------------------------------------------------------------------

Scripting:
	
	void MovePivot( GameObject go, Vector3 targetposition, Quaternion targetrotation, bool updateCollider=true, bool updatePrefab=true )
	Moves the pivot of the GameObject to the targetposition.
	
	void CenterToMesh( GameObject go, bool updateCollider = true, bool updatePrefab=true )
	enters the pivot to the center of the attached mesh.
	
	void CenterToChildren( GameObject go, bool updateCollider = true, bool updatePrefab=true )
	Centers the pivot to the center of all child transforms.
	
	updateCollider	if disabled the collider stays in the old place, you normaly want this enabled
	
	updatePrefab	if enabled the mesh asset gets updated, all prefabs using the mesh are affected.
					if disabled the mesh gets copied, and the link to the shared mesh is lost.
	
---------------------------------------------------------------------------------------------------------


Problems and Errors or Questions:

	Although I haven taken much care, this software might still have some bugs, feel free to bugreport!
	I will try to resolve as quickly as possible.

	If you've got other questions don't hesitate to contact me.
	
	Contact: support@game-bakery.com

---------------------------------------------------------------------------------------------------------

Have fun creating stuff. 

---------------------------------------------------------------------------------------------------------
	
	