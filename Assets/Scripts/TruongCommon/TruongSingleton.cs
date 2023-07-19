using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
///  Inherit this function to ensure that there is only one instance of a class created.
///  You can access the unique object from anywhere in the program using the syntax "ClassName.Instance".
/// </summary>
public abstract class TruongSingleton<T> : TruongMonoBehaviour
{
    public static T Instance { get; private set; }

    [SerializeField] protected bool dontDestroyOnLoad;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize()
    {
        SetInstance();
        SetDontDestroyOnLoadObj();
    }

    private void SetDontDestroyOnLoadObj()
    {
        SetDontDestroyOnLoad();
        if (!dontDestroyOnLoad || Instance == null) return;
        DontDestroyOnLoad(gameObject);
        SetParentTransform();
    }

    private void SetParentTransform()
    {
        //Don't destroy on load only works on root objects so let's force this transform to be a root object:
        transform.parent = null;
    }

    // [Button]
    private T DebugInstance()
    {
        return Instance;
    }

    // [Button]
    private void SetInstance()
    {
        var components = GetComponents<T>();
        LogException(components);
        if (components == null) return;
        Instance = components[0];
    }

    private void LogException(T[] components)
    {
        if (components == null)
        {
            Debug.LogError(
                $"Component inheriting from TruongSingleton not found.");
            return;
        }

        if (components.Length <= 1) return;
        Debug.LogError(
            $"There are more than one component inheriting from TruongSingleton.");
    }

    protected abstract void SetDontDestroyOnLoad();
}