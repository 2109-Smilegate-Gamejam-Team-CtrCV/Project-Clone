using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameSkillView : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public void Show()
    {
        if(!isActiveAndEnabled)
        {
            canvasGroup.DOFade(1, 1).From(0).OnStart(() => gameObject.SetActive(true));
        }
    }

    public void HIde()
    {
        var expModel = SkillManager.Instance.expModel;
        if (!expModel.CanGetSkill)
        {
            canvasGroup.DOFade(0, 1).OnComplete(() => gameObject.SetActive(false));
        }
    }
}
