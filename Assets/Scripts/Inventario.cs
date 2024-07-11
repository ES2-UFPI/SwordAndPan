using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public Dictionary<String, int> mapaInventario = new Dictionary<string, int>();
    // Start is called before the first frame update
    void Start()
    {
        //TODO operação pra carregar inventario de um save
        //operação para inicializar os inventários com tamanhos corretos de items
        mapaInventario.Add("Maçã", 0);
        mapaInventario.Add("Carne", 0);
        mapaInventario.Add("Banana", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void interagirComDict(String nome, int quantia){
        //quantia pode ser negativa
        mapaInventario[nome] = mapaInventario[nome] + quantia;
    }   
}
