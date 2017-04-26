using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(DungeonManager))]
public class DungeonInspector : Editor {

    public override void OnInspectorGUI() {
        DungeonManager r = (DungeonManager)target;
        DrawDefaultInspector();

        /*
        if(GUILayout.Button("Renegerate")) {
            Destroy(r.boardHolder.gameObject);

            Dungeon d = r.GenerateDungeon();
            r.BuildDungeonGOs(d);
        }
        if(GUILayout.Button("New seed")) {
            r.seed = System.Environment.TickCount;
        }
        */
    }
}