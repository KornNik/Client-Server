using UnityEngine;
using UnityEngine.Networking;

public class PlayerLoader : NetworkBehaviour
{
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private PlayerController _controller;

    [SyncVar(hook = "HookUnitIdentity")] private NetworkIdentity _unitIdentity;

    public override void OnStartAuthority()
    {
        if (isServer)
        {
            GameObject unit = Instantiate(_unitPrefab);
            NetworkServer.Spawn(unit);
            _unitIdentity = unit.GetComponent<NetworkIdentity>();
            _controller.SetCharacter(unit.GetComponent<Character>(), true);
        }
        else { CmdCreatePlayer(); }
    }

    [Command]
    public void CmdCreatePlayer()
    {
        GameObject unit = Instantiate(_unitPrefab);
        NetworkServer.Spawn(unit);
        _unitIdentity = unit.GetComponent<NetworkIdentity>();
        _controller.SetCharacter(unit.GetComponent<Character>(), false);
    }

    [ClientCallback]
    private void HookUnitIdentity(NetworkIdentity unit)
    {
        if(isLocalPlayer)
        {
            _unitIdentity = unit;
            _controller.SetCharacter(unit.GetComponent<Character>(), true);
        }
    }
}
