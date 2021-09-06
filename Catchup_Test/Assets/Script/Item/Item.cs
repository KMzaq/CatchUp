using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData _ItemData;

    protected SpriteRenderer _SpriteRenderer;
    public void Init()
    {
        _SpriteRenderer = Util.GetorAddComponent<SpriteRenderer>(this.gameObject);
        _SpriteRenderer.sprite = _ItemData._Image;
    }
}
