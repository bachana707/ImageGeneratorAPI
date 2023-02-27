using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ApiManager : Singleton<ApiManager>
{


    public void ConvertFromBase(string base64Image)
    {
        byte[]  imageBytes = Convert.FromBase64String(base64Image);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage( imageBytes );
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        UIManager.Instance.centralImage.sprite = sprite;
    }

    public void SaveImage(Sprite sprite)
    {
        string proj_path = Application.dataPath;
        var abs_path = Path.Combine(Application.dataPath, proj_path);
        proj_path = Path.Combine("Assets", proj_path);
 
        Directory.CreateDirectory(Path.GetDirectoryName(abs_path));
        File.WriteAllBytes(abs_path, ImageConversion.EncodeToPNG(sprite.texture));
 
        AssetDatabase.Refresh();
 
        var ti = AssetImporter.GetAtPath(proj_path) as TextureImporter;
        ti.spritePixelsPerUnit = sprite.pixelsPerUnit;
        ti.mipmapEnabled = false;
        ti.textureType = TextureImporterType.Sprite;
 
        EditorUtility.SetDirty(ti);
        ti.SaveAndReimport();
 
        //return AssetDatabase.LoadAssetAtPath<Sprite>(proj_path);  //Tu shenaxvis mere amogeba dagvchirda
    }
}
