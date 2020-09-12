using UnityEngine;
using UnityEngine.Networking;

public class NetUnitSetup : NetworkBehaviour
{
    [SerializeField] MonoBehaviour[] _disableBihaviours;

    private void Awake()
    {
        if (!hasAuthority)
        {
            for (int i = 0; i < _disableBihaviours.Length; i++)
            {
                _disableBihaviours[i].enabled = false;
            }
        }
    }

    public override void OnStartAuthority()
    {
        for (int i = 0; i < _disableBihaviours.Length; i++)
        {
            _disableBihaviours[i].enabled = true;
        }
    }
}
