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
            Character character = CreateCharacter();
            _controller.SetCharacter(character, true);
            InventoryUI.instance.SetInventory(character.Inventory);
        }
        else { CmdCreatePlayer(); }
    }

    public Character CreateCharacter()
    {
        GameObject unit = Instantiate(_unitPrefab);
        NetworkServer.Spawn(unit);
        _unitIdentity = unit.GetComponent<NetworkIdentity>();
        unit.GetComponent<Character>().SetInventory(GetComponent<Inventory>());
        return unit.GetComponent<Character>();

    }
    public override bool OnCheckObserver(NetworkConnection connection)
    {
        return false;
    }

    [Command]
    public void CmdCreatePlayer()
    {
        _controller.SetCharacter(CreateCharacter(), false);
    }

    [ClientCallback]
    private void HookUnitIdentity(NetworkIdentity unit)
    {
        if(isLocalPlayer)
        {
            _unitIdentity = unit;
            Character character = unit.GetComponent<Character>();
            _controller.SetCharacter(character, true);
            character.SetInventory(GetComponent<Inventory>());
            InventoryUI.instance.SetInventory(character.Inventory);
        }
    }
}
