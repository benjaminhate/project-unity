﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (GuardController)), CanEditMultipleObjects]
public class GuardControllerEditor : Editor {

	public SerializedProperty 
		state_Prop,
		waypoints_Prop,
		idle_Prop,
		innerState_Prop;

	void OnEnable(){
		state_Prop = serializedObject.FindProperty ("startingState");
		waypoints_Prop = serializedObject.FindProperty ("waypoints");
		idle_Prop = serializedObject.FindProperty ("idleDuration");
		innerState_Prop = serializedObject.FindProperty ("innerState");

	}

	// Draw the waypoins and their number
	void OnSceneGUI(){
		GuardController gc = (GuardController)target;
		int label = 0;
		Handles.color = Color.cyan;
		GUIStyle style = new GUIStyle ();
		style.fontSize = 15;
		style.normal.textColor = Color.black;
		if (gc.startingState == StartingState.Patrol) {
			foreach (Waypoint w in gc.waypoints) {
				Handles.DrawWireArc (w.position, Vector3.forward, Vector3.right, 360, .25f);
				Handles.Label (w.position, (label++).ToString(), style);
			}
		}
	}

	// Hide or show attribute depending on the StartingState
	override public void OnInspectorGUI(){
		serializedObject.Update ();

		GUI.enabled = false;
		EditorGUILayout.ObjectField ("Script", MonoScript.FromMonoBehaviour ((GuardController)target), typeof(GuardController), false);
		GUI.enabled = true;
		EditorGUILayout.PropertyField (innerState_Prop, true);

		EditorGUILayout.PropertyField (state_Prop);
		StartingState s = (StartingState)state_Prop.intValue;

		switch (s) {
		case StartingState.Patrol:
			EditorGUILayout.PropertyField (waypoints_Prop, true);
			EditorGUILayout.PropertyField (idle_Prop);
			break;
		case StartingState.Idle:
			EditorGUILayout.PropertyField (idle_Prop);
			break;
		}
		serializedObject.ApplyModifiedProperties ();
	}

}
