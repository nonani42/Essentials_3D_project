using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class ObjManager : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _snowball;
    [SerializeField] Transform _playerPoint;
    [SerializeField] Transform _enemyPoint;
    static Dictionary<Tags, GameObject> _prefabsDic;
    static List<GameObject> _spawnedObjectsList;

    public static Dictionary<Tags, GameObject> PrefabsList { get => _prefabsDic; }
    public static List<GameObject> SpawnedObjectsList { get => _spawnedObjectsList; }
    private void Awake()
    {
        _prefabsDic = new Dictionary<Tags, GameObject>();
        FillPrefabs();
        _spawnedObjectsList = new List<GameObject>();
        DoOnStart();
    }

    private void FillPrefabs()// сделать считывание потоком из папки?
    {
        _prefabsDic.Add(Tags.player, _player);
        _prefabsDic.Add(Tags.enemy, _enemy);
        _prefabsDic.Add(Tags.snowball, _snowball);
    }

    public void Spawn(Tags tag, Transform position)
    {
        if (CheckTag(tag))
        {
            var temp = Instantiate(_prefabsDic[tag], position);
            temp.transform.parent = null;
            AddObject(temp);
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

    private bool CheckTag(Tags tag)
    {
        return _prefabsDic.ContainsKey(tag);
    }
    private void DoOnStart()
    {
        Spawn(Tags.player, _playerPoint);
        Spawn(Tags.enemy, _enemyPoint);
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

}
public enum Tags
{
    player,
    enemy,
    snowball
}
