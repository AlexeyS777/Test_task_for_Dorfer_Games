using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat : MonoBehaviour
{
    [SerializeField] private GameObject _wheat;
    [SerializeField] private GameObject _wheatCuted;
    [SerializeField] private Item _wheatBlock;    
    [SerializeField] private ParticleSystem _grassCrash;
    private WheatBed _myBed;
    private BoxCollider _myCollider;
    private Transform _myTransform;
    public Vector3 Scale { get { return _myTransform.localScale; } set { _myTransform.localScale = value; } }


    private void Start()
    {
        _myCollider = GetComponent<BoxCollider>();
        _myTransform = GetComponent<Transform>();
        _myBed = transform.parent.GetComponent<WheatBed>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sickle")
        {
            _myCollider.enabled = false;
            _wheat.SetActive(false);
            _wheatCuted.SetActive(true);
            Instantiate(_grassCrash, _myTransform.position,Quaternion.Euler(Vector3.zero), _myTransform);
            _myBed.ChangeCount();            
            Vector3 pos = transform.position;
            pos.y = 0.3f;
            Instantiate(_wheatBlock, pos, _myTransform.rotation);
        }
    }
    
    public void Growing()
    {
        _myTransform.localScale = Vector3.zero;
        _wheatCuted.SetActive(false);
        _wheat.SetActive(true);
    }

    public void EndGrowing()
    {
        _myCollider.enabled = true;
    }
}
