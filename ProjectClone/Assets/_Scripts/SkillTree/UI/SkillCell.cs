using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SkillCell : MonoBehaviour
{
    public bool isUnlocked, canUnlock;
    public SkillData skillData;
    public List<SkillCell> adjCell;
    public Image icon;

    private void Awake()
    {
        GetComponent<Button>().OnClickAsObservable()
            .Where(_ => canUnlock)
            .Subscribe(_ =>
        {
            foreach (var cell in adjCell)
            {
                cell.gameObject.SetActive(true);


                if (!cell.isUnlocked && !cell.canUnlock)
                {
                    cell.GetComponent<Image>().DOColor(Color.gray,0.5f).From(new Color(0.5f, 0.5f, 0.5f, 0));
                    cell.gameObject.transform.DOScale(0.75f, 0.5f).From(0);
                }
                else
                {
                    cell.GetComponent<Image>().color = Color.white;
                }

                cell.GetComponent<Image>().color = cell.isUnlocked ? Color.white : Color.gray;
                cell.canUnlock = true;
            }
            isUnlocked = true;
            transform.DOScale(1, 0.75f);
            GetComponent<Image>().DOColor(Color.white,0.75f);
        }).AddTo(gameObject);

        icon= gameObject.transform.GetChild(0).GetComponent<Image>();
        icon.sprite = skillData.skillIcon;
    }

    private void Start()
    {
    }
}
