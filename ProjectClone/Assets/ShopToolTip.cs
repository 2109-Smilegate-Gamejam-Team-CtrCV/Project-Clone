using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopToolTip : MonoBehaviour
{
    public RectTransform rect;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;


    void Update()
    {
        rect.anchoredPosition = Input.mousePosition ;
    }
}
