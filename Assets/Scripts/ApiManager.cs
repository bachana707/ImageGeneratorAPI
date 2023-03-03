using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MM.Msg;
using UnityEditor;
using UnityEngine;

public class ApiManager : Singleton<ApiManager>
{
    public string bearer;


    public SliderController samplingStep;
    public SliderController width;
    public SliderController height;
    public SliderController cfgScale;

    [HideInInspector] public string lastDownloadedBase64;

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Txt2ImageRequest : BaseRequest
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
        public float steps { get; set; }
        public float cfg_scale { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public bool restore_faces { get; set; }
        public bool tiling { get; set; }
        public string negative_prompt { get; set; }
        public int eta { get; set; }
        public int s_churn { get; set; }
        public int s_tmax { get; set; }
        public int s_tmin { get; set; }
        public int s_noise { get; set; }
        public string sampler_index { get; set; }

        public override string PostUrl() =>
            "http://3.20.221.132:8080/sdapi/v1/txt2img";
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Parameters
    {
        public bool enable_hr { get; set; }
        public double denoising_strength { get; set; }
        public int firstphase_width { get; set; }
        public int firstphase_height { get; set; }
        public double hr_scale { get; set; }
        public object hr_upscaler { get; set; }
        public int hr_second_pass_steps { get; set; }
        public int hr_resize_x { get; set; }
        public int hr_resize_y { get; set; }
        public string prompt { get; set; }
        public List<object> styles { get; set; }
        public int seed { get; set; }
        public int subseed { get; set; }
        public double subseed_strength { get; set; }
        public int seed_resize_from_h { get; set; }
        public int seed_resize_from_w { get; set; }
        public object sampler_name { get; set; }
        public int batch_size { get; set; }
        public int n_iter { get; set; }
        public int steps { get; set; }
        public double cfg_scale { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public bool restore_faces { get; set; }
        public bool tiling { get; set; }
        public string negative_prompt { get; set; }
        public double eta { get; set; }
        public double s_churn { get; set; }
        public double s_tmax { get; set; }
        public double s_tmin { get; set; }
        public double s_noise { get; set; }
        public object override_settings { get; set; }
        public bool override_settings_restore_afterwards { get; set; }
        public List<object> script_args { get; set; }
        public string sampler_index { get; set; }
        public object script_name { get; set; }
    }

    public class Txt2ImageResponse : BaseResponse
    {
        public List<string> images { get; set; }
        public Parameters parameters { get; set; }
        public string info { get; set; }
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class OverrideSettings
    {
    }

    public class Image2ImageRequest : BaseRequest
    {
        public List<string> init_images { get; set; }
        public int resize_mode { get; set; }
        public double denoising_strength { get; set; }
        public int mask_blur { get; set; }
        public int inpainting_fill { get; set; }
        public bool inpaint_full_res { get; set; }
        public int inpaint_full_res_padding { get; set; }
        public bool inpainting_mask_invert { get; set; }
        public List<object> styles { get; set; }
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
        public OverrideSettings override_settings { get; set; }
        public string sampler_index { get; set; }
        public bool include_init_images { get; set; }

        public override string PostUrl() =>
            "http://3.20.221.132:8080/sdapi/v1/img2img";
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class OverrideSettingsImg
    {
    }

    public class ParametersImg
    {
        public object init_images { get; set; }
        public int resize_mode { get; set; }
        public double denoising_strength { get; set; }
        public object image_cfg_scale { get; set; }
        public object mask { get; set; }
        public int mask_blur { get; set; }
        public int inpainting_fill { get; set; }
        public bool inpaint_full_res { get; set; }
        public int inpaint_full_res_padding { get; set; }
        public int inpainting_mask_invert { get; set; }
        public object initial_noise_multiplier { get; set; }
        public string prompt { get; set; }
        public List<object> styles { get; set; }
        public int seed { get; set; }
        public int subseed { get; set; }
        public double subseed_strength { get; set; }
        public int seed_resize_from_h { get; set; }
        public int seed_resize_from_w { get; set; }
        public object sampler_name { get; set; }
        public int batch_size { get; set; }
        public int n_iter { get; set; }
        public int steps { get; set; }
        public double cfg_scale { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public bool restore_faces { get; set; }
        public bool tiling { get; set; }
        public string negative_prompt { get; set; }
        public double eta { get; set; }
        public double s_churn { get; set; }
        public double s_tmax { get; set; }
        public double s_tmin { get; set; }
        public double s_noise { get; set; }
        public OverrideSettingsImg override_settings { get; set; }
        public bool override_settings_restore_afterwards { get; set; }
        public List<object> script_args { get; set; }
        public string sampler_index { get; set; }
        public bool include_init_images { get; set; }
        public object script_name { get; set; }
    }

    public class Image2ImageResponse : BaseResponse
    {
        public List<string> images { get; set; }
        public ParametersImg parameters { get; set; }
        public string info { get; set; }
    }

    public void SendRequestImg2Img(string txt, Action callback)
    {
        NetCenter.Instance.Send<Image2ImageResponse>(new Image2ImageRequest()
            {
                resize_mode = 0,
                denoising_strength = 0.75,
                mask_blur = 4,
                inpainting_fill = 0,
                inpaint_full_res = false,
                inpaint_full_res_padding = 0,
                inpainting_mask_invert = false,
                //styles = [],
                seed = -1,
                subseed = -1,
                subseed_strength = 0,
                seed_resize_from_h = -1,
                seed_resize_from_w = -1,
                batch_size = 1,
                n_iter = 1,
                steps = 3,
                cfg_scale = 7,
                width = 512,
                height = 512,
                restore_faces = false,
                tiling = false,
                negative_prompt = "",
                eta = 0,
                s_churn = 0,
                s_tmax = 0,
                s_tmin = 0,
                s_noise = 1,
                override_settings = { },
                sampler_index = "Euler a",
                include_init_images = false
            },
            msg =>
            {
                var response = msg as Txt2ImageResponse;
                callback?.Invoke();
                ConvertFromBase(response.images[0]);
            }, e => { Debug.Log(e); });
    }

    public void SendRequestTxt2Img(string txt, Action callback)
    {
        NetCenter.Instance.Send<Txt2ImageResponse>(new Txt2ImageRequest()
            {
                enable_hr = false,
                denoising_strength = 0,
                firstphase_height = 0,
                firstphase_width = 0,
                prompt = txt,
                seed = -1,
                subseed = -1,
                subseed_strength = 0,
                seed_resize_from_h = -1,
                seed_resize_from_w = -1,
                batch_size = 1,
                n_iter = 1,
                steps = samplingStep.SliderValue,
                cfg_scale = cfgScale.SliderValue,
                width = width.SliderValue,
                height = height.SliderValue,
                restore_faces = false,
                tiling = false,
                negative_prompt = "",
                eta = 0,
                s_churn = 0,
                s_tmax = 0,
                s_tmin = 0,
                s_noise = 1,
                sampler_index = "Euler a"
            },
            msg =>
            {
                var response = msg as Txt2ImageResponse;
                callback?.Invoke();
                ConvertFromBase(response.images[0]);
            }, e => { Debug.Log(e); });
    }

    public void ConvertFromBase(string base64Image)
    {
        lastDownloadedBase64 = base64Image;
        Debug.Log(base64Image);
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        Texture2D tex = new Texture2D(512, 512);
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