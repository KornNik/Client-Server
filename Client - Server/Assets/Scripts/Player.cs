using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Character _character;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Equipment _equipment;

    public Character Character { get { return _character; } }
    public Inventory Inventory { get { return _inventory; } }
    public Equipment Equipment { get { return _equipment; } }

    public void Setup(Character character, Inventory inventory, Equipment equipment, bool isLocalPlayer)
    {
        _character = character;
        _inventory = inventory;
        _equipment = equipment;
        _character.Player = this;
        _inventory.Player = this;
        _equipment.Player = this;

        if (isLocalPlayer)
        {
            InventoryUI.instance.SetInventory(_inventory);
            EquipmentUI.instance.SetEquipment(_equipment);
        }
    }
}
