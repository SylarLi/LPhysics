using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LCircleCollider2D))]
public class LCircleCollider2DEditor : LCollider2DEditor
{
    protected override void OnEnable()
    {
        base.OnEnable();
        LCircleCollider2D obj = serializedObject.targetObject as LCircleCollider2D;
        if (obj != null &&
            obj.gameObject.renderer != null &&
            obj.radius == 1)
        {
            Vector3 size = obj.gameObject.renderer.bounds.size;
            obj.radius = (size.x > size.y ? size.x / obj.transform.localScale.x : size.y / obj.transform.localScale.y) / 2;
        }
    }

    public override void OnInspectorGUI()
    {
        LCircleCollider2D obj = serializedObject.targetObject as LCircleCollider2D;
        obj.center = EditorGUILayout.Vector2Field("Center", obj.center);
        obj.radius = EditorGUILayout.FloatField("Radius", obj.radius);
        serializedObject.ApplyModifiedProperties();
    }

    protected override void OnDrawSceneGUI()
    {
        base.OnDrawSceneGUI();
        LCircleCollider2D obj = serializedObject.targetObject as LCircleCollider2D;
        Vector2 worldCenter = obj.transform.TransformPoint(obj.center);
        Vector3 localScale = obj.transform.localScale;
        float radius = obj.radius * (localScale.x > localScale.y ? localScale.x : localScale.y);
        Handles.color = LineColor;
        Handles.DrawWireArc(worldCenter, Vector3.forward, worldCenter + new Vector2(0, radius), 360, radius);
        Handles.color = Color.white;
    }
}
