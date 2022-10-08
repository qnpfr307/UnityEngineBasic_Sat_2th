using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;
public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance;
    public static float NoteSpeedScale = 2.0f;

    public bool IsReady = false;
    private Queue<NoteData> _noteDataQueue = new Queue<NoteData>();
    private Dictionary<KeyCode, NoteSpawner> _spawners = new Dictionary<KeyCode, NoteSpawner>();

    [SerializeField] private Transform _spawnersPoint;
    [SerializeField] private Transform _noteHitterPoint;
    [SerializeField] private VideoPlayer _videoPlayer;

    public float NoteFallingDistance => _spawnersPoint.transform.position.y - _noteHitterPoint.transform.position.y;
    public float NoteFallingTime => NoteFallingDistance / NoteSpeedScale;

    public void StartSpawn()
    {
        if (_noteDataQueue.Count > 0)
        {
            StartCoroutine(E_Spawning());
            Invoke("PlayVideo", NoteFallingTime);
        }
    }

    IEnumerator E_Spawning()
    {
        float timeMark = Time.time;
        NoteData noteData;

        // note data�� ���������� ��� ����
        while (_noteDataQueue.Count > 0)
        {
            // ��ȯ�ð��� �� ��Ʈ ���� ť���� ���� ��ȯ��
            while (_noteDataQueue.Count > 0)
            {
                if (_noteDataQueue.Peek().Time < (Time.time - timeMark))
                {
                    noteData = _noteDataQueue.Dequeue();
                    _spawners[noteData.Key].Spawn();
                }
                else
                {
                    break;
                }
            }
            yield return null;
        }
    }


    private void Awake()
    {
        Instance = this;

        StartCoroutine(E_Init());
    }

    IEnumerator E_Init()
    {
        NoteSpawner[] noteSpawners = _spawnersPoint.GetComponentsInChildren<NoteSpawner>();
        for (int i = 0; i < noteSpawners.Length; i++)
            _spawners.Add(noteSpawners[i].Key, noteSpawners[i]);

        // SongSelector �� �뷡������ �ε� �ɶ����� ��ٸ�
        yield return new WaitUntil(() => SongSelector.Instance &&
                                         SongSelector.Instance.IsDataLoaded);

        // ��Ʈ ������ �ð��� ���� �� ť�� ���
        IOrderedEnumerable<NoteData> noteDataFiltered = SongSelector.Instance.Data.Notes.OrderBy(note => note.Time);
        foreach (NoteData noteData in noteDataFiltered)
            _noteDataQueue.Enqueue(noteData);

        IsReady = true;
    }

    private void PlayVideo()
    {
        _videoPlayer.clip = SongSelector.Instance.Clip;
        _videoPlayer.Play();
    }
}
