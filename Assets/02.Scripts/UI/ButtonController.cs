using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [SerializeField] private float alpha;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color _color;

    private Button _button;
    public UnityEvent OnClick;

    private void Start()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(ButtonClickHandle);
    }

    private void ButtonClickHandle()
    {
        Sequence seq = DOTween.Sequence();

        SoundManager.PlaySound("MouseClick");

        seq.Append(transform.DOScale(Vector3.one * 0.9f, 0.2f)).SetEase(Ease.OutQuad);
        seq.Append(transform.DOScale(Vector3.one * 1, 0.2f)).SetEase(Ease.OutQuad);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_button.interactable)
            OnClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.OutQuad);

        SoundManager.PlaySound("MouseHover");

        _text.color = _color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutQuad);

        Color color = Color.black;
        _text.color = color;
    }
}
