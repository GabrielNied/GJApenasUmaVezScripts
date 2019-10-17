using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


    public Image[] attacks = new Image[4];
    public Image imagemAux;
    public Image[] aux = new Image[4];
    
    [SerializeField]
    public Sprite spriteNull;

	void Start () {
		
	}
	
	
	void Update () {
        UpdateAttacksUI();

    }

    public void UpdateAttacksUI()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    attacks[0].color = Color.white;
        //    attacks[0].sprite = attacks[1].sprite;
        //    attacks[2] = attacks[3];


        //    attacks[1].sprite = attacks[2].sprite;
        //    attacks[1] = attacks[2];

        //    attacks[2].sprite = attacks[3].sprite;
        //    attacks[0] = attacks[1];


        //    attacks[3].sprite = spriteNull;

        //    if (attacks[0].sprite == spriteNull)
        //    {
        //        imagemAux.enabled = false;
        //    }

        //    attacks[0].color = Color.white;
        //    attacks[0].sprite = aux[1].sprite;
        //    attacks[0].name = aux[1].name;

        //    attacks[1].sprite = aux[2].sprite;
        //    attacks[1].name = aux[2].name;

        //    attacks[2].sprite = aux[3].sprite;
        //    attacks[2].name = aux[3].name;

        //    attacks[3].sprite = spriteNull;

            //if(attacks[0].sprite == spriteNull)
            //{
            //    imagemAux.enabled = false;
            //}








       // }
    }
}
