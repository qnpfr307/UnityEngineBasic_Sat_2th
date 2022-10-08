using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float rotateSpeed = 200.0f;
    private float h => Input.GetAxis("Horizontal");
    private float v => Input.GetAxis("Vertical");

    private float r => Input.GetAxis("Mouse X");

    /// <summary>
    /// �ʱ�ȭ �̺�Ʈ
    /// Scene Load�� ȣ�� ( GameObject �� Ȱ��ȭ �Ǿ������� ȣ�� )
    /// ���� MonoBehavior �� ��Ȱ��ȭ �Ǿ��־ ȣ��
    /// �Ϲ� Ŭ������ ������ ������� �ַ� ��� (������� �ʱ�ȭ ��)
    /// </summary>
    private void Awake()
    {
        
    }

    /// <summary>
    /// �ʱ�ȭ �̺�Ʈ
    /// GameObject �� Ȱ��ȭ �� �� ���� ȣ��
    /// </summary>
    private void OnEnable()
    {
        
    }

    /// <summary>
    /// �����ͻ󿡼� �ʱ�ȭ �̺�Ʈ
    /// GameObject �� �� MonoBehavior �� �߰������� ȣ�� (���� ȣ�⵵ ����)
    /// Play ��忡���� ȣ����� ����
    /// </summary>
    private void Reset()
    {
        
    }

    /// <summary>
    /// Fixed �����Ӹ��� ȣ��Ǵ� �̺�Ʈ 
    /// ���������� ����Ǵ� ������ ó���Ҷ� ��� (����� ���ɿ� ������ ������ �ȵǴ� �����)
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(h, 0.0f, v).normalized;
        Vector3 deltaMove = dir * moveSpeed * Time.deltaTime;
        //transform.position += deltaMove;
        transform.Translate(deltaMove);

        Vector3 deltaRotate = Vector3.up * r * rotateSpeed * Time.deltaTime;
        transform.Rotate(deltaRotate);
    }

    /// <summary>
    /// �� �����Ӹ��� ȣ��Ǵ� �̺�Ʈ
    /// </summary>
    private void Update()
    {

    }

    /// <summary>
    /// �� �����Ӹ��� ȣ��Ǵ� �̺�Ʈ
    /// Update() ���Ŀ� ȣ���
    /// Ư�� Camera �̵����� � ��� 
    /// </summary>
    private void LateUpdate()
    {
        
    }

    /// <summary>
    /// Gizmos �� ������ �� ������ �����Ҷ����� ȣ��Ǵ� �Լ�
    /// Gizmos : ����� ���� ���ؼ� ȭ��� �׷����� ��� �׷����� ���
    /// </summary>
    private void OnDrawGizmos()
    {
        
    }

    /// <summary>
    /// �� MonoBehavior �� ������Ʈ�� ������ GameObject�� ���õǾ������� ȣ��Ǵ� �Լ�
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        
    }

    /// <summary>
    /// GUI : Graphical User Interface 
    /// GUI �� �̺�Ʈ���� �ڵ鸵�ϴ� �Լ� 
    /// </summary>
    private void OnGUI()
    {
        
    }

    /// <summary>
    /// ���� �Ͻ����� / �Ͻ����� ������ ȣ��
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        
    }

    /// <summary>
    /// ���� ���õ� ������ ����ɶ� ȣ�� (���� ���� ���õǸ� true, ���� �����ϸ� false)
    /// </summary>
    /// <param name="focus"></param>
    private void OnApplicationFocus(bool focus)
    {
        
    }

    /// <summary>
    /// ���� ����ɶ� ȣ��
    /// </summary>
    private void OnApplicationQuit()
    {
        
    }

    /// <summary>
    /// �� MonoBehavior �� ������Ʈ�� ������ GameObject�� ��Ȱ��ȭ �� �� ȣ��
    /// </summary>
    private void OnDisable()
    {
        
    }

    /// <summary>
    /// �� MonoBehavior �� ������Ʈ�� ������ GameObject �� �ı��ɶ� ȣ��
    /// </summary>
    private void OnDestroy()
    {
        // GameObject �� �����ϴ� ������ ���� �ȵ�
    }
}
