using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectMirrorUIManager : MonoBehaviour
{
    [SerializeField] private SelectMirrorScript selectMirrorScript;
    [SerializeField] private Color highlightColor;
    [SerializeField] private Color normalColor;

    [SerializeField] private List<GameObject> items;

    private void OnEnable()
    {
        selectMirrorScript.OnMirrorSelected += OnMirrorSelected;
    }

    private void OnDisable()
    {
        selectMirrorScript.OnMirrorSelected -= OnMirrorSelected;
    }

    private void OnMirrorSelected(int _mirrorNumber)
    {
        for (int i = 0; i < items.Count; i++)
        {
            Color color = i == _mirrorNumber - 1 ? highlightColor : normalColor;
            items[i].GetComponent<Image>().color = color;
        }
    }
}