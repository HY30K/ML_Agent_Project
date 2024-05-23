using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    [SerializeField] TextMeshProUGUI warningText;
    [HideInInspector] public int Score = 0;
    private float count = 3;

    public static GameUIController Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameStart();
    }

    private void Update()
    {
        if (GameManager.Instance.GameStart)
        {
            text.text = Score.ToString();

            if (Score > 0 && Score % 10 == 0 && !warningTriggered)
            {
                Warning();
                warningTriggered = true; // 경고가 트리거되면 플래그를 true로 설정
            }
            else if (Score % 10 != 0)
            {
                warningTriggered = false; // 점수가 10의 배수가 아니면 플래그를 다시 false로 설정
            }
        }
    }

    private void GameStart()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(text.transform.DOLocalMoveY(-0.3f, 1.5f))
            .OnComplete(() => StartCoroutine(CountDownCoroutine()));
    }

    private void Warning()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(warningText.rectTransform.DOLocalMoveY(400, 1f).SetEase(Ease.OutExpo))
            .JoinCallback(() => SoundManager.PlaySound("Siren"))
            .AppendCallback(() => StartCoroutine(BlinkText()))
            .OnComplete(() => GameManager.Instance.Speed += 0.5f);
    }

    private void TextStart()
    {
        GameManager.Instance.GameStart = true;
    }

    private IEnumerator BlinkText()
    {
        int cnt = 3;
        while (cnt > 0)
        {
            warningText.DOFade(0, 0.5f);
            yield return new WaitForSeconds(0.5f);
            warningText.DOFade(1, 0.5f);
            yield return new WaitForSeconds(0.5f);
            cnt--;
        }
        warningText.rectTransform.DOLocalMoveY(800, 1.5f);
    }

    private IEnumerator CountDownCoroutine()
    {
        SoundManager.PlaySound("CountDown");
        while (count > -1)
        {
            Debug.Log("123");
            if (count == 0)
                text.text = "START";
            else
                text.text = count.ToString();

            yield return new WaitForSeconds(1);
            count--;
        }
        TextStart();
    }

    private bool warningTriggered = false; // 경고 트리거 상태를 저장하는 플래그
}
