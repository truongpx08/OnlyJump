using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Spawner : TruongMonoBehaviour
{
    [SerializeField] protected Transform holder;
    [SerializeField] protected List<Transform> prefabs = new List<Transform>();
    [SerializeField] protected List<Transform> objectsPool = new List<Transform>();

    protected override void LoadComponents()
    {
        LoadPrefabs();
        LoadHolder();
    }

    protected override void Awake()
    {
        base.Awake();
        HidePrefab();
    }

    private void LoadHolder()
    {
        if (holder != null) return;
        holder = transform.Find(TruongConstants.HOLDER);
    }


    private void LoadPrefabs()
    {
        if (!ShouldLoadPrefabs()) return;
        prefabs.Clear();
        var prefabsTransform = transform.Find(TruongConstants.PREFABS);
        if (prefabsTransform == null)
        {
            Debug.LogError("No Prefabs found");
            return;
        }

        foreach (Transform prefab in prefabsTransform)
        {
            prefabs.Add(prefab);
        }

        CheckDuplicatePrefab();

        if (Application.isPlaying)
        {
            Debug.LogWarning("Since prefabs is empty, you need to reset it at inspector for initialization.");
        }
    }

    protected bool ShouldLoadPrefabs()
    {
        try
        {
            if (prefabs.Any(p => p == null) || prefabs.Count == 0) return true;
        }
        catch (Exception e)
        {
            return true;
        }

        return false;
    }

    protected void CheckDuplicatePrefab()
    {
        var duplicates = prefabs.GroupBy(x => x.name)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);

        foreach (var prefabName in duplicates)
        {
            Debug.LogError($"Only one {prefabName} is allowed in prefabs, Has remove a {prefabName}");
        }
    }

    [Button]
    private void HidePrefab()
    {
        prefabs.ForEach(p => p.gameObject.SetActive(false));
    }

    [Button]
    private void ShowPrefab()
    {
        prefabs.ForEach(p => p.gameObject.SetActive(true));
    }

    public Transform Spawn(string prefabName)
    {
        var prefab = GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.LogError($"No prefab {prefabName} found");
            return null;
        }

        return Spawn(prefab);
    }

    public Transform Spawn(Transform prefab)
    {
        var newPrefab = GetObjectFromPool(prefab);
        SetPrefab(newPrefab);
        return newPrefab;
    }

    protected Transform Spawn()
    {
        var prefab = GetRandomPrefab();
        if (prefab == null)
        {
            Debug.LogError($"No prefab found");
            return null;
        }

        var newPrefab = GetObjectFromPool(prefab);
        SetPrefab(newPrefab);
        return newPrefab;
    }

    protected void SetPrefab(Transform newPrefab)
    {
        // newPrefab.SetPositionAndRotation(spawnPosition, spawnRotation);
        newPrefab.parent = holder;
        newPrefab.gameObject.SetActive(true);
    }

    [Button]
    protected internal void Despawn(Transform obj)
    {
        obj.gameObject.SetActive(false);
        objectsPool.Add(obj);
        OnDespawn();
    }

    protected virtual void OnDespawn()
    {
        //For override
    }


    protected Transform GetObjectFromPool(Transform prefab)
    {
        foreach (var obj in objectsPool.Where(obj => obj.name == prefab.name))
        {
            objectsPool.Remove(obj);
            return obj;
        }

        return InstantiateObject(prefab);
    }


    protected Transform InstantiateObject(Transform obj)
    {
        Transform newPrefab = Instantiate(obj);
        newPrefab.name = obj.name;
        return newPrefab;
    }

    protected Transform GetPrefabByName(string prefabName)
    {
        return prefabs.Find(p => p.name == prefabName);
    }

    protected Transform GetRandomPrefab()
    {
        int index = Random.Range(0, prefabs.Count);
        return prefabs[index];
    }
}