using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public abstract class Screen : MonoBehaviour
{
    [SerializeField] protected bool immediately= false;
    public bool IsOpen { get; private set; }

    public event Action<Screen> OpenAction = delegate { };

    protected CanvasGroup panel;
    void Awake()
    {
        if (UIController.Instance.screens.Find((screen) => screen == this) == null)
            UIController.Instance.screens.Add(this);

        panel = GetComponent<CanvasGroup>();
        Initialization();
    }

    public void Open()
    {
        IsOpen = true;
        gameObject.SetActive(true);
        OpenAction?.Invoke(this);
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
