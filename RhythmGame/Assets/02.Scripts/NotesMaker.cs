using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NotesMaker : MonoBehaviour
{
    private SongData _songData;
    private KeyCode[] _keys = { KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.Space, KeyCode.J, KeyCode.K, KeyCode.L };
    [SerializeField] private VideoPlayer _vp;
    [SerializeField] private int _level = 1;
    public bool DoRecord;


    //===========================================================================
    //**************************** Public Methods *******************************
    //===========================================================================
    
    /// <summary>
    /// �뷡 ������ ����, ��ȭ����
    /// </summary>
    public void StartRecording()
    {
        if (DoRecord)
            return;

        _songData = new SongData(_vp.clip.name, _level);
        _vp.Play();
        DoRecord = true;
    }

    /// <summary>
    /// Ű �Է� üũ�ϸ鼭 �ش� Ű�� ���� ��Ʈ ������ ����
    /// Update() ���� ȣ���ؾ���
    /// </summary>
    public void Recording()
    {
        foreach (KeyCode key in _keys)
        {
            if (Input.GetKeyDown(key))
                CreateNoteData(key);
        }
    }

    /// <summary>
    /// ��ȭ�� ������ư ������ �뷡������ ���� �� ���� ����
    /// </summary>
    public void StopRecording()
    {
        if (DoRecord == false)
            return;

        SaveSongData();
        _songData = null;
        _vp.Stop();
    }


    //===========================================================================
    //**************************** Private Methods ******************************
    //===========================================================================

    private void Update()
    {
        if (DoRecord)
            Recording();
    }

    private void CreateNoteData(KeyCode keyCode)
    {
        NoteData noteData = new NoteData();

        float time = (float)System.Math.Round(_vp.time, 2);
        noteData.Time = time;
        noteData.Key = keyCode;

        _songData.Notes.Add(noteData);
        Debug.Log($"NotesMaker : Note created, {keyCode}, {time} ");
    }

    private void SaveSongData()
    {
        string dir = UnityEditor.EditorUtility.SaveFilePanel("������ ���� �����ϼ���", "", $"{_songData.Name}", "json");
        System.IO.File.WriteAllText(dir, JsonUtility.ToJson(_songData));
    }
}
