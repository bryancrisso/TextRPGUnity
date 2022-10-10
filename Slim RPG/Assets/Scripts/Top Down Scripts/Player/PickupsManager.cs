using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class PickupsManager : MonoBehaviour
    {
        private AudioSource audioPlayer;
        public AudioClip pickupAudio;

        // Start is called before the first frame update
        void Start()
        {
            audioPlayer = GetComponent<AudioSource>();
        }

        public void Pickup(Pickup pickup)
        {
            InventoryManager.instance.AddItem(pickup.item, pickup.amount);
            audioPlayer.PlayOneShot(pickupAudio);
        }
    }
}