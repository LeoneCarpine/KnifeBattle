using UnityEngine;

public class GeradorFacasMenu : MonoBehaviour
{
    [SerializeField] private FacaCaindoUI prefabFaca;
    [SerializeField] private float intervalo = 0.5f;          // uma faca a cada meio segundo
    [SerializeField] private float velocidadeMinima = 250f;
    [SerializeField] private float velocidadeMaxima = 500f;

    private RectTransform area;
    private float cronometro;

    void Awake()
    {
        area = GetComponent<RectTransform>();
    }

    void Update()
    {
        cronometro += Time.deltaTime;

        if (cronometro >= intervalo)
        {
            cronometro -= intervalo;
            CriarFaca();
        }
    }

    private void CriarFaca()
    {
        float meiaLargura = area.rect.width / 2f;
        float meiaAltura = area.rect.height / 2f;

        // cria a faca como filha deste objeto
        FacaCaindoUI faca = Instantiate(prefabFaca, area);

        // posição: X aleatório na largura toda, Y um pouco acima do topo (fora da tela)
        RectTransform rectFaca = faca.GetComponent<RectTransform>();
        rectFaca.anchoredPosition = new Vector2(
            Random.Range(-meiaLargura, meiaLargura),
            meiaAltura + 100f);

        // velocidade aleatória pra cada faca, e ela some um pouco abaixo da base
        faca.Configurar(
            Random.Range(velocidadeMinima, velocidadeMaxima),
            -meiaAltura - 100f);
    }
}