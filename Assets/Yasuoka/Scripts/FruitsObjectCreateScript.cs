using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsObjectCreateScript : MonoBehaviour
{
    //public List<string> fruitsNameList = new List<string> { "cherry", "strawberry", "grape", "dekopon", "persimmon", "apple", "pear", "peach", "pineapple", "melon", "watermelon" };
    //public GameObject[] fruitsObject = new GameObject[11];

    [Header("�X�e�[�W�~�̐ݒ�")]
    // �~�̔��a
    [SerializeField] private float _radius = 1.0f;
    // �~�̒��S�_
    [SerializeField] private Vector3 _center;

    [Header("�z�u����Prefab")]
    [SerializeField] private int Prefab_num = 30;
    [SerializeField] private GameObject _prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (UIDirector.timerIsRunning)
        {
            GameObject[] allCherry = GameObject.FindGameObjectsWithTag("cherry");
            int remainCherry = allCherry.Length;
            // ����̌��������Ȃ��Ȃ�����
            if (remainCherry < Prefab_num)
            {
                // �w�肳�ꂽ���a�̉~���̃����_���ʒu���擾
                var circlePos = _radius * Random.insideUnitCircle;

                // XZ���ʂŎw�肳�ꂽ���a�A���S�_�̉~���̃����_���ʒu���v�Z
                var spawnPos = new Vector3(circlePos.x, 5, circlePos.y) + _center;

                // Prefab��ǉ�
                Instantiate(_prefab, spawnPos, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_center, _radius);
    }

    void OnBecameInvisible()
    {
        GameObject.Destroy(this.gameObject);
    }
}
