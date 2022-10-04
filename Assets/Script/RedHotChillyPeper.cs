using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RedHotChillyPeper : Collectible
{
    
    public override void PickupCollectible()
    {
        Inventory.Instance.UpdateDamage();
        GameManager.Instance.countRedHotChillyPeperEat++;
    }


}
