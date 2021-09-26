using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class CreditItem : MonoBehaviour, IPointerDownHandler
{
    public bool isOpen;
    public Image rock;
    public GameObject[] items;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isOpen)
        {
            rock.DOFade(0, 1).From(1);
            rock.GetComponent<RectTransform>().DOAnchorPosY(100,1).SetRecyclable(true);
            foreach (var item in items)
            {
                item.gameObject.SetActive(true);
            }
            isOpen = true;
        }
    }
}
