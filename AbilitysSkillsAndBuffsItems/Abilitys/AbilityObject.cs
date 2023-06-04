using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityObject : MonoBehaviour
{
    public event Action OnUpdate;
    public event Action OnSpawn;
    public event Action OnDelete;

    public event Action<GameObject> OnHit;

    public AbilityData data;

    public bool shouldDestroy = false;

    public bool deleteOnCollision = true;
    public bool deleteOnTimer = false;
    float timer = 0f;
    public float timerMax = 5f;




    public Ability ParentAbility { get; set; }

    public void onStart()
    {
        HandleOnSpawn();
    }
    protected virtual void HandleOnHit(GameObject target)
    {
        // Trigger OnHit event with target as parameter
        OnHit?.Invoke(target);

        if (deleteOnCollision)
        {
            shouldDestroy = true;
        }
    }

    private void Update()
    {
        // Trigger OnHit event with target as parameter
        OnUpdate?.Invoke();
        if (deleteOnTimer)
        {
            timer += Time.deltaTime;
            if (timer >= timerMax)
            {
                timer = 0f;
                HandleOnDelete();
            }
        }

    }

    protected void HandleOnSpawn()
    {
        // Perform any initialization or setup for the ability object here
        // Trigger OnSpawn event
        OnSpawn?.Invoke();


    }

    protected void HandleOnDelete()
    {
        // Perform any cleanup or deactivation for the ability object here
        // Trigger OnDelete event
        OnDelete?.Invoke();
        Destroy(gameObject);

    }
    public void Delete()
    {
        HandleOnDelete();
    }
    public void Awake()
    {
        HandleOnSpawn();
    }

    public List<(GameObject, float)> alreadyHit = new List<(GameObject, float)>(); // Gameobject, TimeAdded
    public List<int> alreadyHitRemoveIndeces = new List<int>();

    private void OnTriggerEnter(Collider collision)
    {
        if (data == null)
        {
            Debug.LogError("AbilityObject data is null");
            return;
        }
        // Get target HealthController from collided object
        if (data.CasterStats != null)
        {
            if (data.CasterStats.gameObject.name == collision.gameObject.name)
            {
                return;
            }
            if (gameObject.name == collision.gameObject.name)
            {
                return;
            }
        }
        for (int i = 0; i < alreadyHit.Count; i++)
        {
            if (alreadyHit[i].Item1 == collision.gameObject)
            {
                if (Time.time - alreadyHit[i].Item2 > data.OnHitInterval)
                {
                    alreadyHitRemoveIndeces.Add(i);
                    break;
                }
                return;
            }
        }
        foreach (var item in alreadyHitRemoveIndeces)
        {
            alreadyHit.RemoveAt(item);
        }
        alreadyHitRemoveIndeces.Clear();
        alreadyHit.Add((collision.gameObject, Time.time));
        // Call HandleOnHit method with target as parameter
        ParentAbility?.OnAbilityObjectHit(this, collision.gameObject);
        HandleOnHit(collision.gameObject);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (data == null)
        {
            Debug.LogError("AbilityObject data is null");
            return;
        }
        // Get target HealthController from collided object
        if (data.CasterStats != null)
        {
            if (data.CasterStats.gameObject.name == collision.gameObject.name)
            {
                return;
            }
            if (gameObject.name == collision.gameObject.name)
            {
                return;
            }
        }
        //if contains gameObject return;
        for (int i = 0; i < alreadyHit.Count; i++)
        {
            if (alreadyHit[i].Item1 == collision.gameObject)
            {
                if (Time.time - alreadyHit[i].Item2 > data.OnHitInterval)
                {
                    alreadyHitRemoveIndeces.Add(i);
                    break;
                }
                return;
            }
        }
        foreach (var item in alreadyHitRemoveIndeces)
        {
            alreadyHit.RemoveAt(item);
        }
        alreadyHitRemoveIndeces.Clear();

        alreadyHit.Add((collision.gameObject, Time.time));
        ParentAbility?.OnAbilityObjectHit(this, collision.gameObject);
        HandleOnHit(collision.gameObject);

    }
}


public interface IBouncingAbilityObject
{
    float BounceIntensity { get; set; }
    float BounceDuration { get; set; }
    void Bounce(GameObject target);
}

public interface IPiercingAbilityObject
{
    void Pierce(GameObject target);
}

public interface IHomingAbilityObject
{
    void Home(GameObject target);
}