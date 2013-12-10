using UnityEngine;

/// <summary>
/// Wraps a GameObject with an ICollidable implementation for a cube.
/// </summary>
public class CollisionCube : ICollidable
{
    private readonly GameObject _gameObject;

    /// <summary>
    /// The GameObject's transform.
    /// </summary>
    public Transform Transform
    {
        get
        {
            return _gameObject.transform;
        }
    }

    /// <summary>
    /// An array of all axes to check for separation.
    /// </summary>
    public Vector3[] Axes { get; private set; }

    /// <summary>
    /// An array of verts in local space.
    /// </summary>
    public Vector3[] Vertices { get; private set; }

    /// <summary>
    /// Constructor. Wraps a GameObject with a cube collision implementation.
    /// </summary>
    /// <param name="gameObject"></param>
    public CollisionCube(GameObject gameObject)
    {
        _gameObject = gameObject;

        Axes = new[]
        {
            _gameObject.transform.right,
            _gameObject.transform.up,
            _gameObject.transform.forward
        };

        Vertices = new[]
        {
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),

            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, -0.5f)
        };
    }
}