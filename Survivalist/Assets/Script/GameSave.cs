using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]

public class GameJson
{
    public int water;
    public int food;
    public int wood;
    public int leather;
    public int straw;
    public int leaf;
    public int rock;

    public int day;
    //public int log;

    public float health;
    public float energy;
    public float hunger;
    public float thirst;
    public float hardest;

    public int used_food; // 먹은 음식의 양 
    public int used_water; // 마신 물의 양
    public int used_rock; // 사용한 돌의 양

    public int getwater_count; // 바닷물을 정화한 횟수
    public int forgage_count; // 파밍을 한 횟수
    public int hunt_count; // 사냥을 한 횟수
    public int material_count; // 재료는 찾아떠난 횟수

    public int attack_count; // 습격에 당한 횟수


}


public class GameSave : MonoBehaviour
{
    static GameSave instance = null;

    GameManager gm;
    TextSync ts;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    private void Start()
    {
        gm = GameManager.Instance;
        ts = TextSync.Instance;
    }


    public static GameSave Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public GameJson jsclass = new GameJson();


    public void Load()
    {
        // JSON 파일 특정
        string path = Application.dataPath + "/Save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            jsclass = JsonUtility.FromJson<GameJson>(json);

            gm.water = jsclass.water;
            gm.food = jsclass.food;
            gm.wood = jsclass.wood;
            gm.leather = jsclass.leather;
            gm.straw = jsclass.straw;
            gm.leaf = jsclass.leaf;
            gm.rock = jsclass.rock;

            gm.day = jsclass.day;
            //gm.log_index = jsclass.log;

            ts.Health.value = jsclass.health;
            ts.Energy.value = jsclass.energy;
            ts.Hunger.value = jsclass.hunger;
            ts.Moist.value = jsclass.thirst;
            ts.Hardest.value = jsclass.hardest;

            gm.used_food = jsclass.used_food;
            gm.used_water = jsclass.used_water;
            gm.used_rock = jsclass.used_rock;

            gm.getwater_count = jsclass.getwater_count;
            gm.forgage_count = jsclass.forgage_count;
            gm.hunt_count = jsclass.hunt_count;
            gm.material_count = jsclass.material_count;

            gm.attack_count = jsclass.attack_count;
        }
        else
        {
            Debug.Log("로딩할 파일이 없어요");
        }
    }

    public void Save()
    {
        jsclass.water = gm.water;
        jsclass.food = gm.food;
        jsclass.wood = gm.wood;
        jsclass.leather = gm.leather;
        jsclass.straw = gm.straw;
        jsclass.leaf = gm.leaf;
        jsclass.rock = gm.rock;

        jsclass.day = gm.day;
        //jsclass.log = gm.log_index;

        jsclass.health = ts.Health.value;
        jsclass.energy = ts.Energy.value;
        jsclass.hunger = ts.Hunger.value;
        jsclass.thirst = ts.Moist.value;
        jsclass.hardest = ts.Hardest.value;

        jsclass.used_food = gm.used_food;
        jsclass.used_water = gm.used_water;
        jsclass.used_rock = gm.used_rock;

        jsclass.getwater_count = gm.getwater_count;
        jsclass.forgage_count = gm.forgage_count;
        jsclass.hunt_count = gm.hunt_count;
        jsclass.material_count = gm.material_count;

        jsclass.attack_count = gm.attack_count;

        //Json string 으로 변환
        string json = JsonUtility.ToJson(jsclass, true);
        //json 데이터를 저장할 위치
        string path = Application.dataPath + "/Save.json";

        //Path 에 Json 데이터를 저장
        File.WriteAllText(path, json);
        Debug.Log("Saved");
    }

    private void Update()
    {
        
    }

    public bool Check()
    {
        // JSON 파일 특정
        string path = Application.dataPath + "/Save.json";
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Delete()
    {
        File.Delete(Application.dataPath + "/Save.json");
        Debug.Log("세이브 삭제");
    }
}
