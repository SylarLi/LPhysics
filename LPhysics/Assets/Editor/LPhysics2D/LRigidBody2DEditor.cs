using UnityEditor;
using UnityEngine;
using System;

[CustomEditor(typeof(LRigidBody2D))]
public class LRigidBody2DEditor : Editor
{
    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    public override void OnInspectorGUI()
    {
        LRigidBody2D obj = serializedObject.targetObject as LRigidBody2D;

        obj.isFixed = EditorGUILayout.Toggle("Is Fixed", obj.isFixed);
        obj.mass = EditorGUILayout.FloatField("Mass", obj.mass);
        obj.inertia = EditorGUILayout.Slider("Inertia", obj.inertia, 0, 1);
        obj.linearDrag = EditorGUILayout.FloatField("Linear Drag", obj.linearDrag);
        obj.angularDrag = EditorGUILayout.FloatField("Angular Drag", obj.angularDrag);
        obj.gravityScale = EditorGUILayout.FloatField("Gravity Scale", obj.gravityScale);
        obj.centerOfMass = EditorGUILayout.Vector2Field("Center Of Mass", obj.centerOfMass);

        serializedObject.ApplyModifiedProperties();
    }
}
