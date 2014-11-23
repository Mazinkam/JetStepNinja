using UnityEngine;
using UnityEditor;
using System.Collections;

public class RevertFromDevelopmentBake : ScriptableWizard 
{
	public GameObject parentToCombinedObjects = null;
	
	[MenuItem("Purdyjo/Revert From Development Bake")]
	static void CreateWizard()
	{
		ScriptableWizard.DisplayWizard<RevertFromDevelopmentBake>("Revert Object", "Revert");
	}
	
	void OnWizardUpdate()
	{
	}
	
	//Export combined mesh
	void OnWizardCreate()
	{
		foreach(Renderer r in parentToCombinedObjects.GetComponentsInChildren<Renderer>())
		{
			r.enabled = true;
		}
	}
}
