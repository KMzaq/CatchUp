using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : Item
{
    SpriteRenderer m_spriteRenderer;
    void Start()
    {
        Init();
    }
    bool once = false;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player" && !once)
        {
            once = true;
            if (Inventory.Instance.GetItem(this._ItemData))
            {
                Destroy(this.gameObject);
            }

        }
    }

}
