#if UNITY_EDITOR
namespace LB.UnityPackage.UI
{
    using UnityEditor;
    using UnityEngine;

    public class LB_AnchorHandler : MonoBehaviour
    {
        [MenuItem("AnchorHandler/Anchors to Corners")]
        static void AnchorsToCorners()
        {
            foreach (Transform transform in Selection.transforms)
            {
                var currentTransform = transform.GetComponent<RectTransform>();
                var parentTransform = Selection.activeTransform.parent.GetComponent<RectTransform>();

                if (currentTransform == null || parentTransform == null)
                {
                    return;
                }


                Vector2 newAnchorsMin = new Vector2(currentTransform.anchorMin.x + currentTransform.offsetMin.x / parentTransform.rect.width,
                                                    currentTransform.anchorMin.y + currentTransform.offsetMin.y / parentTransform.rect.height);
                Vector2 newAnchorsMax = new Vector2(currentTransform.anchorMax.x + currentTransform.offsetMax.x / parentTransform.rect.width,
                                                    currentTransform.anchorMax.y + currentTransform.offsetMax.y / parentTransform.rect.height);

                currentTransform.anchorMin = newAnchorsMin;
                currentTransform.anchorMax = newAnchorsMax;

                ResetOffsetMinMax(currentTransform);
            }
        }

        [MenuItem("AnchorHandler/Corners to Anchors")]
        static void CornersToAnchors()
        {
            foreach (Transform transform in Selection.transforms)
            {
                RectTransform currentTransform = transform.GetComponent<RectTransform>();

                if (currentTransform == null)
                {
                    return;
                }

                ResetOffsetMinMax(currentTransform);
            }
        }

        private static void ResetOffsetMinMax(RectTransform currentTransform)
        {
            var newOffset = new Vector2(0, 0);
            currentTransform.offsetMin = newOffset;
            currentTransform.offsetMax = newOffset;
        }
    }
}

#endif

