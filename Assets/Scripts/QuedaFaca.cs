using UnityEngine;

public class QuedaFaca : MonoBehaviour
{
    [SerializeField] private Transform visual;
    [SerializeField] private Transform sombra;
    [SerializeField] private float altura = 8f;      // de quão alto a faca começa a cair
    [SerializeField] private float duracao = 0.6f;   // quanto tempo leva pra chegar ao chão

    [Header("Som ao cravar (opcional)")]
    [SerializeField] private AudioSource fonteSom;
    [SerializeField] private AudioClip somCravar;

    private Collider2D areaPegar;
    private Vector3 posicaoFinal;
    private Vector3 escalaSombra;
    private float tempo;
    private bool cravou;

    void Awake()
    {
        areaPegar = GetComponent<Collider2D>();
        areaPegar.enabled = false;               // ninguém pega a faca no ar

        posicaoFinal = visual.localPosition;      // guarda onde o desenho deve terminar
        escalaSombra = sombra.localScale;         // guarda o tamanho normal da sombra

        visual.localPosition = posicaoFinal + Vector3.up * altura;
        sombra.localScale = Vector3.zero;
    }

    void Update()
    {
        if (cravou)
        {
            return;
        }

        tempo += Time.deltaTime;
        float t = Mathf.Clamp01(tempo / duracao); // vai de 0 até 1 durante a queda

        // t * t faz a faca começar devagar e acelerar, como algo caindo de verdade
        float queda = t * t;
        visual.localPosition = Vector3.Lerp(posicaoFinal + Vector3.up * altura, posicaoFinal, queda);

        // a sombra cresce conforme a faca se aproxima do chão
        sombra.localScale = escalaSombra * t;

        if (t >= 1f)
        {
            Cravar();
        }
    }

    private void Cravar()
    {
        cravou = true;
        areaPegar.enabled = true;

        if (fonteSom != null && somCravar != null)
        {
            fonteSom.PlayOneShot(somCravar);
        }
    }
}