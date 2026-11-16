using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AnimacaoSpriteUI : MonoBehaviour
{
    [SerializeField] private Sprite[] quadros;              // os quadros da animação, em ordem
    [SerializeField] private float quadrosPorSegundo = 8f;  // velocidade da animação

    private Image imagem;
    private int quadroAtual;
    private float cronometro;

    void Awake()
    {
        imagem = GetComponent<Image>();
    }

    void Update()
    {
        if (quadros.Length == 0)
        {
            return;
        }

        cronometro += Time.deltaTime;                 // soma o tempo que passou desde o último frame
        float duracaoQuadro = 1f / quadrosPorSegundo; // 8 por segundo = cada quadro dura 0,125 s

        if (cronometro >= duracaoQuadro)
        {
            cronometro -= duracaoQuadro;
            quadroAtual = (quadroAtual + 1) % quadros.Length; // avança e volta pro 0 no fim
            imagem.sprite = quadros[quadroAtual];
        }
    }
}