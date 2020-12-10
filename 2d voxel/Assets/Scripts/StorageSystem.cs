using System.Collections.Generic;
using UnityEngine;

public class StorageSystem : MonoBehaviour
{
    //width and height of storage
    public Vector2Int storageSize;

    //storage slots
    public SlotData[,] slots;

    private void Initialize(SlotData[,] _slots)
    {
        //checks for save data and creates slots
        if (_slots != null)
            slots = _slots;
        else
            slots = new SlotData[storageSize.x, storageSize.y];
    }

    public bool AddItems(int type, int amount)
    {
        //just in case there are no items to add, why waste the memory :)
        if (amount <= 0) return false;

        int maxAmount = WorldManager.instance.items[type].maxAmount;
        List<Vector2Int> emptySlot = new List<Vector2Int>();

        //scan slots
        for (int y = 0; y < storageSize.y; y++)
        {
            for (int x = 0; x < storageSize.x; x++)
            {
                //if the slot's item is the same as being added and is not full, begin combining
                if (slots[x, y].item == type && slots[x, y].amount < maxAmount)
                {
                    //if slot has room for the items, add all
                    if (slots[x, y].amount + amount <= maxAmount)
                    {
                        slots[x, y].amount += amount;
                        return true;
                    }
                    //if slot has room for only some of the items, add capacity and search again
                    else
                    {
                        amount -= maxAmount - slots[x, y].amount;
                        slots[x, y].amount = maxAmount;
                    }
                }

                //if slot is empty, put on a list as to optimize the code searching for empty slots
                if (slots[x, y].amount == 0 && emptySlot.Count != Mathf.CeilToInt(amount / maxAmount)) emptySlot.Add(new Vector2Int(x, y));
            }
        }

        //makes sure the next code is only run if the storage doesn't hold any of the item being added
        if (amount <= 0) return true;

        //scan empty slots, fill them
        switch (emptySlot.Count)
        {
            case 0:
                //this code is only run when the storage is out of space for items being added
                return false;
            case 1:
                slots[emptySlot[0].x, emptySlot[0].y].item = type;
                slots[emptySlot[0].x, emptySlot[0].y].amount = amount;
                return true;
            default:
                int ES = emptySlot.Count;

                for (int i = 0; i < ES - 1; i++)
                {
                    slots[emptySlot[i].x, emptySlot[i].y].item = type;
                    slots[emptySlot[i].x, emptySlot[i].y].amount = maxAmount;
                    amount -= maxAmount;
                }

                slots[emptySlot[ES].x, emptySlot[ES].y].item = type;
                slots[emptySlot[ES].x, emptySlot[ES].y].amount = amount;
                return true;
        }
    }

    public bool RemoveItems(int type, int amount)
    {
        //just in case there are no items to remove, why waste the memory :)
        if (amount <= 0) return false;

        //creats hypathetical slots as a safeguard from bugs
        SlotData[,] _slots = slots;

        //scan slots
        for (int y = 0; y < storageSize.y; y++)
        {
            for (int x = 0; x < storageSize.x; x++)
            {
                //if the slot's item is the same as being removed, begin subtracting
                if (_slots[x, y].item == type && _slots[x, y].amount > 0)
                {
                    //if slot has enough to handle removal, subtract all
                    if (_slots[x, y].amount - amount >= 0)
                    {
                        _slots[x, y].amount -= amount;
                        if(_slots[x, y].amount == 0) _slots[x, y].item = 0;

                        slots = _slots;
                        return true;
                    }
                    //if slot doesn't have enough to handle removal, subtract some and search again
                    else
                    {
                        amount -= _slots[x, y].amount;
                        _slots[x, y].item = 0;
                        _slots[x, y].amount = 0;
                    }
                }
            }
        }

        //this code is only run when the storage runs out of the item being removed
        return false;
    }
    public bool RemoveItems(Vector2Int slot, int amount)
    {
        //check for potential bugs and memory wastes
        if (amount <= 0 || (storageSize.x > slot.x && storageSize.y > slot.y)) return false;

        //if slot has enough to remove the requested amount, subtract it
        if(slots[slot.x, slot.y].amount <= amount)
        {
            slots[slot.x, slot.y].amount -= amount;
            if (slots[slot.x, slot.y].amount == 0) slots[slot.x, slot.y].item = 0;
            return true;
        }

        //this code is only run when the amount being removed is > currently contained
        return false;
    }
}

#region Storage Data

[System.Serializable]
public struct ItemData
{
    public string name;
    public int maxAmount;
    public int stackable;
}

public struct SlotData
{
    public int item;
    public int amount;
}

#endregion