using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Lihter
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Toggle fullscreenTog, vsyncTog;

        [SerializeField] private List<ResItem> resolutions = new List<ResItem>();
        [FormerlySerializedAs("qualitys")] [SerializeField] private List<string> qualities = new List<string>();

        [SerializeField] private Text resolutionLabel, qualityLabel;

        private int _currentResolutionIndex;
        private int _currentQualityIndex;
        
        private void Start()
        {
            fullscreenTog.isOn = Screen.fullScreen;

            vsyncTog.isOn = QualitySettings.vSyncCount != 0;

            var foundRes = false;
            for (var i = 0; i < resolutions.Count; i++)
            {
                if (Screen.width != resolutions[i].horizontal || Screen.height != resolutions[i].vertical) continue;
                foundRes = true;
                _currentResolutionIndex = i;
                UpdateResolutionLabel();
            }

            if (foundRes) return;
            var newRes = new ResItem {
                horizontal = Screen.width,
                vertical = Screen.height
            };

            resolutions.Add(newRes);
            _currentResolutionIndex = resolutions.Count - 1;

            UpdateResolutionLabel();
        }

        public void ApplyGraphics()
        {
            Screen.SetResolution(resolutions[_currentResolutionIndex].horizontal, resolutions[_currentResolutionIndex].vertical, fullscreenTog.isOn);

            QualitySettings.SetQualityLevel(_currentQualityIndex);

            QualitySettings.vSyncCount = vsyncTog.isOn ? 1 : 0;
        }

        public void ResolutionPrevious()
        {
            _currentResolutionIndex--;
            if(_currentResolutionIndex < 0)
            {
                _currentResolutionIndex = 0;
            }
            UpdateResolutionLabel();
        }

        public void ResolutionNext()
        {
            _currentResolutionIndex++;
            if(_currentResolutionIndex > resolutions.Count - 1)
            {
                _currentResolutionIndex = resolutions.Count - 1;
            }
            UpdateResolutionLabel();
        }

        private void UpdateResolutionLabel()
        {
            resolutionLabel.text = resolutions[_currentResolutionIndex].horizontal.ToString() + " x " + resolutions[_currentResolutionIndex].vertical.ToString();
        }

        public void QualityPrevious()
        {
            _currentQualityIndex--;
            if(_currentQualityIndex < 0)
            {
                _currentQualityIndex = 0;
            }
            UpdateQualityLabel();
        }

        public void QualityNext()
        {
            _currentQualityIndex++;
            if(_currentQualityIndex > qualities.Count - 1)
            {
                _currentQualityIndex = qualities.Count - 1;
            }
            UpdateQualityLabel();
        }

        private void UpdateQualityLabel()
        {
            qualityLabel.text = qualities[_currentQualityIndex];
        }
    }

    [System.Serializable]
    public class ResItem 
    {
        public int horizontal, vertical;
    }
}