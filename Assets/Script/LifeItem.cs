using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeItem : Collectible
{
    public override void PickupCollectible()
    {
        if (PlayerManager.Instance.PlayerLife < 5)
        {
            PlayerManager.Instance.PlayerLife += 1;
        }
    }
}
