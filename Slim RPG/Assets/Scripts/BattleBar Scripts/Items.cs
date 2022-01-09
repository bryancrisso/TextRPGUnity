using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Path = System.IO.Path;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
namespace BattleBar
{
    public enum itemType
    {
        Food,
        Weapon
    }

    public class Items : MonoBehaviour
    {
    
        public List<Item> itemList = new List<Item>();
        public TextMeshProUGUI text;
    
        private void Start()
        {
            if (!Directory.GetFiles(".").Contains(".\\items.json"))
            {
            
                File.Create(".\\items.json").Dispose();
            }
        
            string json = System.IO.File.ReadAllText(".\\items.json");
            itemList = JsonConvert.DeserializeObject<List<Item>>(json);

            string jsonData = JsonConvert.SerializeObject(itemList).ToString();
            File.WriteAllText(".\\compiled_items.json", jsonData);
        }
    }

    public class Item
    {
        //Global variables
        public string name;

        public itemType type = itemType.Food;
        public int cost = 0;
        public bool isStackable = false;
        public int amount;
        public int level = 0;
        public int quality = 0;
        public int id;

        //Food-specific variables
        public int healthRegen;

        //Weapon-specific variables
        public int[] damage = { 0, 0 };
        public int maxDurability = 0;
        public int currentDurability = 0;

        public void init(Item correspondingItem)
        {
            name = correspondingItem.name;
            type = correspondingItem.type;
            cost = correspondingItem.cost;
            isStackable = correspondingItem.isStackable;
            amount = correspondingItem.amount;
            level = correspondingItem.level;
            quality = correspondingItem.quality;
            id = correspondingItem.id;

            healthRegen = correspondingItem.healthRegen;

            damage = correspondingItem.damage;
            maxDurability = correspondingItem.maxDurability;

            if (type == itemType.Food)
            {
                amount = 1;
            }
            else
            {
                currentDurability = maxDurability;
            }
        }
    }
}
