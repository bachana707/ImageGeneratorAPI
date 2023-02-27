using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class ApiManager : Singleton<ApiManager>
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class txt2imgRequest
    {
        public bool enable_hr { get; set; }
        public int denoising_strength { get; set; }
        public int firstphase_width { get; set; }
        public int firstphase_height { get; set; }
        public string prompt { get; set; }
        public List<string> styles { get; set; }
        public int seed { get; set; }
        public int subseed { get; set; }
        public int subseed_strength { get; set; }
        public int seed_resize_from_h { get; set; }
        public int seed_resize_from_w { get; set; }
        public int batch_size { get; set; }
        public int n_iter { get; set; }
        public int steps { get; set; }
        public int cfg_scale { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public bool restore_faces { get; set; }
        public bool tiling { get; set; }
        public string negative_prompt { get; set; }
        public int eta { get; set; }
        public int s_churn { get; set; }
        public int s_tmax { get; set; }
        public int s_tmin { get; set; }
        public int s_noise { get; set; }
        public string sampler_index { get; set; }
    }

    public void Start()
    {
        txt2imgRequest test = new txt2imgRequest()
        {
            enable_hr = false,
            denoising_strength = 0,
            firstphase_height = 0,
            firstphase_width = 0,
            prompt = "dog in space",
            seed = -1,
            subseed = -1,
            subseed_strength = 0,
            seed_resize_from_h = -1,
            seed_resize_from_w = -1,
            batch_size = 1,
            n_iter = 1,
            steps = 1,
            cfg_scale = 7,
            width = 64,
            height = 64,
            restore_faces = false,
            tiling = false,
            negative_prompt = "",
            eta = 0,
            s_churn = 0,
            s_tmax = 0,
            s_tmin = 0,
            s_noise = 1,
            sampler_index = "Euler a"
        };
        test.enable_hr = false;


        StartCoroutine(PostRequest("http://3.20.221.132:8080/sdapi/v1/txt2img", JsonConvert.SerializeObject(test)));
    }


    public string bearer;

    IEnumerator PostRequest(string url, string data)
    {
        UnityWebRequest request;
        request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
        var headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };

        headers.Add("Authorization", "Bearer " + bearer);
        foreach (var item in headers)
        {
            request.SetRequestHeader(item.Key, item.Value);
        }
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
        request.downloadHandler = new DownloadHandlerBuffer();

        var op = request.SendWebRequest();
        

        while (op.isDone == false)
        {
            yield return null;
        }

        Debug.Log(request.downloadHandler.text);
        //
        // IPResponse ipResponse = JsonUtility.FromJson<IPResponse>(request.downloadHandler.text);
        // localHost = ipResponse.IPAddress;
    }


    public void ConvertFromBase(string base64Image)
    {
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imageBytes);
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f),
            100.0f);
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