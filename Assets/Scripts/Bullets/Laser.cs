using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int maxReflections = 10;
    public float max = 100f;
    public Transform cameraTransform;

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        Vector3 start1 = cameraTransform.position;
        start1 += new Vector3(1f,-1f);
        Ray ray0 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Vector3 direct = ray0.direction;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, start1);
        int i = 0;
        List<Vector3> ReflectPoint = new() { start1 };

        while (i < maxReflections)
        {
            Ray ray = new(start1, direct);

            if (Physics.Raycast(ray, out RaycastHit hit, max))
            {
                i++;
                start1 = hit.point;
                direct = Vector3.Reflect(direct, hit.normal);
                ReflectPoint.Add(start1);
            }
            else
            {
                ReflectPoint.Add(start1 + direct * max);
                break;
            }
        }

        lineRenderer.positionCount = ReflectPoint.Count;
        lineRenderer.SetPositions(ReflectPoint.ToArray());
    }
}
