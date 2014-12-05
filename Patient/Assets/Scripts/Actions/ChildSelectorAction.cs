using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ChildSelectorAction : ActionRunner {
	
	private string rootGameObjectName;
	private string childGameObjectName;
	
	public ChildSelectorAction(string rootGameObjectName, string childGameObjectName){
		this.rootGameObjectName = rootGameObjectName;
		this.childGameObjectName = childGameObjectName;
		
		var root = GameObject.Find(rootGameObjectName);
		var child = GameObject.Find(childGameObjectName);
		Utilities.Assert(root != null, "Could not find root " + rootGameObjectName);
//		Utilities.Assert(child != null, "Could not find child " + childGameObjectName);
//		Utilities.Assert(child.transform.parent.gameObject == root, childGameObjectName + " not child of " + rootGameObjectName);
	}
	
	public override void Start () {
		var go = GameObject.Find(rootGameObjectName);
		var children = go.transform.Cast<Transform>().Select(x => x.gameObject);
		int found = 0;
		foreach (var child in children) {
			if (child.name == childGameObjectName) {
				child.SetActive(true);
				found += 1;
			} else {
				child.SetActive(false);
			}
		}	
		
		if (found != 1)
			Debug.LogError("Expected 1 child named " + childGameObjectName + " on " + rootGameObjectName + ", but got " + found);
		
		done ();
	}
	
	// Nothing to interrupt	
	public override void Interrupt () {}
}
