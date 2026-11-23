using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AnimacaoPersonagem : MonoBehaviour
{
    [Header("Camadas")]
    [SerializeField] private SpriteRenderer corpo;
    [SerializeField] private SpriteRenderer espada;

    [Header("Parado (Idle)")]
    [SerializeField] private Sprite[] corpoParado;
    [SerializeField] private Sprite[] espadaParado;
    [SerializeField] private int colunasParado = 12;
    [SerializeField] private float fpsParado = 8f;

    [Header("Andando (Walk)")]
    [SerializeField] private Sprite[] corpoAndando;
    [SerializeField] private Sprite[] espadaAndando;
    [SerializeField] private int colunasAndando = 6;
    [SerializeField] private float fpsAndando = 10f;

    [Header("Morte (Death)")]
    [SerializeField] private Sprite[] corpoMorte;
    [SerializeField] private int colunasMorte = 7;
    [SerializeField] private float fpsMorte = 8f;

    // linhas da spritesheet: 0 = frente, 1 = esquerda, 2 = direita, 3 = costas
    private const int Frente = 0, Esquerda = 1, Direita = 2, Costas = 3;

    private Rigidbody2D fisica;
    private int linha = Frente;
    private int quadro;
    private float cronometro;
    private bool estavaAndando;
    private bool morto;

    // quanto tempo a animação de morte leva: 7 quadros a 8 por segundo = 0,875 s
    public float DuracaoMorte => colunasMorte / fpsMorte;

    void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();

        // garante a ordem certa dos quadros, independente de como foram arrastados
        OrdenarPorPosicao(corpoParado);
        OrdenarPorPosicao(espadaParado);
        OrdenarPorPosicao(corpoAndando);
        OrdenarPorPosicao(espadaAndando);
        OrdenarPorPosicao(corpoMorte);
    }

    void Update()
    {
        if (morto)
        {
            AnimarMorte();
            return;
        }

        Vector2 velocidade = fisica.linearVelocity;
        bool andando = velocidade.sqrMagnitude > 0.01f;

        // só atualiza a direção quando está andando,
        // assim quando para ele continua olhando pro último lado
        if (andando)
        {
            linha = CalcularLinha(velocidade);
        }

        // trocou entre parado e andando? recomeça a animação do quadro 0
        if (andando != estavaAndando)
        {
            quadro = 0;
            cronometro = 0f;
            estavaAndando = andando;
        }

        Sprite[] spritesCorpo = andando ? corpoAndando : corpoParado;
        Sprite[] spritesEspada = andando ? espadaAndando : espadaParado;
        int colunas = andando ? colunasAndando : colunasParado;
        float fps = andando ? fpsAndando : fpsParado;

        // primeiro quadro da linha e quantos quadros ela tem
        // (a linha de costas do Idle só tem 4, por isso o Min)
        int inicio = linha * colunas;
        int quantidade = Mathf.Min(colunas, spritesCorpo.Length - inicio);

        cronometro += Time.deltaTime;
        if (cronometro >= 1f / fps)
        {
            cronometro -= 1f / fps;
            quadro = (quadro + 1) % quantidade;
        }

        corpo.sprite = spritesCorpo[inicio + quadro];
        espada.sprite = spritesEspada[inicio + quadro];

        // olhando pra esquerda ou pra cima, a espada fica atrás do corpo
        bool espadaAtras = linha == Esquerda || linha == Costas;
        espada.sortingOrder = espadaAtras ? corpo.sortingOrder - 1 : corpo.sortingOrder + 1;
    }

    public void TocarMorte()
    {
        morto = true;
        quadro = 0;
        cronometro = 0f;
        espada.gameObject.SetActive(false);
    }

    private void AnimarMorte()
    {
        // usa a mesma linha (direção) em que o personagem estava olhando
        int inicio = linha * colunasMorte;
        corpo.sprite = corpoMorte[inicio + quadro];

        // chegou no último quadro? para aqui, caído (sem loop)
        if (quadro >= colunasMorte - 1)
        {
            return;
        }

        cronometro += Time.deltaTime;
        if (cronometro >= 1f / fpsMorte)
        {
            cronometro -= 1f / fpsMorte;
            quadro++;
        }
    }

    private int CalcularLinha(Vector2 v)
    {
        // o eixo com o movimento mais forte decide a direção
        if (Mathf.Abs(v.x) > Mathf.Abs(v.y))
        {
            return v.x < 0 ? Esquerda : Direita;
        }
        return v.y < 0 ? Frente : Costas;
    }

    // ordena: linha de cima primeiro, e em cada linha da esquerda pra direita
    private void OrdenarPorPosicao(Sprite[] sprites)
    {
        System.Array.Sort(sprites, (a, b) =>
        {
            if (a.rect.y != b.rect.y) return b.rect.y.CompareTo(a.rect.y);
            return a.rect.x.CompareTo(b.rect.x);
        });
    }
}