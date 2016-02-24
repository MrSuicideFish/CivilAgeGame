using UnityEngine;
using System.Collections;
using System;

public class WorldActor : MonoBehaviour, ISelectable
{
    public SessionPlayer Owner { get; private set; }

    public virtual event EventHandler<EventArgs> ActorDeselected;
    public virtual event EventHandler<EventArgs> ActorSelected;

    public virtual void TransferOwnership( SessionPlayer newOwner )
    {
        Owner = newOwner;
    }

    public virtual void Deselect( )
    {
        SendMessage( "OnDeselect", SendMessageOptions.DontRequireReceiver );
    }

    public virtual void OnActorDeselected( )
    {
    }

    public virtual void OnActorSelected( )
    {
    }

    public virtual void Select( )
    {
        SendMessage( "OnSelect", SendMessageOptions.DontRequireReceiver );
    }
}
