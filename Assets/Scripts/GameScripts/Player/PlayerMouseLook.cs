using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouseLook : MonoBehaviour
{
    [field: SerializeField]
    Transform CameraTransform { get; set; }

    public float MouseSensitivity { get; set; } = 2;
    public float FOV 
    { 
        set
        {
            CameraTransform.gameObject.GetComponent<Camera>().fieldOfView = value;
        } 
    }

    float xRot = 45;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    public void OnLook(InputValue inputValue)
    {
        Vector2 toLook = inputValue.Get<Vector2>() / 2 * MouseSensitivity;

		xRot -= toLook.y;
		xRot = Mathf.Clamp(xRot, -90, 90);

		CameraTransform.localRotation = Quaternion.Euler(xRot, 0, 0) ;
		transform.Rotate(Vector3.up * toLook.x);
	}
}
