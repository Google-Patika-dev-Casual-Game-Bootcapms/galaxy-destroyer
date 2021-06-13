using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooterProject.UserInterface
{
    public class SettingsCanvas : BaseCanvas
    {
        [SerializeField] private Slider volumeSlider;

        protected override void Init()
        {

        }

        public float GetVolume()
        {
            return volumeSlider.value;
        }
    }
}
