using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bag : MonoBehaviour
{
    public static event GameManager.Int_event ChangeUI;
    public static event GameManager.InIntArry_event AddGold;

    [SerializeField] int _maxSlotCount;
    private Transform _bagTransform;
    private Item[] _items;
    private Vector3 _itemScale;
    private Vector3[] _itemPos; 
    private int _slotIndex = 0;
    private float _zPos = 0.07f;
    private float _yPos = 0.05f;
    private float _yOffset = 0.08f;
    private bool _removing = false;

    private void OnDestroy()
    {
        Item.AddItem -= AddItem;
        Barn.TradeItems -= RemoveItem;
    }

    public void Inicializing()
    {        
        Item.AddItem += AddItem;
        Barn.TradeItems += RemoveItem;

        _bagTransform = GameObject.Find("Bag").transform;
        _itemPos = new Vector3[_maxSlotCount];
        _items = new Item[_maxSlotCount];
        _itemScale = new Vector3(0.07f, 0.07f, 0.14f);
        _slotIndex = 0;

        float z = _zPos;
        float y = _yPos;
        int index = -1;
        int yIndex = 0;

        for (int i = 0;i < _maxSlotCount;i++)
        {
            index++;
            switch (index)
            {
                case 0:
                    _itemPos[i] = new Vector3(-0.16f,y,z);
                    break;
                case 1:
                    _itemPos[i] = new Vector3(-0.08f, y,z);
                    break;
                case 2:
                    _itemPos[i] = new Vector3(0, y, z);
                    break;
                case 3:
                    _itemPos[i] = new Vector3(0.08f, y, z);
                    break;
                case 4:                    
                    _itemPos[i] = new Vector3(0.16f, y, z);
                    z *= -1f;
                    index = -1;
                    yIndex++;
                    if (yIndex == 2)
                    {
                        yIndex = 0;
                        y += _yOffset;
                    }                    
                    break;
            }
        }
    }

    private void AddItem(Item item)
    {
        if(_slotIndex < _maxSlotCount && !_removing)
        {
            item.Move(true,_bagTransform, _itemPos[_slotIndex], _itemScale);
            _items[_slotIndex] = item;
            _slotIndex++;
            ChangeUI?.Invoke(_slotIndex);
        }
    }
    
    public void RemoveItem(Transform parent)
    {
        if(_slotIndex > 0 && !_removing)
        {
            StartCoroutine(Cor_RemoveItem(parent));
        }        
    }

    private IEnumerator Cor_RemoveItem(Transform parent)
    {
        _removing = true;

        int goldCount = _slotIndex;
        int[] itemPrice = new int[_slotIndex];
        int sl = _slotIndex - 1;

        for(int i = sl; i > -1; i--)
        {
            _slotIndex--;            
            ChangeUI?.Invoke(_slotIndex);
            itemPrice[i] = _items[i].Price;
            _items[i].Move(false, parent);            
            yield return new WaitForSeconds(0.05f);

        }
        _removing = false;
        yield return new WaitForSeconds(0.5f);
        AddGold?.Invoke(goldCount, itemPrice);
        yield break;
    }
}
