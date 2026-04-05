#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class RemoveGUILayerTool
{
    [MenuItem("Tools/Remove GUI Layers from All Scenes")]
    static void RemoveGUILayersFromAllScenes()
    {
        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes" });
        
        int removedCount = 0;
        
        foreach (string guid in sceneGuids)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(guid);
            
            // 開啟場景
            var scene = EditorSceneManager.OpenScene(scenePath);
            
            // 尋找所有 Camera
            var cameras = Object.FindObjectsOfType<Camera>();
            
            foreach (var camera in cameras)
            {
                // 嘗試獲取 GUILayer 組件（使用反射，因為它已過時）
                var guiLayer = camera.GetComponent("GUILayer");
                
                if (guiLayer != null)
                {
                    Object.DestroyImmediate(guiLayer, true);
                    removedCount++;
                    Debug.Log($"已從 {camera.name} 移除 GUI Layer (場景: {scenePath})");
                }
            }
            
            // 儲存場景
            EditorSceneManager.SaveScene(scene);
        }
        
        Debug.Log($"完成！共移除 {removedCount} 個 GUI Layer 組件");
    }
    
    [MenuItem("Tools/Remove GUI Layer from Current Scene")]
    static void RemoveGUILayersFromCurrentScene()
    {
        var scene = EditorSceneManager.GetActiveScene();
        var cameras = Object.FindObjectsOfType<Camera>();
        
        int removedCount = 0;
        
        foreach (var camera in cameras)
        {
            var guiLayer = camera.GetComponent("GUILayer");
            
            if (guiLayer != null)
            {
                Object.DestroyImmediate(guiLayer, true);
                removedCount++;
                Debug.Log($"已從 {camera.name} 移除 GUI Layer");
            }
        }
        
        EditorSceneManager.SaveScene(scene);
        Debug.Log($"完成！共移除 {removedCount} 個 GUI Layer 組件");
    }
}
#endif