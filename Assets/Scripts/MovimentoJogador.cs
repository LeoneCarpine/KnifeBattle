using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MovimentoJogador : MonoBehaviour
{
    public enum TipoControle
    {
        WASD,
        Setas
    }

    [SerializeField] private TipoControle controles = TipoControle.WASD;
    [SerializeField] private float velocidade = 4f;

    private Rigidbody2D corpo;
    private Vector2 direcao;
    
    //pega as dimensões do obj referenciado
    void Awake()
    {
        corpo = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direcao = Vector2.zero;

        Keyboard teclado = Keyboard.current;

        if (teclado == null)
        {
            return;
        }

        if (controles == TipoControle.WASD)
        {
            if (teclado.wKey.isPressed)
            {
                direcao.y += 1f;
            }

            if (teclado.sKey.isPressed)
            {
                direcao.y -= 1f;
            }

            if (teclado.aKey.isPressed)
            {
                direcao.x -= 1f;
            }

            if (teclado.dKey.isPressed)
            {
                direcao.x += 1f;
            }
        }
        else
        {
            if (teclado.upArrowKey.isPressed)
            {
                direcao.y += 1f;
            }

            if (teclado.downArrowKey.isPressed)
            {
                direcao.y -= 1f;
            }

            if (teclado.leftArrowKey.isPressed)
            {
                direcao.x -= 1f;
            }

            if (teclado.rightArrowKey.isPressed)
            {
                direcao.x += 1f;
            }
        }

        direcao = Vector2.ClampMagnitude(direcao, 1f);
    }

    //config velocidade nos intervalos da simulação física
    void FixedUpdate()
    {
        corpo.linearVelocity = direcao * velocidade;
    }
}