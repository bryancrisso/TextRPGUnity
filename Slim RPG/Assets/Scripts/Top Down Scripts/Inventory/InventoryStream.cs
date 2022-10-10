using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    public class InventoryStream : MonoBehaviour
    {
        public GameObject streamPrefab;
        List<InventoryStreamObject> stream = new List<InventoryStreamObject>();

        public void addToStream(ItemObject item, int amount)
        {
            GameObject insObj = Instantiate(streamPrefab);
            insObj.transform.SetParent(transform, false);
            InventoryStreamObject insObjScr = insObj.GetComponent<InventoryStreamObject>();
            stream.Add(insObjScr);
            insObjScr.icon.sprite = item.displayImage;
            insObjScr.itemName.text = item.displayName;
            insObjScr.amount.text = "+" + amount.ToString();
        }

    private void Update()
        {
            for (int i = stream.Count - 1; i > -1; i--)
            {
                if (stream[i].countDown())
                {
                    stream.RemoveAt(i);
                }
            }
        }
    }
}
