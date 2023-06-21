using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class CorotineController : MonoBehaviour
{
    List<corotineHolder> corotines = new List<corotineHolder>();

    public string addCorotine(IEnumerator corotine, string name, bool allowDublicates = false)
    {
        corotineHolder c = corotines.Find(x => x.name == name);
        if (c != null)
        {
            if (c.corotine == null)
            { removeCorotine(c.name); }
        }

        if (corotines.Find(x => x.name == name) != null)
        {

            if (!allowDublicates)
            {
                Debug.Log("Corotine with name " + name + " already exists");
                return "empty";

            }
            name += Random.Range(0, 1000);
        }
        corotines.Add(new corotineHolder(corotine, name));
        StartCoroutine(corotine);
        return name;
    }

    public void removeCorotine(string name)
    {
        corotineHolder c = corotines.Find(x => x.name == name);
        if (c == null)
        {
            Debug.Log("Corotine with name " + name + " does not exist");
            return;
        }
        if (c.corotine != null)
        {
            StopCoroutine(c.corotine);
        }
        corotines.Remove(c);
    }

    public void cancelAllCorotines()
    {
        foreach (corotineHolder corotine in corotines)
        {
            StopCoroutine(corotine.corotine);
        }
    }



}
class corotineHolder
{
    public IEnumerator corotine;
    public string name;
    public corotineHolder(IEnumerator corotine, string name = "Default")
    {
        this.corotine = corotine;
        this.name = name;
    }
}