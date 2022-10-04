using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GreenChillyPeper : Collectible
{
    public override void PickupCollectible()
    {
        Inventory.Instance.UpdateTireRate();
        GameManager.Instance.countGreenChillyPeperEat++;
    }
}
