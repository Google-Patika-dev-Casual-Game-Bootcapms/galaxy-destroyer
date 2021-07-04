using DG.Tweening;
using UnityEngine;

public class PlanetUI : MonoBehaviour
{
    [SerializeField] private float animationDuration;
    [SerializeField] private float activeScaleAmount;
    [SerializeField] private Ease moveEase;
    [SerializeField] private Ease scaleEase;
    [SerializeField] private RectTransform transform;

    public delegate void PlanetAnimationsCompleted();

    public event PlanetAnimationsCompleted OnAnimationsCompleted;

    private PlanetUI previousPlanet;
    private PlanetUI nextPlanet;
    private Tween moveTween;
    private Tween scaleTween;
    private bool isActive;

    public void Move(RectTransform self, RectTransform target)
    {
        moveTween = self.DOMove(target.position, animationDuration)
            .SetUpdate(UpdateType.Fixed)
            .SetEase(moveEase)
            .OnComplete(() =>
            {
                if (!isActive)
                    OnAnimationsCompleted();
            });

        scaleTween = self.DOScale(target.localScale, animationDuration)
            .SetUpdate(UpdateType.Fixed)
            .SetEase(scaleEase)
            .OnComplete(() => OnAnimationsCompleted());
    }

    public void KillTweens()
    {
        moveTween?.Kill(true);
        scaleTween?.Kill(true);
    }

    public PlanetUI PreviousPlanet
    {
        get => previousPlanet;
        set => previousPlanet = value;
    }

    public PlanetUI NextPlanet
    {
        get => nextPlanet;
        set => nextPlanet = value;
    }

    public void ScaleManually()
    {
        transform.localScale = Vector3.one * activeScaleAmount;
    }

    public void CompleteAllAnimations()
    {
        moveTween.Complete();
        scaleTween.Complete();
    }

    public RectTransform Transform => transform;

    public string Name => name;

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }
}