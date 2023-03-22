using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image _joystickBg;
    [SerializeField] private Image _joystick;
    private RectTransform _rectTransformBG;
    private RectTransform _rectTransform;
    private Vector2 _tuchPos;
    
    private void Start()
    {        
        _rectTransformBG = _joystickBg.GetComponent<RectTransform>();
        _rectTransform = _joystick.GetComponent<RectTransform>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _rectTransformBG.position = eventData.position;
        _joystickBg.gameObject.SetActive(true);
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = Vector2.zero;
        _joystickBg.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {        
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransformBG, eventData.position, eventData.pressEventCamera, out _tuchPos))
        {
             _tuchPos = _tuchPos / _rectTransformBG.sizeDelta * 3;
             _tuchPos = (_tuchPos.magnitude > 1.0f) ? _tuchPos.normalized : _tuchPos;
             _rectTransform.anchoredPosition = _tuchPos * (_rectTransformBG.sizeDelta / 3);
        }
    }    

    public float direction()
    {
        return Quaternion.FromToRotation(Vector3.up, _rectTransform.position - _rectTransformBG.position).eulerAngles.z * -1f;
    }
}
