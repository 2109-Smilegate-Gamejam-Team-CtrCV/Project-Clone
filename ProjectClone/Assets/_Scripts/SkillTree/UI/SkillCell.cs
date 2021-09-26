using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class SkillCell : MonoBehaviour
{
    public bool isUnlocked, canUnlock;
    public SkillData skillData;
    public List<SkillCell> adjCell;
    
    private Image _icon;

    private void Start()
    {
        var button = GetComponent<Button>(); 
        
        button.OnClickAsObservable()
            .Where(_ => canUnlock && !isUnlocked)
            .Subscribe(_ =>
        {
            SkillManager.Instance.GetSkill(skillData, out var canGetSkill);
            if (!canGetSkill) return;
            
            foreach (var cell in adjCell)
            {
                cell.gameObject.SetActive(true);
                cell.canUnlock = true;
            }
            isUnlocked = true;

            SoundManager.Instance.PlayFXSound("Skill");
            transform.DOScale(1, 0.75f);
            GetComponent<Image>().DOColor(Color.white,0.75f);
        }).AddTo(gameObject);

        button.OnPointerEnterAsObservable()
            .Select(pointerEventData => pointerEventData.position)
            .Subscribe(_ =>
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                SkillManager.Instance.ShowTooltip(skillData).position = mousePos;
            }).AddTo(gameObject);

        button.OnPointerExitAsObservable()
            .Subscribe(_ => SkillManager.Instance.ShowTooltip(skillData).gameObject.SetActive(false))
            .AddTo(gameObject);

        _icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        _icon.sprite = skillData.skillIcon;
    }

    private void OnEnable()
    {
        if (!isUnlocked && isActiveAndEnabled)
        {
            GetComponent<Image>().DOColor(Color.gray,0.5f).From(new Color(0.5f, 0.5f, 0.5f, 0));
            transform.DOScale(0.75f, 0.5f).From(0);
        }
        else
        {
            GetComponent<Image>().color = Color.white;
            transform.localScale = Vector3.one;
        }

        //GetComponent<Image>().color = isUnlocked ? Color.white : Color.gray;
    }
}
