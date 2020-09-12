using UnityEngine;
using UnityEngine.Networking;

public class UnitLoader : NetworkBehaviour
{
    [SerializeField] GameObject _unitPrefab;

    public override void OnStartServer()
    {
        GameObject unit = Instantiate(_unitPrefab);
        NetworkServer.SpawnWithClientAuthority(unit, gameObject);
    }
}
