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

    public GameObject oHealth; // 건강 slider 오브젝트
    public GameObject oEnergy; // 에너지 slider 오브젝트
    public GameObject oHunger; // 포만감 slider 오브젝트
    public GameObject oMoist; // 수분감 slider 오브젝트
    public GameObject oHardest; // 피로도 slider 오브젝트

    public Color minus; // 수치 딸리면 행동 txt 요 컬러로 변경될꺼임
    public Color current; // 정상수치일때 복원

    public Color hardesttxt_color; // 행동별로 능률 저하에 해당될경우 행동 텍스트 색 조정

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

        // 게임 시작할때 수치 기본값 셋팅
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
            else if(gm.act_hardest_decline[0] <= Hardest.value) // 행동별로 능률저하에 해당될경우 색 조정
            {
                act_getwater_txt.color = hardesttxt_color;
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
            else if (gm.act_hardest_decline[1] <= Hardest.value) // 행동별로 능률저하에 해당될경우 색 조정
            {
                act_forgage_txt.color = hardesttxt_color;
            }
            else
            {
                act_forgage_txt.color = current; // 원상 복귀
            }

            // 사냥
            if (gm.act_hunt_min[0] > Energy.value | gm.act_hunt_min[1] > Hunger.value | gm.act_hunt_min[2] > Moist.value | gm.act_hunt_min[3] < Hardest.value)
            {
                act_hunt_txt.color = minus; // 투명화
            }else if(gm.act_hardest_decline[2] <= Hardest.value) // 행동별로 능률저하에 해당될경우 색 조정
            {
                act_hunt_txt.color = hardesttxt_color;
            }
            else
            {
                act_hunt_txt.color = current; // 원상 복귀
            }


            // Slider 수치가 일정 이하 일경우 깜빡임
            if (Health.value <= gm.lowvalue) // 생명 수치
            {
                Blinking.Instance.SliderBlink(oHealth);
            }
            else
            {
                oHealth.SetActive(true);
            }
            if(Energy.value <= gm.lowvalue) // 에너지 수치
            {
                Blinking.Instance.SliderBlink(oEnergy);
            }
            else
            {
                oEnergy.SetActive(true);
            }
            if(Hunger.value <= gm.lowvalue) // 포만감 수치
            {
                Blinking.Instance.SliderBlink(oHunger);
            }
            else
            {
                oHunger.SetActive(true);
            }
            if(Moist.value <= gm.lowvalue) // 수분감 수치
            {
                Blinking.Instance.SliderBlink(oMoist);
            }
            else
            {
                oMoist.SetActive(true);
            }
            if(Hardest.value >= (100 - gm.lowvalue)) // 피로도 수치
            {
                Blinking.Instance.SliderBlink(oHardest);
            }
            else
            {
                oHardest.SetActive(true);
            }

            // 밥먹고 물먹을때 음식 물 없으면, 포만감, 수분감 95이상일 경우 알파값 조정
            if(gm.food < 1 && Hunger.value > 95f)
            {
                act_eatfood_txt.color = minus;
            }
            else
            {
                act_eatfood_txt.color = current;
            }
            if(gm.water < 1 && Moist.value > 95f)
            {
                act_drink_txt.color = minus;
            }
            else
            {
                act_drink_txt.color = current;
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
            "\n피로도: " + Hardest.value + 
            "\n먹은 음식: " + gm.used_food + 
            "\n마신 물: " + gm.used_water + 
            "\n사용한 돌: " + gm.used_rock +
            "\n정화: " + gm.getwater_count + 
            "\n파밍: " + gm.forgage_count + 
            "\n사냥: " + gm.hunt_count + 
            "\n재료: " + gm.material_count +
            "\n습격: " + gm.attack_count;
    }
}
