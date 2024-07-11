using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BauComMaca : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){

    }
    public void OnInteract()
	{
		Debug.Log("Usuário interagiu com objeto Bau com Maçã, mas não passou como argumento si mesmo");
		
		
	}
    public void OnInteract(object sender, EventArgs e)
	{
        Player player = (Player)sender;
		Debug.Log("Usuário interagiu com objeto Bau com Maçã");
		player.inventario.interagirComDict("Maçã",1);
		Debug.Log("Inventário do jogador tem " + player.inventario.mapaInventario["Maçã"]);
	}
}
