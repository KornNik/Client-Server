using UnityEngine;

[RequireComponent(typeof(UnitMotor))]
public class PlayerController : MonoBehaviour
{

    private int _leftMouseBtn = (int)MouseButton.LeftButton;
    private int _rightMouseBtn = (int)MouseButton.RightButton;

    [SerializeField] LayerMask movementMask;

    Camera cam;
    UnitMotor motor;

    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<UnitMotor>();
        cam.GetComponent<CameraController>().target = transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseBtn))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, movementMask))
            {
                motor.MoveToPoint(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(_rightMouseBtn))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {

            }
        }
    }
}
