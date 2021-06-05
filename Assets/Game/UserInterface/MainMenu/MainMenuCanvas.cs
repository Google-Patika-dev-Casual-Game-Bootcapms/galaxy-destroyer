using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace SpaceShooterProject.UserInterface
{
    public class MainMenuCanvas : BaseCanvas
    {
        public delegate void MenuRequestDelegate();

        public event MenuRequestDelegate OnInGameMenuRequest;
        public event MenuRequestDelegate OnSettingsMenuRequest;
        public event MenuRequestDelegate OnAchievementsMenuRequest;
        public event MenuRequestDelegate OnMarketMenuRequest;
        public event MenuRequestDelegate OnInventoryMenuRequest;
        public event MenuRequestDelegate OnGarageMenuRequest;
        public event MenuRequestDelegate OnCoPilotMenuRequest;
        public event MenuRequestDelegate OnCreditsMenuRequest;

        [SerializeField] private GameObject planets;
        [SerializeField] private float animationDuration;
        [SerializeField] private int activePlanetIndex = 0;
        [SerializeField] private TMP_Text planetName;
        [SerializeField] private RectTransform backgroundImage;

        private List<RectTransform> planetList;
        private bool planetMoveCompleted = true;

        protected override void Init()
        {
            backgroundImage.sizeDelta = GetCanvasSize();
            planetList = planets.GetComponentsInChildren<RectTransform>().ToList();
            planetName.text = planetList[0].gameObject.name;
        }

        public void RightButton()
        {
            if (planetMoveCompleted)
            {
                planetMoveCompleted = false;
                for (var i = 0; i < planetList.Count; i++)
                {
                    if (i == planetList.Count - 1)
                        MovePlanet(planetList[i], planetList[0]);
                    else
                        MovePlanet(planetList[i], planetList[i + 1]);
                }

                activePlanetIndex--;
                if (activePlanetIndex < 0)
                    activePlanetIndex = planetList.Count - 1;
                planetName.text = planetList[activePlanetIndex].gameObject.name;
            }
        }

        public void LeftButton()
        {
            if (planetMoveCompleted)
            {
                planetMoveCompleted = false;

                for (var i = 0; i < planetList.Count; i++)
                {
                    if (i == 0)
                        MovePlanet(planetList[i], planetList[planetList.Count - 1]);
                    else
                        MovePlanet(planetList[i], planetList[i - 1]);
                }

                activePlanetIndex++;
                if (activePlanetIndex > planetList.Count - 1)
                    activePlanetIndex = 0;
                planetName.text = planetList[activePlanetIndex].gameObject.name;
            }
        }

        public void MovePlanet(RectTransform self, RectTransform target)
        {
            self.DOMove(target.position, animationDuration)
                .SetEase(Ease.InOutSine);
            self.DOScale(target.localScale, animationDuration)
                .OnStart(() => { planetMoveCompleted = false; })
                .SetEase(Ease.InOutSine)
                .OnComplete(() => { planetMoveCompleted = true; });
        }

        private Vector2 GetCanvasSize()
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            CanvasScaler canvasScaler = GetComponent<CanvasScaler>();
            var m_ScreenMatchMode = canvasScaler.screenMatchMode;
            var m_ReferenceResolution = canvasScaler.referenceResolution;
            var m_MatchWidthOrHeight = canvasScaler.matchWidthOrHeight;

            float scaleFactor = 0;
            float logWidth = Mathf.Log(screenSize.x / m_ReferenceResolution.x, 2);
            float logHeight = Mathf.Log(screenSize.y / m_ReferenceResolution.y, 2);
            float logWeightedAverage = Mathf.Lerp(logWidth, logHeight, m_MatchWidthOrHeight);
            scaleFactor = Mathf.Pow(2, logWeightedAverage);

            return new Vector2(screenSize.x / scaleFactor, screenSize.y / scaleFactor);
        }

        public void RequestInGameMenu()
        {
            if (OnInGameMenuRequest != null)
            {
                OnInGameMenuRequest();
            }
        }

        public void RequestSettingsMenu()
        {
            if (OnSettingsMenuRequest != null)
            {
                OnSettingsMenuRequest();
            }
        }

        public void RequestAchievementsMenu()
        {
            if (OnAchievementsMenuRequest != null)
            {
                OnAchievementsMenuRequest();
            }
        }

        public void RequestMarketMenu()
        {
            if (OnMarketMenuRequest != null)
            {
                OnMarketMenuRequest();
            }
        }

        public void RequestInventoryMenu()
        {
            if (OnInventoryMenuRequest != null)
            {
                OnInventoryMenuRequest();
            }
        }

        public void RequestGarageMenu()
        {
            if (OnGarageMenuRequest != null)
            {
                OnGarageMenuRequest();
            }
        }

        public void RequestCoPilotMenu()
        {
            if (OnCoPilotMenuRequest != null)
            {
                OnCoPilotMenuRequest();
            }
        }

        public void RequestCreditsMenu()
        {
            if (OnCreditsMenuRequest != null)
            {
                OnCreditsMenuRequest();
            }
        }

        public GameObject ActivePlanet()
        {
            return planetList[activePlanetIndex].gameObject;
        }

        public string ActivePlanetName()
        {
            return ActivePlanet().name;
        }
    }
}