using System;
using UnityEngine;

[Serializable]
public class ComboData
{
    [SerializeField] private string comboName;
    [SerializeField] private KeyCode[] sequence;
    [SerializeField] private AudioClip clip;
    [SerializeField] private Vector3 direction;

    public string ComboName => comboName;
    public KeyCode[] Sequence => sequence;
    public AudioClip Clip => clip;
    public Vector3 Direction => direction;
    public int Length => sequence.Length;
}