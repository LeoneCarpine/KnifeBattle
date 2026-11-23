using UnityEngine;

public class CombateJogador : MonoBehaviour
{
    [SerializeField] private GerenciadorPartida partida;
    [SerializeField] private string nome = "Jogador 1";
    [SerializeField] private Color corNome = Color.white;

    public string Nome => nome;
    public Color CorNome => corNome;
    public float DuracaoMorte => GetComponent<AnimacaoPersonagem>().DuracaoMorte;

    public ArmaJogador Arma { get; private set; }

    void Awake()
    {
        Arma = GetComponent<ArmaJogador>();
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        VerificarContato(colisao);
    }

    void OnCollisionStay2D(Collision2D colisao)
    {
        VerificarContato(colisao);
    }

    private void VerificarContato(Collision2D colisao)
    {
        if (partida == null)
        {
            return;
        }

        CombateJogador adversario =
            colisao.gameObject.GetComponent<CombateJogador>();

        if (adversario == null)
        {
            return;
        }

        partida.ResolverContato(this, adversario);
    }

    public void Parar()
    {
        GetComponent<MovimentoJogador>().enabled = false;

        Rigidbody2D corpo = GetComponent<Rigidbody2D>();
        corpo.linearVelocity = Vector2.zero;
        corpo.simulated = false;

        Arma.RemoverFaca();
    }

        public void Morrer()
    {
        Parar();
        GetComponent<AnimacaoPersonagem>().TocarMorte();
    }
}