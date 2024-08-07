using UnityEngine;

public class ColorChangingObject : MonoBehaviour, IInteractable
{
	// Cores possíveis para o objeto
	public Color[] colors;

	// Referência para o Renderer do objeto
	private Renderer _renderer;

	// Índice da cor atual
	private int currentColorIndex = 0;

	void Start()
	{
		// Obtém o componente Renderer do objeto
		_renderer = GetComponent<Renderer>();

		// Define a cor inicial do objeto
		SetColor(currentColorIndex);
	}

	// Método para mudar a cor do objeto
	private void SetColor(int colorIndex)
	{
		// Verifica se o índice está dentro do intervalo de cores disponíveis
		if (colorIndex >= 0 && colorIndex < colors.Length)
		{
			// Define a cor do objeto
			_renderer.material.color = colors[colorIndex];
		}
	}

	// Implementação do método Interact da interface IInteractable
	public void OnInteract()
	{
		Debug.Log("Oi");
		// Muda para a próxima cor na lista de cores
		currentColorIndex = (currentColorIndex + 1) % colors.Length;
		SetColor(currentColorIndex);
	}
	
}
