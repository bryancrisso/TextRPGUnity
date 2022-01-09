using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BattleBar
{
    public class LootDisplay : MonoBehaviour
    {
        public Image foodIcon;
        public Image weaponIcon;

        public void DisplayLoot(Sprite _foodSprite)
        {
            foodIcon.sprite = _foodSprite;
            weaponIcon.sprite = null;

        }
        public void DisplayLoot(Sprite _foodSprite, Sprite _weaponSprite)
        {
            foodIcon.sprite = _foodSprite;
            weaponIcon.sprite = _weaponSprite;
        }
    }
}
