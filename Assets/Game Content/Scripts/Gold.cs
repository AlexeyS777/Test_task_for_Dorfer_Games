using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gold : MonoBehaviour
{
    public static event GameManager.Int_event EShowGold;
     
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 finPos;
    [SerializeField] private Vector2 finScale;

    private RectTransform myTransform;
    private int _count = 0;
    
    public int Count { get => _count; set {_count = value; } }

    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        myTransform.anchoredPosition = startPos;
        Move();
    }

    public void Move()
    {
        Sequence sc = DOTween.Sequence()
                .Append(myTransform.DOAnchorPos(finPos, 1f))
                .Insert(0, myTransform.DOSizeDelta(finScale, 1f))
                .OnComplete(ShowGold);       
    }

    private void ShowGold()
    {
        EShowGold?.Invoke(Count);
        Destroy(this.gameObject);
    }
}
