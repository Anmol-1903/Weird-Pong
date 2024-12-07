using UnityEngine;
public class SkyboxRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;

    void Update()
    {
        Material skyboxMaterial = RenderSettings.skybox;

        float currentRotation = skyboxMaterial.GetFloat("_Rotation");

        float newRotation = currentRotation + rotationSpeed * Time.deltaTime;

        skyboxMaterial.SetFloat("_Rotation", newRotation);
    }
}