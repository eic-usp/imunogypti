using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DendriticaInstallerScript))]
public class DendriticaInstallerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DendriticaInstallerScript myScript = (DendriticaInstallerScript)target;
        if(GUILayout.Button("Install Celula Dendritica"))
        {
            myScript.InstallDendritica();
        }
    }
}
