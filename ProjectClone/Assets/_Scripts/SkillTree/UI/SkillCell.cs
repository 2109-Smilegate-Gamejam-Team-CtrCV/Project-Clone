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

    private void Awake()
    {
        GetComponent<Button>().OnClickAsObservable()
            .Where(_ => canUnlock)
            .Subscribe(_ =>
        {
            foreach (var cell in adjCell)
            {
                cell.canUnlock = true;
                cell.gameObject.SetActive(true);
                cell.GetComponent<Image>().color = cell.isUnlocked ?  Color.white : Color.gray;
            }
            isUnlocked = true;
            GetComponent<Image>().color = Color.white;
        }).AddTo(gameObject);
    }
}
