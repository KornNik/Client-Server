using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] LayerMask movementMask;

    private Camera _camera;
    private Character _character;
    private int _leftMouseBtn = (int)MouseButton.LeftButton;
    private int _rightMouseBtn = (int)MouseButton.RightButton;


    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            if (_character != null)
            {
                if (Input.GetMouseButtonDown(_leftMouseBtn))
                {
                    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if(Physics.Raycast(ray, out hit, 100f, movementMask))
                        CmdSetMovePoint(hit.point);
                }
            }
        }
    }

    public void SetCharacter(Character character, bool isLocalPlayer)
    {
        _character = character;
        if (isLocalPlayer) { _camera.GetComponent<CameraController>().target = character.transform; }
    }

    [Command]
    public void CmdSetMovePoint(Vector3 point)
    {
        _character.SetMovePoint(point);
    }

    private void OnDestroy()
    {
        if (_character != null) { Destroy(_character.gameObject); }
    }
}
