public interface ICanStoreAndLoad<T>
{
    void LoadFromSaveData(T saveData);

    T GetSaveData();
}