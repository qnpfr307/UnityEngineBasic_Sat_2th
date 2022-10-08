using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ʈ Ÿ�� & ���� 
/// </summary>
public class NoteHitter : MonoBehaviour
{
    public KeyCode Key;
    [SerializeField] private LayerMask _noteLayer;
    private SpriteRenderer _spriteRenderer;
    private Color _colorOrigin;
    [SerializeField] private Color _colorPressed;
    [SerializeField] private ParticleSystem _hitEffect;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _colorOrigin = _spriteRenderer.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            HitNote();
            SetColorPressed();
        }            
        
        if (Input.GetKeyUp(Key))
        {
            SetColorOrigin();
        }   
    }

    /// <summary>
    /// ��Ʈ Ÿ�� ���� ���� ���� ��� ��Ʈ ���� ��
    /// ���� ����� ��Ʈ�� ��Ʈ������
    /// </summary>
    private void HitNote()
    {
        HitType hitType = HitType.Bad;
        List<Collider2D> overlaps = Physics2D.OverlapBoxAll(point: transform.position,
                                                            size: new Vector2(transform.lossyScale.x / 2.0f,
                                                                              transform.lossyScale.y * Constants.HIT_JUDGE_RANGE_BAD),
                                                            angle: 0.0f,
                                                            layerMask: _noteLayer).ToList();
        if (overlaps.Count > 0)
        {
            // �������� ��� �ݶ��̴� ���������� �������� ����
            IOrderedEnumerable<Collider2D> collidersFiltered = overlaps.OrderBy(x => Mathf.Abs(x.transform.position.y - transform.position.y));
            
            // ���� ����� �ݶ��̴��� ���ؼ� ������ �Ÿ� ���ϱ�
            float distance = Mathf.Abs(collidersFiltered.First().transform.position.y - transform.position.y);

            // �Ÿ��� ���� ��Ʈ ����
            if      (distance < Constants.HIT_JUDGE_RANGE_COOL / 2.0f)  hitType = HitType.Cool;
            else if (distance < Constants.HIT_JUDGE_RANGE_GREAT / 2.0f) hitType = HitType.Great;
            else if (distance < Constants.HIT_JUDGE_RANGE_GOOD / 2.0f)  hitType = HitType.Good;
            else if (distance < Constants.HIT_JUDGE_RANGE_MISS / 2.0f)  hitType = HitType.Miss;


            // ������ ��Ʈ ��Ʈ�ϱ�
            GameObject effect = Instantiate(_hitEffect.gameObject, transform.position, Quaternion.identity);
            Destroy(effect, _hitEffect.main.duration + _hitEffect.main.startLifetime.constantMax);
            collidersFiltered.First().gameObject.GetComponent<Note>().Hit(hitType);
            Destroy(collidersFiltered.First().gameObject);
        }
    }

    private void SetColorPressed()
    {
        _spriteRenderer.color = _colorPressed;
    }

    private void SetColorOrigin()
    {
        _spriteRenderer.color = _colorOrigin;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2.0f,
                                                            transform.lossyScale.y * Constants.HIT_JUDGE_RANGE_BAD,
                                                            0.0f));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2.0f,
                                                            transform.lossyScale.y * Constants.HIT_JUDGE_RANGE_MISS,
                                                            0.0f));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2.0f,
                                                            transform.lossyScale.y * Constants.HIT_JUDGE_RANGE_GOOD,
                                                            0.0f));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2.0f,
                                                            transform.lossyScale.y * Constants.HIT_JUDGE_RANGE_GREAT,
                                                            0.0f));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2.0f,
                                                            transform.lossyScale.y * Constants.HIT_JUDGE_RANGE_COOL,
                                                            0.0f));

    }
}
