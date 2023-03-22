using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatBed : MonoBehaviour
{
    [SerializeField] private Wheat[] _wheat;    

    private int _wheatCount = 0;

    private void Start()
    {
        _wheatCount = _wheat.Length;
    }

    public void ChangeCount()
    {
        _wheatCount--;
        if(_wheatCount == 0)
        {
            StartCoroutine(Growing());
        }
    } 
    
    private IEnumerator Growing()
    {
        float timer = 0;

        foreach (Wheat wh in _wheat)
        {
            wh.Growing();
        }

        while (timer < 10)
        {
            timer += Time.deltaTime;

            foreach (Wheat wh in _wheat)
            {                
                wh.Scale = Vector3.one * timer / 10;
                if (timer >= 10)
                {
                    wh.Scale = Vector3.one;
                }                 
            }

            yield return null;
        }

        _wheatCount = _wheat.Length;

        foreach (Wheat wh in _wheat)
        {
            wh.EndGrowing();
        }

        yield break;
    }
}
