namespace SpaceShooterProject.AI.Movements
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class QuadraticRoute : Route
    {
        public override Vector2 CalculateBezierCurve(float t)
        {
            Vector2 position = Mathf.Pow((1 - t), 2) * controlPoints[0] +
                2 * (1 - t) * t * controlPoints[1] + Mathf.Pow(t, 2) * controlPoints[2];

            return position;
        }

        //protected override void DrawGizmosLine()
        //{
        //    DrawQuadraticGizmosLine();
        //}

        protected override bool IsPointsSet()
        {
            return controlPoints.Count == 3;
        }
    }

}
