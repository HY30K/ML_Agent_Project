using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("MainUI")]
    [SerializeField] private TextMeshProUGUI[] TitleTexts;
    [SerializeField] private CanvasGroup ButtonCanvas;

    [Header("SettingUI")]
    [SerializeField] private CanvasGroup SettingCanvas;
    [SerializeField] private Image SettingPanel;
    [SerializeField] private TextMeshProUGUI SettingTitle;


    private void Start()
    {
        OpenMainUI();
    }

    private void OpenMainUI()
    {
        Sequence startSeq = DOTween.Sequence();
        startSeq.Append(TitleTexts[0].rectTransform.DOLocalMoveX(200, 0.3f))
            .Append(TitleTexts[1].rectTransform.DOLocalMoveX(200, 0.3f))
            .Append(TitleTexts[2].rectTransform.DOLocalMoveX(200, 0.3f))
            .JoinCallback(() => SoundManager.PlaySound("TitleText"))
            .Append(ButtonCanvas.DOFade(1, 0.5f));

        ButtonCanvas.blocksRaycasts = true;
    }

    private void CloseMainUI()
    {
        Sequence startSeq = DOTween.Sequence();
        startSeq.Append(TitleTexts[0].rectTransform.DOLocalMoveX(2500, 0.5f))
            .Join(TitleTexts[1].rectTransform.DOLocalMoveX(2500, 0.5f))
            .Join(TitleTexts[2].rectTransform.DOLocalMoveX(2500, 0.5f))
            .Join(ButtonCanvas.DOFade(0, 0.5f));

        ButtonCanvas.blocksRaycasts = false;
    }

    public void OpenSetting()
    {
        Sequence settingSeq = DOTween.Sequence();
        settingSeq.Append(SettingCanvas.DOFade(1, 0.5f))
            .Append(SettingTitle.rectTransform.DOLocalMoveY(300, 1f))
            .Join(SettingPanel.rectTransform.DOLocalMoveY(-100, 1f));

        CloseMainUI();

        SettingCanvas.blocksRaycasts = true;
    }

    public void CloseSetting()
    {
        Sequence settingSeq = DOTween.Sequence();
        settingSeq.Append(SettingTitle.rectTransform.DOLocalMoveY(600, 0.3f))
            .Join(SettingPanel.rectTransform.DOLocalMoveY(1000, 0.3f))
            .Join(SettingCanvas.DOFade(0, 0.5f))
            .OnComplete(OpenMainUI);

        SettingCanvas.blocksRaycasts = false;
    }

    public void StartGame()
    {
        SceneLoader.Instance.LoadScene("Game");
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void MasterChange(Slider slider)
    {
        SoundManager.SettingMaster(slider.value);
    }

    public void BgmSetting(Slider slider)
    {
        SoundManager.SettingBgm(slider.value);
    }

    public void SfxSetting(Slider slider)
    {
        SoundManager.SettingSfx(slider.value);
    }
}
