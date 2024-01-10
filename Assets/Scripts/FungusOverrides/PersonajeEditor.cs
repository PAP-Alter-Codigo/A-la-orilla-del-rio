using Fungus.EditorUtils;
using UnityEditor;

[CustomEditor(typeof(Personaje))]
public class PersonajeEditor: CharacterEditor {
    protected SerializedProperty beepSounds;

    protected override void OnEnable() {
        base.OnEnable();
        beepSounds = serializedObject.FindProperty("beepSounds");
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        EditorGUILayout.PropertyField(beepSounds);
    }
}