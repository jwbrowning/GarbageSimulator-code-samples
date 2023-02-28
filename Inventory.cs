using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton class representing the player's inventory
public class Inventory
{
    private static Inventory instance;
    public static Inventory Instance
    {
        get
        {
            if (instance == null) instance = new Inventory();
            return instance;
        }
    }

    const int size = 24;

    private Organ[] inventoryItems = new Organ[size];
    public int ammoCount = 0;
    public Brain currentBrain;
    public Eyes currentEyes;
    public Lungs currentLungs;
    public Heart currentHeart;
    private float cash = 0;
    public float Cash
    {
        get
        {
            return cash;
        }
    }

    private Inventory()
    {
        currentBrain = new Brain { Quality = 1 - Random.value * .1f };
        currentEyes = new Eyes { Quality = 1 - Random.value * .1f };
        currentHeart = new Heart { Quality = 1 - Random.value * .1f };
        currentLungs = new Lungs { Quality = 1 - Random.value * .1f };
    }

    public void Recreate()
    {
        instance = new Inventory();
    }

    public bool AddItem(Item item)
    {
        bool success = false;

        if (currentBrain == null && item is Brain brain)
        {
            currentBrain = brain;
            success = true;
        }
        else if (currentEyes == null && item is Eyes eyes)
        {
            currentEyes = eyes;
            success = true;
        }
        else if (currentLungs == null && item is Lungs lungs)
        {
            currentLungs = lungs;
            success = true;
        }
        else if (currentHeart == null && item is Heart heart)
        {
            currentHeart = heart;
            success = true;
        }
        else if (item is Organ organ)
        {
            for (int i = 0; i < size; i++)
            {
                if (inventoryItems[i] == null)
                {
                    inventoryItems[i] = organ;
                    success = true;
                    break;
                }
            }
        }
        else if (item is Ammo)
        {
            ammoCount += 5; // idk
        }

        return success;
    }

    public void RemoveItem(int slot)
    {
        inventoryItems[slot] = null;
    }

    public void SellItem(int slot)
    {
        if (inventoryItems[slot] == null) return;
        cash += inventoryItems[slot].Value;
        inventoryItems[slot] = null;
    }

    public Organ GetItem(int slot)
    {
        return inventoryItems[slot];
    }

    public int ItemCount()
    {
        int count = 0;
        foreach (Organ o in inventoryItems)
        {
            if (o != null) count++;
        }
        return count;
    }

    public void MoveItem(int originalSlot, int destinationSlot)
    {
        Organ temp = inventoryItems[destinationSlot];
        inventoryItems[destinationSlot] = inventoryItems[originalSlot];
        inventoryItems[originalSlot] = temp;
    }

    public void EquipBrain(int slot)
    {
        if (inventoryItems[slot] is Brain brain)
        {
            if (currentBrain == null) currentBrain = brain;
            else currentBrain.Quality += inventoryItems[slot].Quality;
            inventoryItems[slot] = null;
        }
        else if (inventoryItems[slot] == null)
        {
            inventoryItems[slot] = currentBrain;
            currentBrain = null;
        }
    }

    public void EquipEyes(int slot)
    {
        if (inventoryItems[slot] is Eyes eyes)
        {
            if (currentEyes == null) currentEyes = eyes;
            else currentEyes.Quality += inventoryItems[slot].Quality;
            inventoryItems[slot] = null;
        }
        else if (inventoryItems[slot] == null)
        {
            inventoryItems[slot] = currentEyes;
            currentEyes = null;
        }
    }

    public void EquipLungs(int slot)
    {
        if (inventoryItems[slot] is Lungs lungs)
        {
            if (currentLungs == null) currentLungs = lungs;
            else currentLungs.Quality += inventoryItems[slot].Quality;
            inventoryItems[slot] = null;
        }
        else if (inventoryItems[slot] == null)
        {
            inventoryItems[slot] = currentLungs;
            currentLungs = null;
        }
    }

    public void EquipHeart(int slot)
    {
        if (inventoryItems[slot] is Heart heart)
        {
            if (currentHeart == null) currentHeart = heart;
            else currentHeart.Quality += inventoryItems[slot].Quality;
            inventoryItems[slot] = null;
        }
        else if (inventoryItems[slot] == null)
        {
            inventoryItems[slot] = currentHeart;
            currentHeart = null;
        }
    }

    public void UpdateOrgans()
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i]?.Update(.5f);
            if (inventoryItems[i] != null && inventoryItems[i].Quality <= 0) inventoryItems[i] = null;
        }
        currentBrain?.Update();
        if (currentBrain != null && currentBrain.Quality <= 0) currentBrain = null;
        currentEyes?.Update();
        if (currentEyes != null && currentEyes.Quality <= 0) currentEyes = null;
        currentLungs?.Update();
        if (currentLungs != null && currentLungs.Quality <= 0) currentLungs = null;
        currentHeart?.Update();
        if (currentHeart != null && currentHeart.Quality <= 0) currentHeart = null;
    }

    public List<Organ> getCurrentOrgans()
    {
        List<Organ> organs = new List<Organ>();
        if (currentBrain != null) organs.Add(currentBrain);
        if (currentEyes != null) organs.Add(currentEyes);
        if (currentLungs != null) organs.Add(currentLungs);
        if (currentHeart != null) organs.Add(currentHeart);
        return organs;
    }

}
