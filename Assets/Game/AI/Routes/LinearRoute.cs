namespace SpaceShooterProject.AI.Movements
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LinearRoute : Route
    {
        public override Vector2 CalculateBezierCurve(float t)
        {
            Debug.Log("Calculation");
            Vector2 position = (1 - t) * controlPoints[0] + t * controlPoints[1];
            Debug.Log(position);
            return position;
        }

        //protected override void DrawGizmosLine()
        //{
        //    //DrawLinearGizmosLine();
        //}

        protected override bool IsPointsSet()
        {
            return controlPoints.Count == 2;
        }
    }
}

