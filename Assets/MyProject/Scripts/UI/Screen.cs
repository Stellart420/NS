using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Screen : MonoBehaviour
{
    [SerializeField] protected bool immediately= false;
    public bool IsOpen { get; private set; }

    public event Action<Screen> OpenAction = delegate { };

    void Awake()
    {
        if (UIController.Instance.screens.Find((screen) => screen == this) == null)
            UIController.Instance.screens.Add(this);
        Initialization();
    }

    public void Open()
    {
        IsOpen = true;
        OpenAction?.Invoke(this);
        gameObject.SetActive(true);
        SelfOpen();

    }

    protected abstract void SelfOpen();

    public void Close()
    {
        IsOpen = false;

        gameObject.SetActive(false);
        SelfClose();
    }

    protected abstract void SelfClose();

    

    protected virtual void Initialization() 
    {
        if (immediately)
            Open();
        else
            Close();
    }
}
