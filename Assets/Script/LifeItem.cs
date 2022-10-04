using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeItem : Collectible
{
    public override void PickupCollectible()
    {
        if (GameManager.Instance.PlayerLife < 5)
        {
            GameManager.Instance.PlayerLife += 1;
        }
    }
}
