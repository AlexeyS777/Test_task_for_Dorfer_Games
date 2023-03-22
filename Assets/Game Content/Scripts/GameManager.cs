using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public delegate void Item_event(Item value);
    public delegate void Transform_event(Transform value);
    public delegate void Int_event(int count);
    public delegate void InIntArry_event(int count,int[] price);

    [SerializeField] private TextMeshProUGUI _block_txt;
    [SerializeField] private TextMeshProUGUI _gold_txt;
    [SerializeField] private Gold _gold;
    private int _goldCount = 0;
    private Animator _anim;
 


    private void Awake()
    {
        Bag.ChangeUI += ShowBlock;
        Bag.AddGold += AddGold;
        Gold.EShowGold += ShowGold;
    }

    private void OnDestroy()
    {
        Bag.ChangeUI -= ShowBlock;
        Bag.AddGold -= AddGold;
        Gold.EShowGold -= ShowGold;
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void ShowBlock(int value)
    {
        _block_txt.text = $"BLOCK:{value}/40"; 
    }

    private void ShowGold(int value)
    {
        _goldCount += value;
        _gold_txt.text = _goldCount.ToString();
        _anim.Play("Gold counter");
    }

    private void AddGold(int count, int[] price)
    {
        StartCoroutine(Cor_AddGold(count, price));
    }

    private IEnumerator Cor_AddGold(int count, int[]price)
    {        
        for(int i = 0; i < count; i++)
        {            
            Gold g = Instantiate(_gold, transform);
            g.Count = price[i];
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
