using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject 
{

    new public string name = "New Item";
    public Sprite Icon = null;
    public ItemPickup PickupPrefab;

    public virtual void Use() 
    {
        Debug.Log("Using " + name);
    }
}
