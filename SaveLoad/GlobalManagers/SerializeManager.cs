using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using System.Collections.Generic;
// das hier is halb flash. die set methoden sind kacke. ich wollte nen interface machen für die classen (loadFromSaveData())
//Nimm interface mit nem genericTyp .iterface 2 methoden loadFromSaveData and StoreInSaveData
public static class SerializeManager
{
    private static readonly string SavePath = Application.persistentDataPath + "/saves/";

    public static void Save<T>(T data, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        // Create SurrogateSelector
        // Create BinaryFormatter

        // Create SurrogateSelector
        SurrogateSelector selector = new SurrogateSelector();

        // Add Vector3SerializationSurrogate to handle Vector3
        Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);

        // Add QuaternionSerializationSurrogate to handle Quaternion
        QuaternionSerializationSurrogate quaternionSurrogate = new QuaternionSerializationSurrogate();
        selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);

        // Set SurrogateSelector of formatter
        formatter.SurrogateSelector = selector;


        if (!Directory.Exists(SavePath))
            Directory.CreateDirectory(SavePath);

        string path = SavePath + fileName;

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);

        stream.Close();
    }


    public static T Load<T>(string fileName)
    {
        string path = SavePath + fileName;

        if (!File.Exists(path))
        {
            Debug.LogError("Save file not found in " + path);
            return default(T);
        }

        BinaryFormatter formatter = new BinaryFormatter();
        // Create SurrogateSelector
        // Create BinaryFormatter

        // Create SurrogateSelector
        SurrogateSelector selector = new SurrogateSelector();

        // Add Vector3SerializationSurrogate to handle Vector3
        Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);

        // Add QuaternionSerializationSurrogate to handle Quaternion
        QuaternionSerializationSurrogate quaternionSurrogate = new QuaternionSerializationSurrogate();
        selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);

        // Set SurrogateSelector of formatter
        formatter.SurrogateSelector = selector;


        FileStream stream = new FileStream(path, FileMode.Open);

        T data = (T)formatter.Deserialize(stream);

        stream.Close();

        return data;
    }
    public static void SavePlayer(GameObject player)
    {
        SaveData saveData = new SaveData();
        saveData.playerSaveData = player.GetComponent<PlayerController>().GetSaveData();
        saveData.abilityControllerSaveData = player.GetComponent<AbilityController>().GetSaveData();
        saveData.inventorySaveData = player.GetComponent<Inventory>().GetSaveData();
        saveData.buffSysteSaveData = player.GetComponent<BuffSystem>().GetSaveData();
        saveData.healthControllerSaveData = player.GetComponent<HealthController>().GetSaveData();
        saveData.manaControllerSaveData = player.GetComponent<ManaController>().GetSaveData();
        saveData.equipManagerSaveData = player.GetComponent<EquipManager>().GetSaveData();
        saveData.experienceSystemSaveData = player.GetComponent<ExperienceController>().GetSaveData();
        saveData.stunnableSaveData = player.GetComponent<isStunnableController>().GetSaveData();
        saveData.skillControllerSaveData = player.GetComponent<SkillController>().GetSaveData();
        saveData.characterStatsSaveData = player.GetComponent<CharacterStats>().GetSaveData();


        Save(saveData, "player");

    }
    public static void LoadPlayer(GameObject player)
    {
        SaveData saveData = Load<SaveData>("player");
        if (saveData == null)
        {
            Debug.Log("No SaveData found");
            return;
        }
        player.GetComponent<PlayerController>().LoadFromSaveData(saveData.playerSaveData);
        player.GetComponent<AbilityController>().LoadFromSaveData(saveData.abilityControllerSaveData);
        player.GetComponent<Inventory>().LoadFromSaveData(saveData.inventorySaveData);
        player.GetComponent<BuffSystem>().LoadFromSaveData(saveData.buffSysteSaveData);
        player.GetComponent<HealthController>().LoadFromSaveData(saveData.healthControllerSaveData);
        player.GetComponent<ManaController>().LoadFromSaveData(saveData.manaControllerSaveData);
        player.GetComponent<EquipManager>().LoadFromSaveData(saveData.equipManagerSaveData);
        player.GetComponent<ExperienceController>().LoadFromSaveData(saveData.experienceSystemSaveData);
        player.GetComponent<isStunnableController>().LoadFromSaveData(saveData.stunnableSaveData);
        player.GetComponent<SkillController>().LoadFromSaveData(saveData.skillControllerSaveData);
        player.GetComponent<CharacterStats>().LoadFromSaveData(saveData.characterStatsSaveData);

    }
    sealed class Vector3SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(System.Object obj, SerializationInfo info, StreamingContext context)
        {
            Vector3 v3 = (Vector3)obj;
            info.AddValue("x", v3.x);
            info.AddValue("y", v3.y);
            info.AddValue("z", v3.z);
        }

        public System.Object SetObjectData(System.Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Vector3 v3 = (Vector3)obj;
            v3.x = (float)info.GetValue("x", typeof(float));
            v3.y = (float)info.GetValue("y", typeof(float));
            v3.z = (float)info.GetValue("z", typeof(float));
            obj = v3;
            return obj;
        }
    }
    sealed class QuaternionSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(System.Object obj, SerializationInfo info, StreamingContext context)
        {
            Quaternion quaternion = (Quaternion)obj;
            info.AddValue("x", quaternion.x);
            info.AddValue("y", quaternion.y);
            info.AddValue("z", quaternion.z);
            info.AddValue("w", quaternion.w);
        }

        public System.Object SetObjectData(System.Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Quaternion quaternion = (Quaternion)obj;
            quaternion.x = (float)info.GetValue("x", typeof(float));
            quaternion.y = (float)info.GetValue("y", typeof(float));
            quaternion.z = (float)info.GetValue("z", typeof(float));
            quaternion.w = (float)info.GetValue("w", typeof(float));
            obj = quaternion;
            return obj;
        }
    }


}
[System.Serializable]
public class SaveData
{
    //Add all SaveDatas that needs to be stored

    public PlayerSaveData playerSaveData;

    public CharacterStatsSaveData characterStatsSaveData;
    public AbilityControllerSaveData abilityControllerSaveData;
    public InventorySaveData inventorySaveData;
    public BuffSystemSaveData buffSysteSaveData;
    public HealthControllerSaveData healthControllerSaveData;
    public ManaControllerSaveData manaControllerSaveData;

    public EquipManagerSaveData equipManagerSaveData;
    public ExperienceSystemSaveData experienceSystemSaveData;

    public StunnableSaveData stunnableSaveData;

    public SkillControllerSaveData skillControllerSaveData;


}


/*   public List<Skill> activeSkills;
    public SkillTree skillTree;
    public int availableSkillPoints;
    public StatsModifier totalStatsModier;
    public delegate void SkillEvent(SkillNode skillNode);
    public event SkillEvent OnSkillUnlocked;
    public event SkillEvent OnSkillUnlearnd;*/











