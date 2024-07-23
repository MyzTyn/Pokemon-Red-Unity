[System.Serializable]
public class Item
{
    public ItemsEnum item;
    public int quantity;
    public bool isKeyItem;

    public Item(ItemsEnum item, int quantity, bool isKeyItem)
    {
        this.item = item;
        this.quantity = quantity;
        this.isKeyItem = isKeyItem;
    }
}