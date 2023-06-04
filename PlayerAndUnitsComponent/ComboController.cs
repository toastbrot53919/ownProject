using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ComboController{
    public List<ComboCounter> comboCounterList;

    public ComboController(){
        comboCounterList = new List<ComboCounter>();
    }
    public void UpdateComboController(){
        foreach (ComboCounter comboCounter in comboCounterList)
        {
            comboCounter.UpdateComboCounter();
        }
    }
    public void IncreaseComboCounter(string comboName){
        bool found = false;
        foreach (ComboCounter comboCounter in comboCounterList)
        {
            if(comboCounter.ComboName == comboName){
                comboCounter.IncreaseComboCounter();
                found = true;
            }
        }
        if(!found){
            comboCounterList.Add(new ComboCounter(1f,comboName));
        }
    }
    public int GetComboCounter(string comboName){
        foreach (ComboCounter comboCounter in comboCounterList)
        {
            if(comboCounter.ComboName == comboName){
                return comboCounter.GetComboCounter();
            }
        }
        return 0;
    }

    internal void ResetComboCounter(string comboName)
    {
        foreach (ComboCounter comboCounter in comboCounterList)
        {
            if(comboCounter.ComboName == comboName){
                comboCounter.ResetComboCounter();
            }
        }
    }
}
[System.Serializable]
public class ComboCounter{
    
    public string ComboName;
    public int comboCounter;
    public float comboTimer;
    public float comboTimeLimit;
    public ComboCounter(float comboTimeLimit,string comboName){
        this.comboTimeLimit = comboTimeLimit;
        comboCounter = 0;
        comboTimer = 0;
        ComboName = comboName;
    }
    public void UpdateComboCounter(){
        comboTimer += Time.deltaTime;
        if(comboTimer >= comboTimeLimit){
            comboCounter = 0;
        }
    }
    public void IncreaseComboCounter(){
        UpdateComboCounter();
        comboCounter++;
        comboTimer = 0;
    }
    public int GetComboCounter(){
        return comboCounter;
    }
    public void ResetComboCounter(){
        comboCounter = 0;
        comboTimer = 0;
    }
}