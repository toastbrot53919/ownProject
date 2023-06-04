using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
internal class UiAbilitySlot : UiBaseDragAndDropFunc
{
   public Ability ability;
   public Image icon;

    private void Start()
    {
        if(ability!=null){
            icon.sprite = ability.icon;
        }
    }

   
}
