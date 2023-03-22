using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    public static event GameManager.Item_event AddItem;

    [SerializeField] private int _price;
    private Collider _myCollider;
    private Transform _myTransform;
    private Animator _anim;
    private Vector3 _startScale;

    public Collider MyCollider { get => _myCollider; }
    public Transform MyTransform { get => _myTransform; }
    public int Price { get => _price; }

    private void Start()
    {
        _myTransform = GetComponent<Transform>();
        _anim = GetComponent<Animator>();
        _myCollider = GetComponent<Collider>();
        _startScale = _myTransform.localScale;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AddItem?.Invoke(this);
        }
    }

    public void Move(bool direction, Transform parent, Vector3 slotPos = new Vector3(), Vector3 scale = new Vector3())
    {
        if (direction)
        {
            _myCollider.enabled = false;
            _myTransform.parent = parent;

            Vector3 point1 = MyTransform.localPosition;
            point1.y += 1;
            Vector3[] path = new Vector3[] { point1, Vector3.up, slotPos };
            MyTransform.DOScale(scale, 1f);
            MyTransform.DOLocalPath(path, 1f, PathType.CatmullRom, PathMode.Full3D);
            MyTransform.DOLocalRotate(Vector3.zero, 1f, RotateMode.Fast);
        }
        else
        {
            _myTransform.parent = parent;
            Sequence sc = DOTween.Sequence()
                .Append(_myTransform.DOMove(parent.position, 2f))
                .Insert(0,_myTransform.DOScale(_startScale, 1f))
                .OnComplete(Destroy);
        }        
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void AnimOff()
    {
        _anim.enabled = false;
        _myCollider.enabled = true;
    }
}
