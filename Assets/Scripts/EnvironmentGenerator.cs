using System.Data;
using UnityEngine;
using UnityEngine.U2D;


[ExecuteInEditMode]
public class EnvironmentGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private SpriteShapeController _spriteShapeController;
    [SerializeField, Range(3f, 100f)] private int levelLength = 50;
    [SerializeField, Range(1f, 50f)] private float xMultiplier = 2f;
    [SerializeField, Range(1f, 50f)] private float yMultiplier = 2f;
    [SerializeField, Range(0f, 1f)] private float curveSmoothness = 0.5f;
    [SerializeField] private float noiseStep = 0.5f;
    [SerializeField] private float bottom = 10f;

    private Vector3 lastPos;

    public void OnValidate()
    {
        _spriteShapeController.spline.Clear();

        for (int i = 0; i < levelLength; i++)
        {
            lastPos = transform.position + new Vector3(xMultiplier * i, Mathf.PerlinNoise(0, i * noiseStep) * yMultiplier);
            _spriteShapeController.spline.InsertPointAt(i, lastPos);

            if (i != 0 && i != levelLength - 1)
            {
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * xMultiplier * curveSmoothness);
                _spriteShapeController.spline.SetRightTangent(i, Vector3.right * xMultiplier * curveSmoothness);
            }
        }
        _spriteShapeController.spline.InsertPointAt(levelLength, new Vector3(lastPos.x, transform.position.y - bottom));
        _spriteShapeController.spline.InsertPointAt(levelLength + 1, new Vector3(transform.position.x, transform.position.y - bottom));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
