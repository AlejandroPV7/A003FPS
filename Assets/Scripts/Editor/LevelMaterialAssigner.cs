using UnityEditor;
using UnityEngine;

// Pone los materiales al nivel de Blender segun el nombre de cada objeto.
// Cada vez que guardo Level.blend, Unity lo reimporta y los materiales se vuelven a poner solos.
public class LevelMaterialAssigner : AssetPostprocessor
{
    const string carpeta = "Assets/Materials/Level/";

    void OnPostprocessModel(GameObject nivel)
    {
        if (!assetPath.EndsWith("Level.blend")) return;

        foreach (MeshRenderer r in nivel.GetComponentsInChildren<MeshRenderer>())
        {
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(carpeta + ElegirMaterial(r.name) + ".mat");
            if (mat == null) continue;

            Material[] mats = r.sharedMaterials;
            for (int i = 0; i < mats.Length; i++)
                mats[i] = mat;
            r.sharedMaterials = mats;
        }
    }

    string ElegirMaterial(string nombre)
    {
        if (nombre.StartsWith("Suelo")) return "Suelo";
        if (nombre.Contains("Rampa")) return "Rampa";
        if (nombre.Contains("Barandilla") || nombre.Contains("Pasarela")) return "Metal";
        if (nombre.StartsWith("Caja") || nombre.StartsWith("Palet") || nombre.StartsWith("Mesa")) return "Madera";
        if (nombre.StartsWith("Barril")) return "Barril";
        if (nombre.StartsWith("Contenedor")) return "Contenedor";
        if (nombre.StartsWith("Sacos")) return "Sacos";
        if (nombre.StartsWith("Jardinera")) return "Jardinera";
        if (nombre.StartsWith("Almacen")) return "Almacen";
        if (nombre.StartsWith("Edificio")) return "Edificio";
        if (nombre.StartsWith("ZonaA")) return "Ladrillo";
        if (nombre.StartsWith("ZonaB")) return "Militar";
        return "Hormigon";
    }
}
