using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePresenter : MonoBehaviour
{
    [SerializeField]
    private TitleMainView mainView;
    [SerializeField]
    private TitleOptionView optionView;
    [SerializeField]
    private GameObject creditView;


    private void Awake()
    {
        SoundManager.Instance.AdjustMasterVolume(OptionManager.Instance.Master);
        SoundManager.Instance.AdjustBGMVolume(OptionManager.Instance.BGM);
        SoundManager.Instance.AdjustFxVoulme(OptionManager.Instance.VFX);
        SoundManager.Instance.PlayBGMSound("BGM");
        mainView.StartClick.Subscribe(_ => SceneManager.LoadScene(1));
        mainView.CreditClick.Subscribe(_ => creditView.SetActive(true));
        mainView.OptionClick.Subscribe(_ => optionView.Show());
        mainView.ExitClick.Subscribe(_ => Application.Quit());


        optionView.CancelClick.Subscribe(p => optionView.Hide());
        optionView.MasterVolumeValueChanged.Subscribe(p => OptionManager.Instance.Master = p);
        optionView.BGMVolumeValueChanged.Subscribe(p => OptionManager.Instance.BGM = p);
        optionView.VFXVolumeValueChanged.Subscribe(p => OptionManager.Instance.VFX = p);

    }
}
