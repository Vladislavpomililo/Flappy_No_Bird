using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSettings : MonoBehaviour
{
    // Храним и загружаем разрешение экрана и полноэкранность.

    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Toggle windowsMode;
    private Resolution[] resolutionsToArray;

    void Start()
    {
        if (PlayerPrefs.HasKey("FullScreen"))
        {
            if(PlayerPrefs.GetInt("FullScreen") == 0)
            {
                Screen.fullScreen = false;
            }
            else
            {
                Screen.fullScreen = true;
            }
        }
        else
        {
            Screen.fullScreen = true;
        }
        windowsMode.isOn = !Screen.fullScreen;

        Resolution[] resolutions = Screen.resolutions;
        resolutionsToArray = resolutions.Distinct().ToArray();
        string[] res = new string[resolutionsToArray.Length];
        for (int i = 0; i < resolutionsToArray.Length; i++)
        {
            res[i] = resolutionsToArray[i].width.ToString() + "x" + resolutionsToArray[i].height.ToString();
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(res.ToList());

        if (PlayerPrefs.HasKey("Resolution"))
        {
            dropdown.value = PlayerPrefs.GetInt("Resolution");
            Screen.SetResolution(resolutionsToArray[dropdown.value].width, resolutionsToArray[dropdown.value].height, Screen.fullScreen);
        }
        else
        {
            Screen.SetResolution(resolutionsToArray[resolutionsToArray.Length - 1].width, resolutionsToArray[resolutionsToArray.Length - 1].height, Screen.fullScreen);
            dropdown.value = resolutionsToArray.Length - 1;
        }
    }

    public void SetRes()
    {
        Screen.SetResolution(resolutionsToArray[dropdown.value].width, resolutionsToArray[dropdown.value].height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", dropdown.value);
    }
    public void ScreenMod()
    {
        Screen.fullScreen = !windowsMode.isOn;

        if(Screen.fullScreen)
        {
            PlayerPrefs.SetInt("FullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 0);
        }
    }
}
