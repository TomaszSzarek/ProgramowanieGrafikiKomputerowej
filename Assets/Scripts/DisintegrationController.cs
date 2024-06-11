using UnityEngine;

public class DisintegrationController : MonoBehaviour
{
    public Material disintegrationMaterial;
    public Material originalMaterial;
    public float disintegrationDuration = 2.0f;
    private float disintegrationAmount = 1.0f;
    public bool isDisintegrating = false;

    public bool isChaningColor = false;
    private float changeColor = 0f;
    private Color startColor;
    private Color finalColor;
    private Material[] materials;
    private void Start()
    {
        originalMaterial = GetComponent<MeshRenderer>().material;
        startColor = originalMaterial.color;
        materials = GetComponent<MeshRenderer>().materials;
    }

    void Update()
    {
        if (isChaningColor)
        {
            Color finColor = Color.Lerp(disintegrationMaterial.color, disintegrationMaterial.GetColor("_Disintegration_Color"), 0.5f);
            if (changeColor < 1f)
            {
                changeColor += Time.deltaTime;
                float t = changeColor / 12f;

                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i].color = Color.Lerp(startColor, finColor, t);
                }
            }
            else
            {
                isChaningColor = false;
                isDisintegrating = true;

                Material newMaterial = new Material(disintegrationMaterial);
                newMaterial.SetTexture("_MainTex", originalMaterial.mainTexture);
                newMaterial.color = finColor;

                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] = newMaterial;
                }

                GetComponent<MeshRenderer>().materials = materials;
            }          
        }

        if (isDisintegrating)
        {
            disintegrationAmount -= Time.deltaTime / disintegrationDuration;

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetFloat("_DisintegrationAmount", disintegrationAmount);
            }

            if (disintegrationAmount <= 0f)
            {
                isDisintegrating = false;
                Destroy(gameObject);
            }
        }
    }

    public void StartDisintegration()
    {
        isChaningColor = true;
    }
}
