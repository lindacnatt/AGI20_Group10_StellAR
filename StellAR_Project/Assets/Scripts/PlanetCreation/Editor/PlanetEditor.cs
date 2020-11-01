using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor{
    Planet planet;
    Editor shapeEditor;
    Editor colorEditor;
    Editor craterEditor;
    Editor noiseEditor;
    public override void OnInspectorGUI(){
        using(var check = new EditorGUI.ChangeCheckScope()){
            base.OnInspectorGUI();
            if(check.changed){
                planet.GeneratePlanet();
            }
        }
        if(GUILayout.Button("Generate Planet")){
            planet.GeneratePlanet();
        }
        DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated, ref planet.shapeSettingsFoldout, ref shapeEditor);
        DrawSettingsEditor(planet.colorSettings, planet.OnColorSettingsUpdated, ref planet.colorSettingsFoldout, ref colorEditor);
        DrawSettingsEditor(planet.craterSettings, planet.OnCraterSettingsUpdated, ref planet.craterSettingsFoldout, ref craterEditor);
        //DrawSettingsEditor(planet.noiseSettings, planet.OnShapeSettingsUpdated, ref planet.shapeSettingsFoldout, ref noiseEditor);
    }
    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor){
        if(settings != null){
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using(var check = new EditorGUI.ChangeCheckScope()){
                if(foldout){
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if(check.changed ){
                        if(onSettingsUpdated != null){
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }

    }
    private void OnEnable(){
        planet = (Planet)target;
    }
}
