using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LBoxCollider2D), true)]
public class LBoxCollider2DEditor : LCollider2DEditor
{
    protected override void OnEnable()
    {
        base.OnEnable();
        LBoxCollider2D obj = serializedObject.targetObject as LBoxCollider2D;
        if (obj != null && 
            obj.gameObject.renderer != null &&
            obj.size.Equals(Vector2.one))
        {
            Vector3 size = obj.gameObject.renderer.bounds.size;
            obj.size = new Vector2(size.x / obj.transform.localScale.x, size.y / obj.transform.localScale.y);
        }
    }

    public override void OnInspectorGUI()
    {
        LBoxCollider2D obj = serializedObject.targetObject as LBoxCollider2D;
        obj.center = EditorGUILayout.Vector2Field("Center", obj.center);
        obj.size = EditorGUILayout.Vector2Field("Size", obj.size);
        serializedObject.ApplyModifiedProperties();
    }

    protected override void OnDrawSceneGUI()
    {
        base.OnDrawSceneGUI();
        LBoxCollider2D obj = serializedObject.targetObject as LBoxCollider2D;
        Vector2[] localVertics = new Vector2[4];
        Vector2 size2 = obj.size / 2;
        localVertics[0] = obj.center - size2;
        localVertics[1] = obj.center + new Vector2(size2.x, -size2.y);
        localVertics[2] = obj.center + size2;
        localVertics[3] = obj.center + new Vector2(-size2.x, size2.y);
        Vector3[] vertics = System.Array.ConvertAll<Vector2, Vector3>(localVertics, (Vector2 each) => obj.transform.TransformPoint(each));
        Handles.color = LineColor;
        Handles.DrawAAPolyLine(LineWidth, vertics);
        Handles.DrawAAPolyLine(LineWidth, new Vector3[] { vertics[0], vertics[3] });
        Handles.color = Color.white;
    }
}
