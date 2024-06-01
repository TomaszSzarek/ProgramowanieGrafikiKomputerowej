using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer line0;
    public int maxR = 10;
    public float max = 100f;
    public Transform camera1;
    public GameObject ParticlePrefab;
    private List<GameObject> Particles = new();
    void Start()
    {
        if (line0 == null)
        {
            line0 = GetComponent<LineRenderer>();
        }

        if (camera1 == null)
        {
            camera1 = Camera.main.transform;
        }
    }

    void Update()
    {
        Vector3 start1 = camera1.position;
        start1 += new Vector3(1f,-1f);
        Ray ray0 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Vector3 direct = ray0.direction;
        line0.positionCount = 1;
        line0.SetPosition(0, start1);
        int i = 0;
        List<Vector3> RPoint = new() { start1 };

        foreach (GameObject Particle in Particles)
        {
            Destroy(Particle);
        }
        Particles.Clear();

        while (i < maxR)
        {
            Ray ray = new(start1, direct);

            if (Physics.Raycast(ray, out RaycastHit hit, max))
            {
                i++;
                start1 = hit.point;
                direct = Vector3.Reflect(direct, hit.normal);
                RPoint.Add(start1);

                Vector3 sparkD = hit.normal; 
                Quaternion sparkR = Quaternion.LookRotation(sparkD);
                GameObject spark = Instantiate(ParticlePrefab, start1, sparkR);

                if (spark.TryGetComponent<ParticleSystem>(out var SPS))
                {
                    SPS.Clear();
                    SPS.Play();
                }
                Particles.Add(spark);
            }
            else
            {
                RPoint.Add(start1 + direct * max);
                break;
            }
        }

        line0.positionCount = RPoint.Count;
        line0.SetPositions(RPoint.ToArray());
    }
}
