using UnityEngine;
using System.Collections.Generic;
using System;

public class HotkeyController : MonoBehaviour
{

    public List<Hotkey> hotkeys;
    private CharacterCombatController combatController;
    public Dictionary<KeyCode, Hotkey> hotkeyMapping;
    private PlayerController playerController;
    public void Update()
    {
        HandleHotkey();
    }
    public void Start()
    {
        combatController = GetComponent<CharacterCombatController>();
        playerController = GetComponent<PlayerController>();
        hotkeys = new List<Hotkey>();
        for (int i = 0; i < 9; i++)
        {
            hotkeys.Add(new Hotkey());
        }

        hotkeyMapping = new Dictionary<KeyCode, Hotkey>
        {
            { KeyCode.Alpha1, hotkeys[0] },
            { KeyCode.Alpha2, hotkeys[1] },
            { KeyCode.Alpha3, hotkeys[2] },
            { KeyCode.Alpha4, hotkeys[3] },
            { KeyCode.Alpha5, hotkeys[4] },
            { KeyCode.Alpha6, hotkeys[5] },
            { KeyCode.Alpha7, hotkeys[6] },
            { KeyCode.Alpha8, hotkeys[7] },
            { KeyCode.E, hotkeys[8] }
        };

        Hotkey hotkeyTest = new Hotkey();
        hotkeyTest.ability = combatController.abilityController.learnedAbilitys[0];
        hotkeys[0].ability = combatController.abilityController.learnedAbilitys[0];
    }
    private void HandleHotkey()
    {
        foreach (KeyValuePair<KeyCode, Hotkey> entry in hotkeyMapping)
        {
            if (Input.GetKeyDown(entry.Key))
            {
                Hotkey hotkey = entry.Value;
                if (hotkey.ability != null)
                {
                    playerController.PerformAbility(hotkey.ability, this.gameObject);
                }
                // else if (hotkey.item != null)
                {
                    // UseItem(hotkey.item);
                }
            }
            if (Input.GetKeyUp(entry.Key))
            {
                Hotkey hotkey = entry.Value;
                if (hotkey.ability != null)
                {
                    if (hotkey.ability.animingMode == AnimingMode.PrePositionPlacement)
                    {
                        playerController.StopPerformAbility();
                    }
                    if (hotkey.ability.animingMode == AnimingMode.Default)
                    {
                        combatController.StopAbility(hotkey.ability);
                    }
                }
            }
        }
    }

    internal void SwapHotkeys(int hotkeyIndex1, int hotkeyIndex2)
    {
        Hotkey tempHotkey = hotkeys[hotkeyIndex1];
        hotkeys[hotkeyIndex1] = hotkeys[hotkeyIndex2];
        hotkeys[hotkeyIndex2] = tempHotkey;
    }

    internal void AssignAbilityToHotkey(int hotkeyIndex, Ability assignedAbility)
    {
        hotkeys[hotkeyIndex].ability = assignedAbility;
        hotkeys[hotkeyIndex].item = null;
    }

    internal void AssignItemToHotkey(int hotkeyIndex, Item assignedItem)
    {
        hotkeys[hotkeyIndex].item = assignedItem;
        hotkeys[hotkeyIndex].ability = null;
    }

}
public class Hotkey
{
    public Ability ability;
    public Item item;
}