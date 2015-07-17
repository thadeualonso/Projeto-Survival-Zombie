using UnityEngine;
using System.Collections;

public class Player : Humanoid {

	//Declaraçao de variaveis
	public Transform groundCheck; // Objeto para checar colisao com o  chao
	public LayerMask whatIsGround; // Layer para indicar o que e chao
	public float jumpForce = 700f; // Força do pulo

    private bool olhandoDireita = true;
	private bool grounded = false; // Bool para indicar se o personagem esta tocando o chao
	private float eixoX; // Float que recebe o valor do Input.GetAxis("Horizontal") para movimentacao
    private float eixoY;
    private Animator animator; // Armazena o componente Animator do gameObject
	private Rigidbody2D body2D; // Armazena o componente Rigidbody2D do gameObject
	private float groundRadius = 0.2f; // Raio produzido pelo groundCheck
	private bool isAndando = false; // Bool para dizer se o personagem esta andando ou parado
    private bool canClimb = false;
    private int exp;
    private BoxCollider2D boxCollider;

	// Use this for initialization
	void Awake () {
        // Atributos
        this.HP = 10;
        this.Dano = 2;
        this.exp = 0;

        animator = GetComponent<Animator>();    //Recebe o componente Animator do gameObject
        body2D = GetComponent<Rigidbody2D>();   // Recebe o componente Rigidbody2D do gameObject
        boxCollider = GetComponent<BoxCollider2D>();
        this.velocidadeMax = 5.0f;              // Velocidade maxima do personagem
	}
	
	// FixedUpdate tem resultados melhores para uso da fisica
	void FixedUpdate () 
    {
        Mover();
        Pulo();
	}

    // Evento chamado ao entrar em colisão com algum collider2D com trigger
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Se a tag do objeto em colisao for igual a Ladder
        if (collider.gameObject.tag == "Ladder")
        {
            canClimb = true;
        }
    }

    // Evento chamado ao sair da colisão com algum collider2D com trigger
    void OnTriggerExit2D(Collider2D collider)
    {
        // Se a tag do objeto em colisao for igual a Ladder
        if (collider.gameObject.tag == "Ladder")
        {
            canClimb = false;
        }
    }

    void Escalar()
    {
        // Muda a gravidade do rigidbody para 0
        body2D.gravityScale = 0;    
        // Faz o player se mover nos eixos X e Y
        body2D.velocity = new Vector2(eixoX * velocidadeMax, eixoY * velocidadeMax);    
    }

    void Pulo()
    {
        if (grounded && Input.GetButtonDown("Jump")) // Se estiver tocando o chao e o botao Jump for pressionado 1 vez
        {
            animator.SetBool("ground", false);          // animator ground se torna falso
            body2D.AddForce(new Vector2(0, jumpForce)); // Adiciona força de pulo no eixo Y
        }
    }

    // Método reescrito da classe Humanoid 
    public override void Mover()
    {
        eixoX = Input.GetAxis("Horizontal");    // Recebe o valor float dos controles no eixo x
        eixoY = Input.GetAxis("Vertical");      // Recebe o valor float dos controles no eixo y

        // ground recebe true se o personagem tocar o chao ou false se nao 
        // Physics2D.OverlapCircle cria um circulo que detecta colisao com algum LayerMask
        // Parametros (Posicao, Raio, Layer)
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        animator.SetFloat("speed", Mathf.Abs(eixoX));   // Passa o valor exato dos controles para o animator do gameObject
        animator.SetBool("ground", grounded);           // Muda o Bool ground do animator
        animator.SetFloat("vSpeed", body2D.velocity.y); // Muda o Float vSpeed do animator passando a velocidade vertical
        animator.SetBool("isCrouch", false);
        isAndando = false;                              // Muda o Bool isAndando para false, indicando que o player não está andando

        if( (grounded) && (eixoX == 0) && (Input.GetKey(KeyCode.S)) )
        {
            Agachar();
        }
        else
        {
            boxCollider.size = new Vector2(boxCollider.size.x, 1.20f);
            boxCollider.offset = new Vector2(boxCollider.offset.x, 0.3f);
        }

        if (canClimb) // Se canClimb = True
        {
            Escalar();
        }
        else
        {
            body2D.gravityScale = 6;    // Muda a gravidade do rigidbody para 6
            body2D.velocity = new Vector2(eixoX * velocidadeMax, body2D.velocity.y);   // Move na direcao recebida do eixo x
        }

        if (eixoX != 0) // Se alguma força estiver sendo aplicada nos eixos horizontais
        {
            isAndando = true;
            velocidadeMax = 5f;
        }

        if (isAndando && Input.GetButton("Run"))    // Run = left shift
        {
            velocidadeMax = 10f;
        }

        // Se estiver movendo para a direita e nao estiver olhando para a direita
        if (eixoX > 0 && !olhandoDireita)
        {
            // Inverte a posicao
            Flip();

            // Caso contratio se estiver movendo para a esquerda e estiver olhando para a direita
        }
        else if (eixoX < 0 && olhandoDireita)
        {
            // Inverte a posicao
            Flip();

        }
    }

    // Inverte o personagem
    void Flip()
    {
        olhandoDireita = !olhandoDireita;   // Muda a direcao para qual o gameObject esta olhando
        Vector3 theScale = transform.localScale;    // Armazena a escala local do gameObject
        theScale.x *= -1;   // Inverte a escala
        transform.localScale = theScale;    // Aplica a inversao
    }

    void Agachar()
    {
        animator.SetBool("isCrouch", true);
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
        boxCollider.size = new Vector2(boxCollider.size.x, 1.20f);
        boxCollider.offset = new Vector2(boxCollider.offset.x, 0.20f);
    }

    void CheckGameOver()
    {
        if (this.HP <= 0)
        {
            Destroy(this);
        }
    }

}
