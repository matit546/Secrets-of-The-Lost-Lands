using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Dropdown resolutionsDropdown;
    public Slider slider;
    public Dropdown quality;
    public Toggle fullscreen;
    Resolution[] resolutions;
    public RenderPipelineAsset[] qualityLevel;
    private void Start()
    {
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        resolutionsDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        for(int i =0;i<resolutions.Length; i++)
        {

                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);
            
            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
        fullscreen.isOn = Screen.fullScreen;
        quality.value = QualitySettings.GetQualityLevel();
        slider.value = 0.3f;
    }

    public void SetVolume(float volume)
    {
        AudioManager.musicManagerInstance.SetVolume(volume);
        slider.value = volume;
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
        QualitySettings.renderPipeline = qualityLevel[quality];
    }

    public void Fullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int res)
    {
        Resolution resolution = resolutions[res];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }
}
