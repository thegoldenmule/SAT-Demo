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
        _cubeA.Initialize(
            Vector3.zero,
            Random.Range(1f, 10f),
            Quaternion.Euler(
                Random.Range(0f, 180f),
                Random.Range(0f, 180f),
                Random.Range(0f, 180f)));
        _cubeB.Initialize(
            Random.onUnitSphere * 10f,
            1f,
            Quaternion.Euler(
                Random.Range(0f, 180f),
                Random.Range(0f, 180f),
                Random.Range(0f, 180f)));
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
