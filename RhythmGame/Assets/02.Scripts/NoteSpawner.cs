using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ʈ ��ȯ Ŭ����
/// </summary>
public class NoteSpawner : MonoBehaviour
{
    public KeyCode Key;
    [SerializeField] private GameObject _notePrefab;

    /// <summary>
    /// ��Ʈ ��ȯ�� �������� ����
    /// </summary>
    public void Spawn()
    {
        GameObject note = Instantiate(_notePrefab, transform.position, Quaternion.identity);
        note.transform.localScale = transform.lossyScale;
    }
}
