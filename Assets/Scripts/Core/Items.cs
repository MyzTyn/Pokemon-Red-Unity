using PokemonUnity.Inventory;

[System.Serializable]
public class Item
{
    public Items item;
    public int quantity;
    public bool isKeyItem;

    public Item(Items item, int quantity, bool isKeyItem)
    {
        this.item = item;
        this.quantity = quantity;
        this.isKeyItem = isKeyItem;
    }
}