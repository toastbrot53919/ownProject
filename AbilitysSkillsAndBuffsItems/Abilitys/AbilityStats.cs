
[System.Serializable]
public class AbilityStats{
    
    public float baseDamage;
    public float strengthScaling;
    public float intelligenceScaling;
    public float cooldown;
    //manacost not implemented yet
    public float manaCost;
    public float range;
    public float projectileSpeed;
    public float stunDuration;
    public AbilityStats(){
        
    }
    public void setZero(){
        baseDamage = 0;

        strengthScaling = 0;
        intelligenceScaling = 0;
        cooldown = 0;
        //manacost not implemented yet
        manaCost = 0;
        range = 0;
        projectileSpeed = 0;
        stunDuration = 0;

    }
    public void addStats(AbilityStats statsToAdd){
        baseDamage += statsToAdd.baseDamage;

        strengthScaling += statsToAdd.strengthScaling;
        intelligenceScaling += statsToAdd.intelligenceScaling;
        cooldown += statsToAdd.cooldown;
        //manacost not implemented yet
        manaCost += statsToAdd.manaCost;
        range += statsToAdd.range;
        projectileSpeed += statsToAdd.projectileSpeed;
        stunDuration += statsToAdd.stunDuration;
    }
    public string printStats(){
        string s  = "";
        if(baseDamage!=0){
            s += "Base Damage: " + baseDamage + "\n";
        }
        if(strengthScaling!=0){
            s += "Strength Scaling: " + strengthScaling + "\n";
        }
        if(intelligenceScaling!=0){
            s += "Intelligence Scaling: " + intelligenceScaling + "\n";
        }
        if(cooldown!=0){
            s += "Cooldown: " + cooldown + "\n";
        }
        if(manaCost!=0){
            s += "Mana Cost: " + manaCost + "\n";
        }
        if(range!=0){
            s += "Range: " + range + "\n";
        }
        if(projectileSpeed!=0){
            s += "Projectile Speed: " + projectileSpeed + "\n";
        }
        if(stunDuration!=0){
            s += "Stun Duration: " + stunDuration + "\n";
        }

        return s;
    }
}