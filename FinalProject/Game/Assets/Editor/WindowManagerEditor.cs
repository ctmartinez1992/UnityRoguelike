using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using System.Text;

[CustomEditor(typeof(WindowManager))]
public class WindowManagerEditor : Editor {

	private ReorderableList list;

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();

		if(GUILayout.Button("Generate Window Enums")) {
			GenericWindow[] windows = ((WindowManager)target).windows;
			int total = windows.Length;

			StringBuilder sb = new StringBuilder();
			sb.Append("public enum Windows {");
			sb.Append("None,");

			for(int i = 0; i < total; ++i) {
				sb.Append(windows[i].name.Replace(" ", ""));

				if(i < total - 1) {
					sb.Append(",");
				}
			}

			sb.Append("}");

			string path = EditorUtility.SaveFilePanel("Save The Window Enums", "", "WindowEnums.cs", "cs");

			using(FileStream fs = new FileStream(path, FileMode.Create)) {
				using(StreamWriter writer = new StreamWriter (fs)) {
					writer.Write(sb.ToString());
				}
			}
		}
	}

	private void OnEnable() {
		list = new ReorderableList(serializedObject, serializedObject.FindProperty("windows"), true, true, true, true);

		list.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField(rect, "Windows");
		};

		list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
			SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
			EditorGUI.PropertyField(new Rect(rect.x, rect.y, Screen.width - 75, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
		};
	}
}