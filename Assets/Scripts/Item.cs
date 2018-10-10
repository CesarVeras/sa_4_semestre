using UnityEngine;

public class Item
{
    public string nome = "ItemGenerico";
    public CientificNumber preco = new CientificNumber(10);
    public float progressaoPreco = 1.05f;
    public float progressaoGanhos = 0.5f;
    public int valorGerado = 5;
    public string precoParaMostrar;

    public void AumentarPreco(int quantidade)
    {
        for (int i = 0; i < quantidade; i++)
        {
            preco ^= progressaoPreco;
            valorGerado += Mathf.CeilToInt(valorGerado * progressaoGanhos);
        }
    }
}