using UnityEngine;
using System.Collections;
using System;

public class WorldActor : MonoBehaviour, ISelectable
{
    public SessionPlayer Owner { get; private set; }

    public virtual event EventHandler<EventArgs> ActorDeselected;
    public virtual event EventHandler<EventArgs> ActorSelected;

    public bool IsSelected { get; private set; }

    public virtual void TransferOwnership( SessionPlayer newOwner )
    {
        Owner = newOwner;
    }

    public virtual void OnActorSelected( ) { }

    public virtual void OnActorDeselected( ) { }

    public virtual void Select( )
    {
        IsSelected = true;

        //Enable selection highlight
        GetComponent<MeshRenderer>( ).material.SetFloat( "_OutlineWidth", 0.2f );
        GetComponent<MeshRenderer>( ).material.SetColor( "_HighlightColor", SessionGameManager.SelectedActorHightlightColor );

        SendMessage( "OnSelect", SendMessageOptions.DontRequireReceiver );
    }

    public virtual void Deselect( )
    {
        IsSelected = false;

        //Disable selection highlight
        GetComponent<MeshRenderer>( ).material.SetFloat( "_OutlineWidth", 0.0f );

        SendMessage( "OnDeselect", SendMessageOptions.DontRequireReceiver );
    }
}
