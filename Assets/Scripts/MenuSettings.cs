using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
//using System.IO;

public class MenuSettings : MonoBehaviour
{

    private PostProcessVolume m_Volume;
    private MotionBlur m_MotionBlur;
    private DepthOfField m_DepthOfField;
    private ChromaticAberration m_ChromaticAbberation;
    private float m_FogDistance;
    private Bloom m_Bloom;
    private ScreenSpaceReflections m_ScreenSpaceReflections;

    private GraphicsSettings gs;

    private void Start()
    {
        StartUp();
    }
    public void StartUp()
    {



        m_Volume = GameObject.Find("Main Camera").GetComponent<PostProcessVolume>();


        m_Volume.profile.TryGetSettings(out m_Bloom);

        m_Volume.profile.TryGetSettings(out m_MotionBlur);

        m_Volume.profile.TryGetSettings(out m_DepthOfField);

        m_Volume.profile.TryGetSettings(out m_ChromaticAbberation);

        m_Volume.profile.TryGetSettings(out m_ScreenSpaceReflections);

        Debug.Log(m_Bloom);

        LoadGraphicsSettings();
    }

    bool ConvertIntToBool(int input)
    {
        if (input == 1)
        { return true; }
        else
            return false;
    }

    public void quit()
    {
         Application.Quit();
    }
    public void writeToFile()
    {
        PlayerPrefs.SetInt("motionblur", m_MotionBlur.enabled.value ? 1 : 0);
        PlayerPrefs.SetInt("depthoffield", m_DepthOfField.enabled.value ? 1 : 0);
        PlayerPrefs.SetFloat("chromaticabberation", m_ChromaticAbberation.enabled.value ? 1 : 0);

        PlayerPrefs.SetFloat("fogdistance", m_FogDistance);
        //PlayerPrefs.SetFloat("shadow", QualitySettings.shadowDistance);

        PlayerPrefs.SetInt("bloom", m_Bloom.enabled.value ? 1 : 0);
        PlayerPrefs.SetInt("screenspacereflections", m_ScreenSpaceReflections.enabled.value ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadGraphicsFile()
    {
        GraphicsSettings graphicsSettings = new GraphicsSettings();
        graphicsSettings.m_motionBlur = ConvertIntToBool(PlayerPrefs.GetInt("motionblur"));
        graphicsSettings.m_depthOfField = ConvertIntToBool(PlayerPrefs.GetInt("depthoffield"));
        graphicsSettings.m_chromaticAbberation = ConvertIntToBool(PlayerPrefs.GetInt("chromaticabberation"));
        
        graphicsSettings.m_fogDistance = PlayerPrefs.GetFloat("fogdistance");
        //loadedCharacter.bullets = PlayerPrefs.GetFloat("bullets_CharacterSlot" + characterSlot);

        graphicsSettings.m_bloom = ConvertIntToBool(PlayerPrefs.GetInt("bloom"));
        graphicsSettings.m_screenSpaceReflections = ConvertIntToBool(PlayerPrefs.GetInt("screenspacereflections"));
        gs = graphicsSettings;
    }
    void LoadGraphicsSettings()
    {
        LoadGraphicsFile();//Load from file 
        SetGraphicsSettings( ); //and set as current profile

    }
    void SetGraphicsSettings()
    {
       
        m_MotionBlur.enabled.value = gs.m_bloom;
        m_DepthOfField.enabled.value = gs.m_depthOfField;
        m_ChromaticAbberation.enabled.value = gs.m_chromaticAbberation;
        RenderSettings.fogDensity = m_FogDistance;
        m_FogDistance = gs.m_fogDistance;
        m_Bloom.enabled.value = gs.m_bloom;
        m_ScreenSpaceReflections.enabled.value = gs.m_screenSpaceReflections;
        
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_MotionBlur,m_DepthOfField,m_ChromaticAbberation,m_Bloom,m_ScreenSpaceReflections); 
  
    }

    public void SetQuality( float quality)
    {
        Debug.Log("Quality:"+ quality);
        QualitySettings.SetQualityLevel((int)quality);
    }
    public void SetFog(float density)
    {
        m_FogDistance = density;
        Camera.main.farClipPlane = 2.0f/ density;
        RenderSettings.fogDensity = m_FogDistance;

        SetGraphicsSettings();
    }
    
    void Update()
    {
        
    }
    public void SetBloom(bool set)
    {
        Debug.Log(set);
        gs.m_bloom = set;
        SetGraphicsSettings();
    }
    public void SetMotionBlur(bool set)
    {
        Debug.Log(set);
        gs.m_motionBlur = set;
        SetGraphicsSettings();
    }
    public void SetChromaticAbberation(bool set)
    {
        Debug.Log(set);
        gs.m_chromaticAbberation = set;
        SetGraphicsSettings();
    }
    public void SetDepthOfField(bool set)
    {
        Debug.Log(set);
        gs.m_depthOfField = set;
        SetGraphicsSettings();
    }
    public void SetScreenSpaceReflections(bool set)
    {
        Debug.Log(set);
        gs.m_screenSpaceReflections = set;
        SetGraphicsSettings();
    }


    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Freeflight");
    }
}
