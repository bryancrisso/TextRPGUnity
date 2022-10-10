using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TopDown
{
    public class InventoryStreamObject : MonoBehaviour
    {
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI amount;
        public Image icon;
        public GameObject child;

        [SerializeField]
        private float liveTimer = 2f;
        private float currentLiveTimer;

        public float moveTime = 1f;
        private float currentMoveTime = 0f;
        private float normalisedVal;
        private float width;
        private Vector2 startPos;

        private void Start()
        {
            currentLiveTimer = liveTimer;
            width = child.GetComponent<RectTransform>().rect.width;
            startPos = child.GetComponent<RectTransform>().anchoredPosition;
            StartCoroutine("ScrollIn");
        }

        IEnumerator ScrollIn()
        {
            while (currentMoveTime <= moveTime)
            {
                currentMoveTime += Time.deltaTime;
                normalisedVal = currentMoveTime / moveTime;
                child.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(new Vector2(startPos.x + width, startPos.y), startPos, normalisedVal);
                yield return null;
            }
            currentMoveTime = 0f;
        }

        IEnumerator ScrollOut()
        {
            while (currentMoveTime <= moveTime)
            {
                currentMoveTime += Time.deltaTime;
                normalisedVal = currentMoveTime / moveTime;
                child.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startPos, new Vector2(startPos.x + width, startPos.y), normalisedVal);
                yield return null;
            }
            currentMoveTime = 0f;
            Destroy(gameObject);
        }

        public bool countDown()
        {
            if (currentLiveTimer > 0)
            {
                currentLiveTimer -= Time.deltaTime;
                return false;
            }
            else
            {
                StartCoroutine("ScrollOut");
                return true;
            }
        }
    }

}