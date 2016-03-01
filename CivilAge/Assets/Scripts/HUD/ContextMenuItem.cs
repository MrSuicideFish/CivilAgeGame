using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ContextMenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum ContextMenuItemType
    {
        TARGET,
        COMMAND
    }
    public ContextMenuItemType ItemType = ContextMenuItemType.COMMAND;

    public bool IsHovering { get; private set; }
    public Delegate TargetCommand;
    public ContextMenuTarget[] Targets;

    public void OnPointerEnter( PointerEventData eventData )
    {
        IsHovering = true;
    }

    public void OnPointerExit( PointerEventData eventData )
    {
        IsHovering = false;
    }

    public void OnPointerClick( PointerEventData eventData )
    {
        switch ( ItemType )
        {
            case ContextMenuItemType.COMMAND:
                if ( TargetCommand == null )
                {
                    Debug.LogError( "Context Menu Item: " + this.ToString( ) + " is of ContextMenuType COMMAND but does not contain a command delegate." );
                    return;
                }

                TargetCommand.DynamicInvoke( UIContextMenu.CurrentEvent );

                break;

            case ContextMenuItemType.TARGET:

                break;
        }

        UIContextMenu.Close( );
    }
}