namespace Destination
{
    [System.Serializable]
    public class Item
    {
        public string name;
        public int id = -1;

        public Item()
        {
            name = "";
            id = -1;
        }

        public Item(ItemObject _item)
        {
            name = _item.name;
            id = _item.data.id;
        }
    }
}