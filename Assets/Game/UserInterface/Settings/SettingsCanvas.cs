using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooterProject.UserInterface
{
    public class SettingsCanvas : BaseCanvas
    {
        [SerializeField] private Slider volumeSlider;

        public delegate void SettingsVolumeChangeDelegate(float value);

        public event SettingsVolumeChangeDelegate OnVolumeValueChanged;

        protected override void Init()
        {
            volumeSlider.onValueChanged.AddListener(delegate (float sliderValue) { OnVolumeValueChanged(sliderValue); });
        }

        public float GetVolume()
        {
            return volumeSlider.value;
        }
    }
}
