namespace SpaceShooterProject.AI.Movements
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LinearRoute : Route
    {
        public override Vector2 CalculateBezierCurve(float t)
        {
            Vector2 position = (1 - t) * controlPoints[0].position + t * controlPoints[1].position;
            return position;
        }

        protected override void DrawGizmosLine()
        {
            DrawLinearGizmosLine();
        }

        protected override bool IsPointsSet()
        {
            return controlPoints.Length == 2;
        }
    }
}

