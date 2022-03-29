using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    GameManager gm;
    TextSync ts;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        ts = TextSync.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl)) // 왼쪽 컨트롤 누르고 누르면 아이템 증가
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                gm.food += 100;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                gm.water += 100;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                gm.wood += 100;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                gm.leather += 100;
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                gm.leaf += 100;
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                gm.rock += 100;
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                gm.straw += 100;
            }

            // 수치
            if (Input.GetKeyDown(KeyCode.A))
            {
                ts.Health.value += 10;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                ts.Energy.value += 10;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                ts.Hunger.value += 10;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                ts.Moist.value += 10;
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                ts.Hardest.value -= 10;
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                gm.food = 0;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                gm.water = 0;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                gm.wood = 0;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                gm.leather = 0;
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                gm.leaf = 0;
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                gm.rock = 0;
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                gm.straw = 0;
            }

            // 수치
            if (Input.GetKeyDown(KeyCode.A))
            {
                ts.Health.value -= 10;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                ts.Energy.value -= 10;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                ts.Hunger.value -= 10;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                ts.Moist.value -= 10;
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                ts.Hardest.value += 10;
            }

        } // else

        



    }
}
