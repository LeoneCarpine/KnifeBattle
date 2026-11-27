using UnityEngine;

public class BarraTempoFaca : MonoBehaviour
{
    [SerializeField] private ArmaJogador arma;
    [SerializeField] private SpriteRenderer fundo;
    [SerializeField] private SpriteRenderer preenchimento;

    [SerializeField] private Color corCheia = new Color(0.49f, 0.78f, 0.38f);    // verde
    [SerializeField] private Color corMetade = new Color(0.95f, 0.76f, 0.31f);   // amarelo
    [SerializeField] private Color corAcabando = new Color(0.88f, 0.29f, 0.23f); // vermelho

    void LateUpdate()
    {
        bool mostrar = arma.TemFaca;
        fundo.enabled = mostrar;
        preenchimento.enabled = mostrar;

        if (!mostrar)
        {
            return;
        }

        float fracao = arma.FracaoRestante;
        preenchimento.transform.localScale = new Vector3(fracao, 1f, 1f);

        if (fracao > 0.5f)
        {
            preenchimento.color = corCheia;
        }
        else if (fracao > 0.25f)
        {
            preenchimento.color = corMetade;
        }
        else
        {
            preenchimento.color = corAcabando;
        }
    }
}