using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RedHotChillyPeper : Collectible
{
    
    public override void PickupCollectible()
    {
        Inventory.instance.UpdateDamage();
        GameManager.Instance.countRedHotChillyPeperEat++;
    }


}
