using UnityEngine;

public class ItemPickup : Interactable 
{

    public Item Item;

    public override bool Interact(GameObject user) 
    {
        return PickUp(user);
    }

    public bool PickUp(GameObject user) 
    {
        Character character = user.GetComponent<Character>();
        if (character != null && character.Inventory.Add(Item))
        {
            Destroy(gameObject);
            return true;
        }
        else { return false; }
    }
}
