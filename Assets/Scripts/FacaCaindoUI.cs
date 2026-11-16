using UnityEngine;

public class FacaCaindoUI : MonoBehaviour
{
    private RectTransform rect;
    private float velocidade;
    private float limiteInferior;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Chamado pelo gerador logo depois de criar a faca
    public void Configurar(float novaVelocidade, float novoLimite)
    {
        velocidade = novaVelocidade;
        limiteInferior = novoLimite;
    }

    void Update()
    {
        // desce "velocidade" unidades por segundo
        rect.anchoredPosition += Vector2.down * velocidade * Time.deltaTime;

        // saiu da tela? apaga, senão as facas se acumulam na memória pra sempre
        if (rect.anchoredPosition.y < limiteInferior)
        {
            Destroy(gameObject);
        }
    }
}