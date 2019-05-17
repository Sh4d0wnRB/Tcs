using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMove : MonoBehaviour
{
    #region Variaveis
    Rigidbody2D RigidBody;
    public float VelocidadeMaxima;
    float EixoX;
    public ParticleSystem fumaça;
    public bool OlhandoParaDireita = true;
    public float aceleração;
    bool parado;
    public float pulo;
    public bool noChao;
    public Transform LocalPe;
    public LayerMask camadaChao;
    int k;
    public float n;
    #endregion

    void Start () {
        RigidBody = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate() {
        aceleração = EixoX * VelocidadeMaxima;
        RigidBody.velocity = new Vector2(aceleração * Time.deltaTime, RigidBody.velocity.y);
        if (Input.GetButtonDown("Jump") && noChao)
        {
            RigidBody.AddForce(new Vector2(0f, pulo), ForceMode2D.Impulse);
        }
        if (Input.GetButtonDown("Jump") && k != 0 && !noChao)
        {
            RigidBody.AddForce(new Vector2(0f, pulo), ForceMode2D.Impulse);
            k--;
        }
    }

	void Update () {
        noChao = Physics2D.OverlapCircle(LocalPe.position, n, camadaChao);
        if (noChao)
        {
            k = 1;
        }
        if (!noChao)
        {
            fumaça.Stop();
        }
        EixoX = Input.GetAxis("Horizontal");
        #region Virar
        if (EixoX == 0 && parado)
        {
            fumaça.Stop();
            parado = false;
        }
        if(EixoX != 0 && !parado)
        {
            parado = true;
            fumaça.Play();
        }
        if (EixoX < 0 && OlhandoParaDireita)
        {
            Virar();
        }
        if (EixoX > 0 && !OlhandoParaDireita)
        {
            Virar();

        }
        #endregion
    }

    void Virar() {
        OlhandoParaDireita = !OlhandoParaDireita;
        var xpto = transform.localScale;
        xpto.x *= -1;
        transform.localScale = xpto;
    }
}
