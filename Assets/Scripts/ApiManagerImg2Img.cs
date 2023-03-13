using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MM.Msg;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ApiManagerImg2Img : Singleton<ApiManagerTxt2Img>
{
    public string bearer;
    public TMP_InputField promptInput;
    public TMP_InputField NegativepromptInput;


    public SliderController samplingStep;
    public SliderController width;
    public SliderController height;
    public SliderController cfgScale;
    public Toggle enable_hr;
    public TMP_InputField denoising_strength;
    public TMP_InputField firstphase_height;
    public TMP_InputField firstphase_width;
    public TMP_InputField seed;
    public TMP_InputField subseed;
    public TMP_InputField subseed_strength;
    public TMP_InputField seed_resize_from_h;
    public TMP_InputField seed_resize_from_w;
    public TMP_InputField batch_size;
    public TMP_InputField n_iter;
    public Toggle restore_faces;
    public Toggle tiling;
    public TMP_InputField eta;
    public TMP_InputField s_churn;
    public TMP_InputField s_tmax;
    public TMP_InputField s_tmin;
    public TMP_InputField s_noise;
    public TMP_InputField sampler_index;


    [HideInInspector] public string lastDownloadedBase64;


    public class Image2ImageRequest : BaseRequest
    {
        public List<string> init_images { get; set; }
        public int resize_mode { get; set; }
        public double denoising_strength { get; set; }

        public string mask { get; set; }
        public int mask_blur { get; set; }
        public int inpainting_fill { get; set; }
        public bool inpaint_full_res { get; set; }
        public int inpaint_full_res_padding { get; set; }
        public bool inpainting_mask_invert { get; set; }
        
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
        public OverrideSettingsImg override_settings { get; set; }
        public string sampler_index { get; set; }
        public bool include_init_images { get; set; }

        public override string PostUrl() =>
            "http://3.137.201.231:8080/sdapi/v1/img2img";
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
                init_images = { },
                resize_mode = 0,
                denoising_strength = Int32.Parse(denoising_strength.text),
                mask_blur = 4,
                inpainting_fill = 0,
                inpaint_full_res = false,
                inpaint_full_res_padding = 0,
                inpainting_mask_invert = false,
                styles = { },
                seed = Int32.Parse(seed.text),
                subseed = Int32.Parse(subseed.text),
                subseed_strength = Int32.Parse(subseed_strength.text),
                seed_resize_from_h = Int32.Parse(seed_resize_from_h.text),
                seed_resize_from_w = Int32.Parse(seed_resize_from_w.text),
                batch_size = Int32.Parse(batch_size.text),
                n_iter = Int32.Parse(n_iter.text),
                steps = samplingStep.SliderValue,
                cfg_scale = cfgScale.SliderValue,
                width = width.SliderValue,
                height = height.SliderValue,
                restore_faces = restore_faces.isOn,
                tiling = tiling.isOn,
                negative_prompt = NegativepromptInput.text,
                eta = Int32.Parse(eta.text),
                s_churn = Int32.Parse(s_churn.text),
                s_tmax = Int32.Parse(s_tmax.text),
                s_tmin = Int32.Parse(s_tmin.text),
                s_noise = Int32.Parse(s_noise.text),
                override_settings = { },
                sampler_index = sampler_index.text,
                include_init_images = false
            },
            msg =>
            {
                var response = msg as Image2ImageResponse;
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
}