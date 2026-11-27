using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GerenciadorPartida : MonoBehaviour
{
    [SerializeField] private float pausaAntesDoResultado = 0.5f;
    [SerializeField] private GameObject painelResultado;
    [SerializeField] private TMP_Text textoVencedor;
    private bool partidaEncerrada;
    public bool Encerrada => partidaEncerrada;

    void Awake()
    {
        partidaEncerrada = false;
        painelResultado.SetActive(false);
    }
    public void ResolverContato(
        CombateJogador jogadorA,
        CombateJogador jogadorB)
    {
        if (partidaEncerrada)
        {
            return;
        }

        bool aTemFaca = jogadorA.Arma.TemFaca;
        bool bTemFaca = jogadorB.Arma.TemFaca;

        if (aTemFaca && bTemFaca)
        {
            jogadorA.Arma.RemoverFaca();
            jogadorB.Arma.RemoverFaca();

            Debug.Log("As facas se anularam!");
            return;
        }

        if (aTemFaca)
        {
            Golpear(jogadorA, jogadorB);
        }
        else if (bTemFaca)
        {
            Golpear(jogadorB, jogadorA);
        }
    }

    private void Golpear(CombateJogador atacante, CombateJogador alvo)
    {
        // alvo ainda piscando: o golpe não conta, e o atacante continua com a faca
        if (alvo.Vida.Invencivel)
        {
            return;
        }

        atacante.Arma.RemoverFaca();

        bool morreu = alvo.Vida.TomarDano();
        if (morreu)
        {
            EncerrarPartida(atacante, alvo);
        }
    }

    private void EncerrarPartida(CombateJogador vencedor, CombateJogador perdedor)
    {
        partidaEncerrada = true;

        vencedor.Parar();
        perdedor.Morrer();

        float espera = perdedor.DuracaoMorte + pausaAntesDoResultado;
        StartCoroutine(MostrarResultado(vencedor, espera));
    }

    private IEnumerator MostrarResultado(CombateJogador vencedor, float espera)
    {
        // deixa a animação de morte terminar antes de cobrir a tela
        yield return new WaitForSeconds(espera);

        string cor = ColorUtility.ToHtmlStringRGB(vencedor.CorNome);
        textoVencedor.text = $"<color=#{cor}>{vencedor.Nome}</color>\nVENCEU!";
        painelResultado.SetActive(true);
    }
    public void ReiniciarPartida()
    {
        SceneManager.LoadScene("Arena");
    }

    public void VoltarAoMenu()
    {
        SceneManager.LoadScene("MenuInicial");
    }
}