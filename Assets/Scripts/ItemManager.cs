using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {
    public Item item0 = new Item();
    public Text textoPreco;
    public Text textoGanhos;
    public InputField entradaDeNumeros;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        textoPreco.text = "Preço: " + item0.preco.ToFormattedString();
        textoGanhos.text = "Ganhos: " + item0.valorGerado;
	}

    public void Comprar()
    {
        item0.AumentarPreco(1);
    }

    public void MostrarNumero()
    {
        item0.preco = new CientificNumber(int.Parse(entradaDeNumeros.text));
        textoPreco.text = item0.preco.ToFormattedString();
    }
}
