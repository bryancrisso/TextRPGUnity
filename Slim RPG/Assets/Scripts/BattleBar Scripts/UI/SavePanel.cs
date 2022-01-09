using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace BattleBar
{
    public class SavePanel : MonoBehaviour
    {
        public int index;
        public TextMeshProUGUI saveNameText;
        public SaveManager saveManager;

        private void Start()
        {
            saveManager = GameObject.FindGameObjectWithTag("TextureManager").GetComponent<SaveManager>();
        }

        public void On_Create_Button_Click()
        {
            //here
        }
    }
}