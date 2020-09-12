using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour
{
    [SerializeField] int _maxHealth;
    [SyncVar] int _curHealth;

    public override void OnStartAuthority()
    {
        _curHealth = _maxHealth;
    }
}
