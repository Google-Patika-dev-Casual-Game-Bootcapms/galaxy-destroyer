using System.Collections.Generic;
using System.Linq;
using Devkit.Base.Object;
using TMPro;
using UnityEngine;

public class PlanetUIController : MonoBehaviour, IInitializable
{
    [SerializeField] private TMP_Text planetName;
    private List<PlanetUI> planets = new List<PlanetUI>();
    private bool allPlanetAnimationsCompleted = true;
    private int completedAnimationCounter;
    private PlanetUI activePlanet;
    private int activePlanetIndex = 0;

    public void PreInit()
    {
    }

    public void Init()
    {
        planets = GetComponentsInChildren<PlanetUI>().ToList();

        SetPlanetNeighbors(planets);

        ActivatePlanet(0);
    }

    public void SubscribeAllPlanetAnimationCompletionEvents()
    {
        planets.ForEach(planet => { planet.OnAnimationsCompleted += OnPlanetAnimationComplete; });
    }

    public void UnSubscribeAllPlanetAnimationCompletionEvents()
    {
        planets.ForEach(planet =>
        {
            planet.KillTweens();
            planet.OnAnimationsCompleted -= OnPlanetAnimationComplete;
        });
    }

    private void SetPlanetNeighbors(List<PlanetUI> planets)
    {
        planets[0].PreviousPlanet = planets[planets.Count - 1];
        planets[0].NextPlanet = planets[1];
        planets[planets.Count - 1].PreviousPlanet = planets[planets.Count - 2];
        planets[planets.Count - 1].NextPlanet = planets[0];

        for (var i = 1; i < planets.Count - 1; i++)
        {
            planets[i].NextPlanet = planets[i + 1];
            planets[i].PreviousPlanet = planets[i - 1];
        }
    }

    public void NextPlanet()
    {
        if (allPlanetAnimationsCompleted)
        {
            allPlanetAnimationsCompleted = false;
            planets.ForEach(planet =>
            {
                planet.Move(planet.Transform, planet.NextPlanet.Transform);
                if (planet.IsActive)
                {
                    planet.IsActive = false;
                    planet.NextPlanet.IsActive = true;
                    activePlanet = planet.PreviousPlanet;
                }
            });
            activePlanetIndex--;
            if (activePlanetIndex < 0)
                activePlanetIndex = planets.Count - 1;
            planetName.text = planets[activePlanetIndex].Name;
        }
    }

    public void PreviousPlanet()
    {
        if (allPlanetAnimationsCompleted)
        {
            allPlanetAnimationsCompleted = false;
            planets.ForEach(planet =>
            {
                planet.Move(planet.Transform, planet.PreviousPlanet.Transform);
                if (planet.IsActive)
                {
                    planet.IsActive = false;
                    planet.PreviousPlanet.IsActive = true;
                    activePlanet = planet.NextPlanet;
                }
            });
            activePlanetIndex++;
            if (activePlanetIndex > planets.Count - 1)
                activePlanetIndex = 0;
            planetName.text = planets[activePlanetIndex].Name;
        }
    }

    private void OnPlanetAnimationComplete()
    {
        completedAnimationCounter++;
        if (completedAnimationCounter == planets.Count)
        {
            allPlanetAnimationsCompleted = true;
            completedAnimationCounter = 0;
        }
    }

    public void CompleteAllPlanetAnimations()
    {
        planets.ForEach(planet => { planet.CompleteAllAnimations(); });
        allPlanetAnimationsCompleted = true;
    }

    public int GetSelectedPlanetIndex()
    {
        return activePlanetIndex;
    }

    public void UpdateUI(int selectedPlanetId)
    {
        return;//TODO: remove work-around

        if (selectedPlanetId < 0)
        {
            selectedPlanetId = 0;
        }

        if (selectedPlanetId >= planets.Count)
        {
            selectedPlanetId = 0;
        }

        ActivatePlanet(selectedPlanetId);
    }

    private void ActivatePlanet(int planetIndex)
    {
        activePlanet = planets[0];
        planets[0].IsActive = true;
        planets[0].ScaleManually();
    }
}