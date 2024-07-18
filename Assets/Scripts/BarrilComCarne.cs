using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrilComCarne : MonoBehaviour, IInteractable
{
    [SerializeField] private Mesh ModeloBauFechado;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){

    }
    public void OnInteract()
	{
		Debug.Log("Usuário interagiu com objeto Barril com Carne, mas não passou como argumento si mesmo");
		
		
	}
    public void OnInteract(object sender, EventArgs e)
	{
        Player player = (Player)sender;
		Debug.Log("Usuário interagiu com objeto Barril com Carne");
		player.inventario.interagirComDict("Carne",1);
		Debug.Log("Inventário do jogador tem " + player.inventario.mapaInventario["Carne"]);
        var meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = ModeloBauFechado;
	}
}
