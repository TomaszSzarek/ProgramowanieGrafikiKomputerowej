using UnityEngine;

public class DisintegrationControllerTest : MonoBehaviour
{
    private DisintegrationController disintegrationController;
    void Start()
    {
        disintegrationController = GetComponent<DisintegrationController>();
        //disintegrationController.originalMaterial = meshRenderer.material;
        Invoke(nameof(StartDis), 5f);
    }

    private void StartDis()
    {
        disintegrationController.StartDisintegration();
    }
}
