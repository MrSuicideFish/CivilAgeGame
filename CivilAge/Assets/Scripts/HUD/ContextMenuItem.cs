using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ContextMenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum ContextMenuItemType
    {
        TARGET,
        COMMAND
    }
    public ContextMenuItemType ItemType = ContextMenuItemType.COMMAND;

    public bool IsHovering { get; private set; }

    public void OnPointerEnter( PointerEventData eventData )
    {
        IsHovering = true;
    }

    public void OnPointerExit( PointerEventData eventData )
    {
        IsHovering = false;
    }
}