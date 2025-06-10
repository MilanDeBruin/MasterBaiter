using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private AudioSource BGAudio;

    [Header("GUI")]
    [SerializeField] private Toggle AudioToggle;
    [SerializeField] private Slider VolumeSlider;

    void Update()
    {
        if(!SettingsMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape)){
            LoadSettings();
        }
    }

    private void LoadSettings()
    {
        AudioToggle.isOn = !BGAudio.mute;
        VolumeSlider.value = BGAudio.volume;
        SettingsMenu.SetActive(true);

    }

    public void SaveSettings()
    {
        BGAudio.mute = !AudioToggle.isOn;
        BGAudio.volume = VolumeSlider.value;
        SettingsMenu.SetActive(false);
    }

    
}
