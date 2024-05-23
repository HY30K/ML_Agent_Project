using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private CanvasGroup _buttonGroup;
    [SerializeField] private string _soundName;

    private void Start()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_titleText.rectTransform.DOLocalMoveY(100f, 1.5f).SetEase(Ease.OutBounce))
           .JoinCallback(() => SoundManager.PlaySound(_soundName))
           .Append(_buttonGroup.DOFade(1, 0.5f));
    }

    public void Menu()
    {
        SceneManager.LoadSceneAsync("Intro");
    }

    public void Restart()
    {
        SceneLoader.Instance.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
