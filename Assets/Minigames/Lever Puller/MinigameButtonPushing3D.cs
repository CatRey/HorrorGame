using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameButtonPushing3D : InteractableInCollider
{
    public List<ButtonWithCollider> buttons = new();
    public List<int> pressed = new();
    
    private void Start()
    {
        base.Start();
        MakeUninteractable();
    }


    private void Update()
    {
        
    }

    public override void MakeInteractable()
    {
        base.MakeInteractable();

        foreach (var item in buttons)
        {
            item.interactable = true;
        }
    }

    public override void MakeUninteractable()
    {
        base.MakeUninteractable();

        foreach (var item in buttons)
        {
            item.interactable = false;
            item.Unmove();
        }

        pressed.Clear();
    }

    public void ButtonPressed(int index)
    {
        if (!pressed.Contains(index))
        {
            pressed.Add(index);
        }
        else
        {
            pressed.Remove(index);
        }

        if (pressed.Count >= buttons.Count)
        {
            MakeUninteractable();
        }
    }
}
