using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouseLook : MonoBehaviour
{
    [field: SerializeField]
    Transform CameraTransform { get; set; }
    public int MouseSensitivity { get; set; }

    float xRot = 45;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    public void OnLook(InputValue inputValue)
    {
        Vector2 toLook = inputValue.Get<Vector2>();

		xRot -= toLook.y;
		xRot = Mathf.Clamp(xRot, -90, 90);

		CameraTransform.localRotation = Quaternion.Euler(xRot, 0, 0);
		transform.Rotate(Vector3.up * toLook.x);
	}
}
