using UnityEngine;
using UnityEngine.UI;

public class MusicSettings : MonoBehaviour
{
    // загружаем и храним состояния музыки в игре(вкл/выкл)

    [SerializeField] private Toggle toggle;
    private int music;

    void Start()
    {
        LoadMusic();
    }

    public void Music()
    {
        if (toggle.isOn == true)
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("Music", 1);
        }
        if (toggle.isOn == false)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("Music", 0);
        }
    }

    private void LoadMusic()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            music = PlayerPrefs.GetInt("Music");
            if (music == 1)
            {
                toggle.isOn = true;
            }
            if (music == 0)
            {
                toggle.isOn = false;
            }
        }
    }
}
