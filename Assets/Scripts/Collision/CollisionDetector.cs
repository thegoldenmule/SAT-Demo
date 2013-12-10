using UnityEngine;

/// <summary>
/// Contains static methods for collision detection.
/// </summary>
public static class CollisionDetector
{
    /// <summary>
    /// When set, draws gizmos when IsCollision is called.
    /// </summary>
    public static bool DrawGizmos { get; set; }

    /// <summary>
    /// Detects if there is a collision between two ICollidable implementations.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool IsCollision(ICollidable a, ICollidable b)
    {
        Vector3[] aAxes = a.Axes;
        Vector3[] bAxes = b.Axes;

        if (DrawGizmos)
        {
            DrawAxes(aAxes);
            DrawAxes(bAxes);
        }

        int aAxesLength = aAxes.Length;
        int bAxesLength = bAxes.Length;

        Vector3[] aVertices = a.Vertices;
        Vector3[] bVertices = b.Vertices;

        int aVertsLength = aVertices.Length;
        int bVertsLength = bVertices.Length;

        bool hasOverlap = ProjectionHasOverlap(a.Transform, b.Transform, aAxesLength, ref aAxes, bVertsLength, ref bVertices, aVertsLength, ref aVertices, Color.red, Color.green);
        hasOverlap = ProjectionHasOverlap(b.Transform, a.Transform, bAxesLength, ref bAxes, aVertsLength, ref aVertices, bVertsLength, ref bVertices, Color.green, Color.red) && hasOverlap;

        return hasOverlap;
    }

    /// <summary>
    /// Detects whether or not there is overlap on all separating axes.
    /// </summary>
    /// <param name="aTransform"></param>
    /// <param name="bTransform"></param>
    /// <param name="aAxesLength"></param>
    /// <param name="aAxes"></param>
    /// <param name="bVertsLength"></param>
    /// <param name="bVertices"></param>
    /// <param name="aVertsLength"></param>
    /// <param name="aVertices"></param>
    /// <param name="aColor"></param>
    /// <param name="bColor"></param>
    /// <returns></returns>
    private static bool ProjectionHasOverlap(
        Transform aTransform,
        Transform bTransform,

        int aAxesLength,
        ref Vector3[] aAxes,

        int bVertsLength,
        ref Vector3[] bVertices,

        int aVertsLength,
        ref Vector3[] aVertices,
        
        Color aColor,
        Color bColor)
    {
        bool hasOverlap = true;

        for (int i = 0; i < aAxesLength; i++)
        {
            float bProjMin = float.MaxValue, aProjMin = float.MaxValue;
            float bProjMax = float.MinValue, aProjMax = float.MinValue;

            Vector3 axis = aAxes[i];

            for (int j = 0; j < bVertsLength; j++)
            {
                float val = FindScalarProjection(bTransform.TransformPoint(bVertices[j]), axis);

                if (val < bProjMin)
                {
                    bProjMin = val;
                }

                if (val > bProjMax)
                {
                    bProjMax = val;
                }
            }

            if (DrawGizmos)
            {
                Gizmos.color = bColor;
                Gizmos.DrawLine(bProjMin * axis, bProjMax * axis);
            }

            for (int j = 0; j < aVertsLength; j++)
            {
                float val = FindScalarProjection(aTransform.TransformPoint(aVertices[j]), axis);

                if (val < aProjMin)
                {
                    aProjMin = val;
                }

                if (val > aProjMax)
                {
                    aProjMax = val;
                }
            }

            if (DrawGizmos)
            {
                Gizmos.color = aColor;
                Gizmos.DrawLine(aProjMin * axis, aProjMax * axis);
            }

            float overlap = FindOverlap(aProjMin, aProjMax, bProjMin, bProjMax);
            if (overlap < Mathf.Epsilon)
            {
                hasOverlap = false;
            }
        }

        return hasOverlap;
    }

    /// <summary>
    /// Draws axes through the origin.
    /// </summary>
    /// <param name="axes"></param>
    private static void DrawAxes(Vector3[] axes)
    {
        const int LENGTH = 100;

        Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        for (int i = 0, len = axes.Length; i < len; i++)
        {
            Gizmos.DrawLine(-LENGTH / 2f * axes[i], LENGTH / 2f * axes[i]);
        }
    }

    /// <summary>
    /// Calculates the scalar projection of one vector onto another.
    /// </summary>
    /// <param name="point"></param>
    /// <param name="unitAxis"></param>
    /// <returns></returns>
    private static float FindScalarProjection(Vector3 point, Vector3 unitAxis)
    {
        return Vector3.Dot(point, unitAxis) / unitAxis.magnitude;
    }

    /// <summary>
    /// Calculates the amount of overlap of two intervals.
    /// </summary>
    /// <param name="astart"></param>
    /// <param name="aend"></param>
    /// <param name="bstart"></param>
    /// <param name="bend"></param>
    /// <returns></returns>
    private static float FindOverlap(float astart, float aend, float bstart, float bend)
    {
        if (astart < bstart)
        {
            if (aend < bstart)
            {
                return 0f;
            }

            return aend - bstart;
        }
        
        if (bend < astart)
        {
            return 0f;
        }

        return bend - astart;
    }
}