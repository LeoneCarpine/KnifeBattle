using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoAnimado : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float escalaHover = 1.1f;      // 10% maior com o mouse em cima
    [SerializeField] private float escalaClique = 0.95f;    // 5% menor enquanto segura o clique
    [SerializeField] private float suavidade = 15f;         // quanto maior, mais rápido chega

    private Vector3 escalaOriginal;
    private Vector3 escalaAlvo;
    private bool mouseEmCima;

    void Awake()
    {
        escalaOriginal = transform.localScale;
        escalaAlvo = escalaOriginal;
    }

    void Update()
    {
        // a cada frame, anda uma parte do caminho até a escala alvo
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            escalaAlvo,
            suavidade * Time.unscaledDeltaTime);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEmCima = true;
        escalaAlvo = escalaOriginal * escalaHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseEmCima = false;
        escalaAlvo = escalaOriginal;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        escalaAlvo = escalaOriginal * escalaClique;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // soltou o clique: volta pro hover se o mouse ainda estiver em cima
        escalaAlvo = mouseEmCima ? escalaOriginal * escalaHover : escalaOriginal;
    }
}