using UnityEngine;

/// <summary>
/// An interface for collidable objects.
/// </summary>
public interface ICollidable
{
    /// <summary>
    /// The GameObject Transform.
    /// </summary>
    Transform Transform { get; }

    /// <summary>
    /// An array of unit length axes on which to check for separation.
    /// </summary>
    Vector3[] Axes { get; }

    /// <summary>
    /// An array of vertices in local space to check against.
    /// </summary>
    Vector3[] Vertices { get; }
}