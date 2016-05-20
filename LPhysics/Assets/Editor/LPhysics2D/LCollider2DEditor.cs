using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LCollider2D), true)]
public class LCollider2DEditor : Editor
{
    public static readonly Color LineColor = new Color(0, 0.8f, 1, 1f);
    public const float LineWidth = 4;

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    public void OnSceneGUI()
    {
        if (serializedObject.targetObject != null &&
            (serializedObject.targetObject as LCollider2D).enabled)
        {
            OnDrawSceneGUI();
        }
    }

    protected virtual void OnDrawSceneGUI()
    {

    }
}
