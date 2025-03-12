using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float distance = 5;

    [SerializeField] float minVerticalAngle = -45;
    [SerializeField] float maxVerticalAngle = 45;

    [SerializeField] Vector2 framingOffset;
    [SerializeField] bool invertX;
    [SerializeField] bool invertY;
    
    [SerializeField] float minHeight = 0.5f; // Hauteur minimale pour la caméra

    float rotationX;
    float rotationY;
    float invertXVal;
    float invertYVal;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        invertXVal = invertX ? -1 : 1;
        invertYVal = invertY ? -1 : 1;

        // Gestion des rotations
        rotationX += Input.GetAxis("Mouse Y") * invertYVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        rotationY += Input.GetAxis("Mouse X") * invertXVal * rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        // Position ciblée avec l'offset
        var focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

        // Calcul de la position de la caméra
        Vector3 cameraPosition = focusPosition - targetRotation * new Vector3(0, 0, distance);

        // Empêcher la caméra de descendre sous un certain seuil (ex: les pieds du joueur)
        cameraPosition.y = Mathf.Max(cameraPosition.y, followTarget.position.y + minHeight);

        // Appliquer position et rotation
        transform.position = cameraPosition;
        transform.rotation = targetRotation;
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);

public Quaternion GetPlanarRotation()
{
    return Quaternion.Euler(0, rotationY, 0);
}
}   