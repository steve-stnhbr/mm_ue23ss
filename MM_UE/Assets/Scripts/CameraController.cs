using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraController : MonoBehaviour
{
    [Tooltip("This value describes how fast the Camera turns per second")]
    public float yawSpeedPerSecond = 120f;
    [Tooltip("This value describes the transform of the orbit center, around which the camera rotates")]
    public Transform orbitCenter;
    [Tooltip("Sets if the human can rotate the camera")]
    public bool canHumanRotate = true;
    [Tooltip("Sets if the wizard can rotate the camera")]
    public bool canWizardRotate = true;

    [Header("Focus (Optional)")]
    [Tooltip("The transform of the first focus, if set, the camera orbits around its position")]
    public Transform focusOne;
    [Tooltip("The transform of the second focus, if set, the camera orbits around the middle of both foci")]
    public Transform focusTwo;
    

    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (focusOne!=null)
        {
            handleFocus();
        }

        float humanAngleInput = canHumanRotate ? Input.GetAxis("HumanCameraYaw"): 0;
        float wizardAngleInput = canWizardRotate && Input.GetAxis("WizardCameraButton")>0 ? Input.GetAxis("Mouse X") : 0;

        float angleChange = humanAngleInput + wizardAngleInput * Time.deltaTime * yawSpeedPerSecond;
        camera.transform.RotateAround(orbitCenter.position, new Vector3(0, 1, 0), angleChange);
        
    }

    void handleFocus()
    {
        Vector3 newOrbitPosition = orbitCenter.position;
        if (focusTwo == null)
        {
            newOrbitPosition = focusOne.position;
        }
        else
        {
            newOrbitPosition = (focusOne.position + focusTwo.position) / 2;
        }
        
        Vector3 focusDifference = newOrbitPosition - orbitCenter.position;
        transform.position += focusDifference;


    }

}
