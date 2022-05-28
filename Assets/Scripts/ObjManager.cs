using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _snowball;
    [SerializeField] GameObject _suriken;
    [SerializeField] GameObject _pickupHeal;

    [SerializeField] Transform _playerPoint;
    [SerializeField] Transform[] _enemyPoint;
    [SerializeField] Transform[] _pickupPoint;


    static Dictionary<Tags, GameObject> _prefabsDic;
    static List<GameObject> _spawnedObjectsList;
    static Transform[][] _patrolPointsFlowerArr;
    static Transform[][] _patrolPointsTreeArr;
    static int _countFlower;
    static int _countTree;

    public static Dictionary<Tags, GameObject> PrefabsDic { get => _prefabsDic; }
    public static List<GameObject> SpawnedObjectsList { get => _spawnedObjectsList; }

    private void Awake()
    {
        _countFlower = 0;
        _countTree = 0;
        _prefabsDic = new Dictionary<Tags, GameObject>();
        FillPrefabs();
        _spawnedObjectsList = new List<GameObject>();
        FillPatrolPoints();
        DoOnStart();
    }

    private void FillPrefabs()// сделать считывание потоком из папки?
    {
        _prefabsDic.Add(Tags.player, _player);
        _prefabsDic.Add(Tags.enemyFlower, _enemy);
        _prefabsDic.Add(Tags.snowball, _snowball);
        _prefabsDic.Add(Tags.suriken, _suriken);
        _prefabsDic.Add(Tags.heal, _pickupHeal);

    }

    public void Spawn(Tags tag, Transform position)
    {
        if (CheckTag(tag))
        {
            var temp = Instantiate(_prefabsDic[tag], position);
            temp.transform.parent = null;
            AddObject(temp);
            SetIndexForEnemies(tag);
        }
        else
        {
            Debug.Log("No prefab in the dictionary with this tag");
        }
    }

    private void AddObject(GameObject temp)
    {
        _spawnedObjectsList.Add(temp);
    }

    private void SetIndexForEnemies(Tags tag)
    {
        switch (tag)
        {
            case Tags.enemyFlower:
                _spawnedObjectsList.Last().GetComponent<Enemy>().Index = _countFlower++;
                break;
            case Tags.enemyTree:
                _spawnedObjectsList.Last().GetComponent<Enemy>().Index = _countTree++;
                break;
        }
    }

    private bool CheckTag(Tags tag)
    {
        return _prefabsDic.ContainsKey(tag);
    }

    private void DoOnStart()
    {
        Spawn(Tags.player, _playerPoint);
        foreach(var point in _enemyPoint)
        {
            Spawn(Tags.enemyFlower, point);
        }
        foreach (var point in _pickupPoint)
        {
            Spawn(Tags.heal, point);
        }
    }

    public static GameObject FindPlayer()
    {
        GameObject temp = null;
        foreach (var el in SpawnedObjectsList)
        {
            if (el.GetComponent<Player>() != null)
            {
                temp = el;
            }
        }
        return temp;
    }

    public Transform[] GetPatrolPoints(Tags tag, int index)
    {
        switch (tag)
        {
            case Tags.enemyFlower:
                return _patrolPointsFlowerArr[index];
            case Tags.enemyTree:
                return _patrolPointsTreeArr[index];
            default: 
                return new Transform[0];
        }
    }

    private void FillPatrolPoints()
    {
        Transform[][] temp;
        GameObject tempWaypoints;
        int lengthFirstLevel;
        int lengthSecondLevel;

        for (int i = 0; i < Enum.GetNames(typeof(WaypointsType)).Length; i++)
        {
            tempWaypoints = GameObject.Find(((WaypointsType)i).ToString());
            if(tempWaypoints == null) 
            { 
                return;
            }
            lengthFirstLevel = tempWaypoints.transform.childCount;
            temp = new Transform[lengthFirstLevel][];

            for (int j = 0; j < lengthFirstLevel; j++)
            {
                lengthSecondLevel = tempWaypoints.transform.GetChild(j).transform.childCount;

                temp[j] = new Transform[lengthSecondLevel];
                for (int k = 0; k < lengthSecondLevel; k++)
                {
                    temp[j][k] = tempWaypoints.transform.GetChild(j).transform.GetChild(k);
                }
            }
            switch ((WaypointsType)i)
            {
                case WaypointsType.FlowerWaypoints:
                    _patrolPointsFlowerArr = temp;
                    break;
                case WaypointsType.TreeWaypoints:
                    _patrolPointsTreeArr = temp;
                    break;
                default:
                    Debug.Log("No assigned array for this type in WaypointsType");
                    break;
            }
        }
    }
}

public enum Tags
{
    player,
    enemyFlower,
    enemyTree, 
    enemyBush,
    snowball,
    suriken,
    heal,
    shield,
    spareAmmo
}

public enum WaypointsType
{
    FlowerWaypoints,
    TreeWaypoints
}