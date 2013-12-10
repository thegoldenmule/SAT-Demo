using UnityEngine;

/// <summary>
/// The Cube MonoBehavior mainly provides an ISpatial implementation as well as
/// change the color when hit.
/// </summary>
public class Cube : MonoBehaviour
{
    /// <summary>
    /// Backing variable for the Hit property.
    /// </summary>
    private bool _hit;

    /// <summary>
    /// Sets whether or not the cube is being hit!
    /// </summary>
    public bool Hit
    {
        get
        {
            return _hit;
        }
        set
        {
            if (_hit != value)
            {
                _hit = value;

                ApplyColor();
            }
        }
    }

    /// <summary>
    /// The ICollidable implementation.
    /// </summary>
    public ICollidable Collider
    {
        get;
        private set;
    }

    /// <summary>
    /// Initializes the cube with transform information.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="scale"></param>
    /// <param name="rotation"></param>
    public void Initialize(
        Vector3 position,
        float scale,
        Quaternion rotation)
    {
        transform.position = position;
        transform.localScale = Vector3.one * scale;
        transform.rotation = rotation;

        Collider = new CollisionCube(gameObject);
    }

    /// <summary>
    /// Changes the shader uniforms.
    /// </summary>
    private void ApplyColor()
    {
        renderer.material.SetColor(
            "_Color",
            Hit ? Color.red : Color.white);
    }
}