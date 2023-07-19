using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Inheriting this class helps facilitate maintenance and expansion, enhancing flexibility in project development.   
/// </summary>
public abstract class TruongMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        SetDefault();
    }

    /// <summary>
    /// Calling this function makes all variables and dependencies of self and children assigned values.
    /// </summary>
    [Button]
    protected void SetDefaultAll()
    {
        SetDefault();
        var child = GetComponentsInChildren<TruongMonoBehaviour>().ToList();
        child.ForEach(c => c.SetDefault());
    }

    /// <summary>
    /// Renaming variables often leads to variables and dependencies being reset.
    /// Call this function in Awake to ensure that variables and dependencies are assigned values when entering the game.
    /// </summary>
    protected virtual void SetDefault()
    {
        SetDefaultVar();
        LoadComponents();
        SetVarComponentsToDefault();
    }

    /// <summary>
    /// Renaming variables often leads to variables being reset.
    /// Therefore, assign default values to variables in this function to initialize them quickly instead of re-entering them in the Unity editor.
    /// </summary>
    protected virtual void SetDefaultVar()
    {
        //For override
    }

    /// <summary>
    /// Renaming variables often leads to the loss of dependencies for components.
    /// Therefore, assign default values to components in this function to initialize them quickly instead of re-entering them in the Unity editor.
    /// </summary>
    protected virtual void LoadComponents()
    {
        //For override
    }

    /// <summary>
    /// The variables of the components are assigned values after loading the components.
    /// </summary>
    protected virtual void SetVarComponentsToDefault()
    {
        //For override
    }

    /// <summary>
    /// Calling this function helps the children's variables and dependencies to be assigned values.
    /// </summary>
    protected void SetDefaultChild()
    {
        var child = GetComponentsInChildren<TruongMonoBehaviour>().ToList();
        child.ForEach(c => c.SetDefault());
    }


    protected T GetParentComponent<T>() where T : Component
    {
        T component = null;
        Transform parent = transform.parent;

        while (parent != null)
        {
            component = parent.GetComponent<T>();
            if (component != null)
            {
                // Found the component, exit the loop
                break;
            }

            // Continue searching in the next parent object
            parent = parent.parent;
        }

        if (component != null)
        {
            // Component found in the parent object
            return component;
        }

        // Component not found in the parent objects
        return null;
    }
}