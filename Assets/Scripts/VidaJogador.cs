using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VidaJogador : MonoBehaviour
{
    [SerializeField] private int vidasMaximas = 3;
    [SerializeField] private Image[] coracoes;
    [SerializeField] private Sprite coracaoCheio;
    [SerializeField] private Sprite coracaoVazio;

    [Header("Invencibilidade")]
    [SerializeField] private float tempoInvencivel = 1.5f;
    [SerializeField] private float intervaloPiscar = 0.1f;
    [SerializeField] private SpriteRenderer corpo;

    public int Vidas { get; private set; }
    public bool Invencivel { get; private set; }

    void Awake()
    {
        Vidas = vidasMaximas;
        AtualizarCoracoes();
    }

    // devolve true se esse golpe matou o jogador
    public bool TomarDano()
    {
        Vidas--;
        AtualizarCoracoes();

        if (Vidas <= 0)
        {
            return true;
        }

        StartCoroutine(Piscar());
        return false;
    }

    private void AtualizarCoracoes()
    {
        // com 2 vidas: corações 0 e 1 cheios, coração 2 vazio
        for (int i = 0; i < coracoes.Length; i++)
        {
            coracoes[i].sprite = i < Vidas ? coracaoCheio : coracaoVazio;
        }
    }

    private IEnumerator Piscar()
    {
        Invencivel = true;
        float tempo = 0f;

        while (tempo < tempoInvencivel)
        {
            corpo.enabled = !corpo.enabled;   // liga/desliga o desenho
            yield return new WaitForSeconds(intervaloPiscar);
            tempo += intervaloPiscar;
        }

        corpo.enabled = true;                 // garante que termina visível
        Invencivel = false;
    }
}