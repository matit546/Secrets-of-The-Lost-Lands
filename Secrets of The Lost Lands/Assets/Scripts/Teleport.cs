using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public List<Transform> destination = new List<Transform>();
    public GameObject  panel;
    public Dropdown dropdown;
    public Player player;
    CharacterController cc;
    bool isVisible;
    private void Start()
    {
        cc = player.GetComponent<CharacterController>();

        foreach (var dest in destination)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = dest.gameObject.name.ToString() });
        }
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    private void DropdownItemSelected(Dropdown dropdown)
    {
        cc.enabled = false;
        int index = dropdown.value;
        player.transform.position = destination[index].position;
        cc.enabled = true;
        panel.SetActive(false);
        isVisible ^= true;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote) && player.godMode)
        {
            isVisible ^= true;

            if (isVisible)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                panel.SetActive(true);
            }
            else
            {
                Cursor.visible = false;
                panel.SetActive(false);
            }
        }
    }

}