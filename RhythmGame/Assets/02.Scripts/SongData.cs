using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �뷡 ������
/// �뷡 �̸�, ���̵�, ��Ʈ ��� ���� 
/// </summary>
[System.Serializable]
public class SongData
{
    public string Name;
    public int Level;
    public List<NoteData> Notes;

    public SongData(string name, int level)
    {
        Name = name;
        Level = level;
        Notes = new List<NoteData>();
    }

    public SongData()
    {
        Notes = new List<NoteData>();
    }
}
