using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace Lihter
{
    public class AudioMenu : MonoBehaviour
    {
        [SerializeField] private AudioMixer theMixer;
        [SerializeField] private TextMeshProUGUI[] audioLabel;
        [SerializeField] private Slider[] audioSlider;

        private void Start()
        {
            theMixer.GetFloat("MasterVol", out var vol);
            audioSlider[0].value = vol;
            audioLabel[0].text = Mathf.RoundToInt(audioSlider[0].value + 80).ToString();
            
            theMixer.GetFloat("MusicVol", out vol);
            audioSlider[1].value = vol;
            audioLabel[1].text = Mathf.RoundToInt(audioSlider[1].value + 80).ToString();
            
            theMixer.GetFloat("SfxVol", out vol);
            audioSlider[2].value = vol;
            audioLabel[2].text = Mathf.RoundToInt(audioSlider[2].value + 80).ToString();
        }
        
        public void SetMasterVolume()
        {
            audioLabel[0].text = Mathf.RoundToInt(audioSlider[0].value + 80).ToString();
            theMixer.SetFloat("MasterVol", audioSlider[0].value);
            PlayerPrefs.SetFloat("MasterVol", audioSlider[0].value);
        }

        public void SetMusicVolume()
        {
            audioLabel[1].text = Mathf.RoundToInt(audioSlider[1].value + 80).ToString();
            theMixer.SetFloat("MusicVol", audioSlider[1].value);
            PlayerPrefs.SetFloat("MusicVol", audioSlider[1].value);
        }

        public void SetSfxVolume()
        {
            audioLabel[2].text = Mathf.RoundToInt(audioSlider[2].value + 80).ToString();
            theMixer.SetFloat("SfxVol", audioSlider[2].value);
            PlayerPrefs.SetFloat("SfxVol", audioSlider[2].value);
        }
    }
}
