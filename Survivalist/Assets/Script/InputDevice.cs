using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDevice : MonoBehaviour
{
    TextSync ts;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        ts = TextSync.Instance;
        gm = GameManager.Instance;


    }

    // Update is called once per frame
    void Update()
    {
        // Input 영역
        if (!gm.isCooltime) // 쿨타임 안 걸렸으면
        {

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) // 물 얻으러
            {
                if (ts.Energy.value >= gm.act_getwater_min[0] && ts.Hunger.value >= gm.act_getwater_min[1] && ts.Moist.value >= gm.act_getwater_min[2] && ts.Hardest.value <= gm.act_getwater_min[3])
                {
                    //Debug.Log("물 얻으러");
                    gm.isGetsWater = true;
                    gm.isCooltime = true;
                    gm.cooltime = gm.act_cooltime[0];
                }
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) // 파밍
            {
                if (ts.Energy.value >= gm.act_goforgage_min[0] && ts.Hunger.value >= gm.act_goforgage_min[1] && ts.Moist.value >= gm.act_goforgage_min[2] && ts.Hardest.value <= gm.act_goforgage_min[3])
                {
                    //Debug.Log("파밍");
                    gm.isForage = true;
                    gm.isCooltime = true;
                    gm.cooltime = gm.act_cooltime[1];
                }
            }

            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) // 사냥
            {
                if (ts.Energy.value >= gm.act_hunt_min[0] && ts.Hunger.value >= gm.act_hunt_min[1] && ts.Moist.value >= gm.act_hunt_min[2] && ts.Hardest.value <= gm.act_hunt_min[3])
                {
                    //Debug.Log("사냥");
                    gm.isHunt = true;
                    gm.isCooltime = true;
                    gm.cooltime = gm.act_cooltime[2];
                }
            }

            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) // 먹는다
            {
                //if (food > 0 && ts.Moist.value >= act_eatfood_min[2] && ts.Hunger.value < 100f) // 음식이 한단위 이상 있는지, 
                //{
                if (gm.food > 0 && ts.Hunger.value < 100f)
                {
                    //Debug.Log("먹는다");
                    gm.isEat = true;
                    gm.isCooltime = true;
                    gm.cooltime = gm.act_cooltime[3];
                }
                //}
            }

            else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) // 콸콸
            {
                if (gm.water > 0 && ts.Moist.value < 100f) // 물이 최소한 만큼 있는지, 갈증이 이미 해소되었는지
                {
                    //Debug.Log("콸콸");
                    gm.isDrink = true;
                    gm.isCooltime = true;
                    gm.cooltime = gm.act_cooltime[4];
                }
            }

            //else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) // 재료
            //{
            //    if (ts.Energy.value >= act_material_min[0] && ts.Hunger.value >= act_material_min[1] && ts.Moist.value >= act_material_min[2])
            //    {
            //        isMaterial = true;
            //        isCooltime = true;
            //        cooltime = act_cooltime[5];
            //    }

            //}

            else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) // 취침
            {
                //if(ts.Hunger.value >= act_sleep_min[1] && ts.Moist.value >= act_sleep_min[2] && ts.Hardest.value > 0)
                //{
                if (ts.Hardest.value > 0)
                {
                    //Debug.Log("취침");
                    gm.isSleep = true;
                    gm.isCooltime = true;
                    gm.cooltime = gm.act_cooltime[6];
                }
                //}
            }

        } // Input 영역 if (!isCooltime)
    }
}
