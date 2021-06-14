using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject options;
    public AudioMixer audioMixer;
    
    public void PlayAction()
    { 
        SceneManager.LoadScene("InitGame");
    }
    
    public void OptionsAction()
    { 
        options.SetActive(true);
    }
    
    public void ExitAction()
    {
        Application.Quit();
    }

    public void ExitOptionsAction()
    {
        options.SetActive(false);
    }

    public void MuteAll(bool mute)
    {
        AudioListener.pause = mute;
    }

    public void VolumeSlider(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
