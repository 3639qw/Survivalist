using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSync : MonoBehaviour
{
    static public TextSync instance = null;

    GameManager gm;

    public Text foodtxt; 
    public Text watertxt;
    public Text woodtxt;
    public Text leathertxt;
    public Text leaftxt;
    public Text rocktxt;
    public Text strawtxt;

    public Text daytxt;
    public Text act_txt;

    public Text cheattxt;

    public Text act_getwater_txt; // 활동 물 txt
    public Text act_forgage_txt; // 활동 파밍 txt
    public Text act_hunt_txt; // 활동 사냥 txt
    public Text act_eatfood_txt; // 활동 식사 txt
    public Text act_drink_txt; // 활동 음료 txt
    public Text act_material_txt; // 활동 재료수집 txt

    /*(저장대상)*/ public Slider Health;
    /*(저장대상)*/ public Slider Energy;
    /*(저장대상)*/ public Slider Hunger;
    /*(저장대상)*/ public Slider Moist;
    /*(저장대상)*/ public Slider Hardest;

    public GameObject oHealth;
    public GameObject oEnergy;
    public GameObject oHunger;
    public GameObject oMoist;
    public GameObject oHardest;

    public Color minus; // 수치 딸리면
    public Color current; // 정상

    public static TextSync Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;

        Health.value = 100;
        Energy.value = 100;
        Hunger.value = 100;
        Moist.value = 90;
        Hardest.value = 0;


    }

    // Update is called once per frame
    void Update()
    {

        if (!gm.isCooltime)
        {

            // 수치가 최소량 보다 미달하면 활동 텍스트 알파값 조정

            // 물
            if (gm.act_getwater_min[0] > Energy.value | gm.act_getwater_min[1] > Hunger.value | gm.act_getwater_min[2] > Moist.value | gm.act_getwater_min[3] < Hardest.value)
            {
                act_getwater_txt.color = minus; // 투명화
            }
            else
            {
                act_getwater_txt.color = current; // 원상 복귀
            }

            // 파밍
            if (gm.act_goforgage_min[0] > Energy.value | gm.act_goforgage_min[1] > Hunger.value | gm.act_goforgage_min[2] > Moist.value | gm.act_goforgage_min[3] < Hardest.value)
            {
                act_forgage_txt.color = minus; // 투명화
            }
            else
            {
                act_forgage_txt.color = current; // 원상 복귀
            }

            // 사냥
            if (gm.act_hunt_min[0] > Energy.value | gm.act_hunt_min[1] > Hunger.value | gm.act_hunt_min[2] > Moist.value | gm.act_hunt_min[3] < Hardest.value)
            {
                act_hunt_txt.color = minus; // 투명화
            }
            else
            {
                act_hunt_txt.color = current; // 원상 복귀
            }


            // Slider 수치가 일정 이하 일경우 깜빡임
            if (Health.value < gm.lowvalue)
            {
                Blinking.Instance.SliderBlink(oHealth);
            }
            else
            {
                oHealth.SetActive(true);
            }
            if(Energy.value < gm.lowvalue)
            {
                Blinking.Instance.SliderBlink(oEnergy);
            }
            else
            {
                oEnergy.SetActive(true);
            }
            if(Hunger.value < gm.lowvalue)
            {
                Blinking.Instance.SliderBlink(oHunger);
            }
            else
            {
                oHunger.SetActive(true);
            }
            if(Moist.value < gm.lowvalue)
            {
                Blinking.Instance.SliderBlink(oMoist);
            }
            else
            {
                oMoist.SetActive(true);
            }
            if(Hardest.value > (100 - gm.lowvalue))
            {
                Blinking.Instance.SliderBlink(oHardest);
            }
            else
            {
                oHardest.SetActive(true);
            }
        }
        else
        {
            oHealth.SetActive(true);
            oEnergy.SetActive(true);
            oHunger.SetActive(true);
            oMoist.SetActive(true);
            oHardest.SetActive(true);

        }



        // 자원 txt 출력
        watertxt.text = gm.water.ToString();
        foodtxt.text = gm.food.ToString();
        woodtxt.text = gm.wood.ToString();
        leathertxt.text = gm.leather.ToString();
        strawtxt.text = gm.straw.ToString();
        leaftxt.text = gm.leaf.ToString();
        rocktxt.text = gm.rock.ToString();

        // 생존일수 txt 출력
        daytxt.text = "생존 " + gm.day.ToString() + "일차";

        // 지금 하고 있는 일
        act_txt.text = gm.nowact;


        cheattxt.text = 
            "생명: " + Health.value + 
            "\n체력: " + Energy.value + 
            "\n포만감: " + Hunger.value + 
            "\n갈증: " + Moist.value + 
            "\n피로도: " + Hardest.value;
    }
}
