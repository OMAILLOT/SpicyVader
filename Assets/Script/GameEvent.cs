using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : MonoSingleton<GameEvent>
{
    public virtual void ActiveEvent()
    {

    }

    public virtual void winEvent()
    {

    }
}
