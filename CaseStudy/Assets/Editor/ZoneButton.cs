using UnityEngine;
using UnityEditor;
using Zone;

namespace EditorScripts
{
    public class ZoneButton : EditorWindow
    {
        public string path = "Assets/Scripts/Zone/";
        private int count = 0;

        private void CreateScriptableObjects(int count)
        {
#if UNITY_EDITOR
            var obj = ScriptableObject.CreateInstance<ZoneSO>();

            obj.ZoneOrder = count;

            UnityEditor.AssetDatabase.CreateAsset(obj, path + $"Zone_{count}.asset");
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        [MenuItem("Window/ZoneButtonPanel")]
        public static void ShowWindow()
        {
            GetWindow<ZoneButton>("ZoneButton");
        }

        private void OnGUI()
        {
            GUILayout.Label("Create Scriptable Objects", EditorStyles.boldLabel);
            GUILayout.Label("How many zone will be created?", EditorStyles.label);
            count = EditorGUILayout.IntField(count);

            if (GUILayout.Button("Create"))
            {
                for (int i = 1; i < count + 1; i++)
                {
                    CreateScriptableObjects(i);
                }
            }
        }
    }
}