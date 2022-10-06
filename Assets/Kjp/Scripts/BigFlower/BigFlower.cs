using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigFlower : MonoBehaviour
{
    [SerializeField]
    private GameObject _root;
    [SerializeField]
    private int _rootCnt = 4;

    private List<GameObject> _rootList = new List<GameObject>();
    public List<GameObject> RootList
    {
        get => _rootList;
    }

    private AIBrain aiBrain;

    private void Awake()
    {
        aiBrain = GetComponent<AIBrain>();

        for (int i = 0; i < _rootCnt; i++)
        {
            GameObject root = Instantiate(_root, transform);
            float rot = ((float)360 / _rootCnt) * (i + 1);
            Quaternion q = Quaternion.Euler(0, 0, rot);
            root.transform.rotation = q;
            _rootList.Add(root);
        }
    }

    public void AddRoot(int cnt = 1)
    {
        if(cnt == 1)
        {
            GameObject root = Instantiate(_root, transform);
            _rootList.Add(root);
        }
        else
        {
            for(int i = 0; i < cnt; i++)
            {
                GameObject root = Instantiate(_root, transform);
                _rootList.Add(root);
            }
        }

        for (int i = 0; i < _rootList.Count; i++)
        {
            //_rootList[i].transform.DOKill();
            float rot = ((float)360 / _rootList.Count) * (i + 1);
            Quaternion q = Quaternion.Euler(0, 0, rot);
            _rootList[i].transform.rotation = q;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddRoot();
            //aiBrain.GetCurrentState().OnStateEnter();
        }
    }
}
