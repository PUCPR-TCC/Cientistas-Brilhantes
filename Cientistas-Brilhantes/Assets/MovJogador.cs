using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovJogador : MonoBehaviour
{
    // VARI�VEIS
    [SerializeField] private float moveSpeed; // mover
    [SerializeField] private float walkSpeed; // andar
    //[SerializeField] private float jumpSpeed;
    [SerializeField] private float runSpeed; // correr

    [SerializeField] private bool isGrounded; // verifica se o personagem est� no ch�o
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight; // altura do pulo

    // vari�vel de 3 floats
    private Vector3 moveDirection; // dire��o para onde nos movimentamos
    private Vector3 velocity;

    // REFER�NCIAS
    private CharacterController controller; // refer�ncia para o controlador do personagem
    private Animator anim;

    //private bool isJumping;

    private void Start()
    {
        // 
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        Move();
    }

    // fun��o para movimento
    private void Move()
    {
        // acessamos o eixo z
        float moveZ = Input.GetAxis("Vertical");


        // VERIFICA SE O PERSONAGEM EST� NO CH�O
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        // transform.position: posi��o do jogador
        // groundCheckDistance: raio da esfera gerada no p� do jogador.
        // groundMask: camada que verificamos. O ch�o est� em uma camada separada.

        // se estiver no ch�o,paramos de aplicar a gravidade
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // dire��o para onde queremos ir
        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        // somente se estiver no ch�o
        if (isGrounded)
        {
            // (0,0,0)
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                // andar
                Walk();
            }

            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                // correr
                Run();
            }

            else if (moveDirection == Vector3.zero)
            {
                // idle
                Idle();
            }
            moveDirection *= moveSpeed;

        }

        // Time.deltaTime: faz com que o movimento continue o mesmo
        // a velocidade de movimento vai ser igual � velocidade selecionada
        controller.Move(moveDirection * Time.deltaTime);


        // calculamos a gravidade do personagem
        velocity.y += gravity * Time.deltaTime;

        // aplicamos a gravidade ao personagem
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    IEnumerator TimeDelay()
    {
        yield return new WaitForSecondsRealtime(2);
    }
}