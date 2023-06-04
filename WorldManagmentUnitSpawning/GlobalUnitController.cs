using System.Collections.Generic;
using UnityEngine;
public static class GlobalUnitController
{
    public static List<GameObject> units = new List<GameObject>();
    public static void addUnit(GameObject unit)
    {
        Debug.Log("Unit added To GlobalUnitController");
        units.Add(unit);
    }
}
