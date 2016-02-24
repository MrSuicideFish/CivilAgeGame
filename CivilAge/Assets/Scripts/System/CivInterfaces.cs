using UnityEngine;
using System;
using System.Collections;

public interface ISelectable
{
    void Select( );
    void Deselect( );

    event EventHandler<EventArgs> ActorSelected;
    event EventHandler<EventArgs> ActorDeselected;

    void OnActorSelected( );
    void OnActorDeselected( );
}