using UnityEngine;

/// <summary>
/// Demos the separating axes approach.
/// 
/// Directions: Push play. In the scene view, move objects around.
/// 
/// Limitations:
/// 
/// * After object creation, the objects may not be rotated. This could easily
/// be remedied by returning new axes each frame, but would degrade
/// performance.
/// </summary>
public class ASTDemo : MonoBehaviour
{
    [SerializeField]
    private Cube _cubeA;

    [SerializeField]
    private Cube _cubeB;

    /// <summary>
    /// Called as part of the Unity lifecycle to initialize the object.
    /// </summary>
    private void Awake()
    {
        _cubeA.Initialize();
        _cubeB.Initialize();
    }

    /// <summary>
    /// Called to draw gizmos.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        CollisionDetector.DrawGizmos = true;

        _cubeA.Hit = _cubeB.Hit = CollisionDetector.IsCollision(_cubeA.Collider, _cubeB.Collider);
    }
}
