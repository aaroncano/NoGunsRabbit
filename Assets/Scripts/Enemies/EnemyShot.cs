using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    //se activa si se quiere iniciar del lado izquierdo
    [SerializeField] private bool isFlipped = false;
    
    //Line renderers
    [SerializeField] private LineRenderer shootRayLine = null;
    [SerializeField] private LineRenderer checkRayLine = null;

    //partículas
    [SerializeField] private GameObject shootingParticles = null;

    [SerializeField] private float detectPlayerRange = 3f;
    [SerializeField] private LayerMask detectLayerMask = 1;
    [SerializeField] private Transform eyesPoint = null;
    [SerializeField] private float timeBetweenShot = 5f;

    //modificadores
    [SerializeField] private bool shootEveryone = true;
    [SerializeField] private bool isAutomatic = false;

    private bool isReadyToShoot;
    private bool canShoot;
    private bool isShooting;
    private float waitingForShot;

    private void Awake()
    {
        if (isFlipped) flip();

        isShooting = false;
        isReadyToShoot = false;
        canShoot = true;
        waitingForShot = 0f;
    }

    private void Update()
    {
        if (waitingForShot <= 0f )
        {
            isReadyToShoot = true;
        }
        else
        {
            waitingForShot -= Time.deltaTime;
        }

        if (isReadyToShoot && canShoot)
        {
            if (!isAutomatic)
            {
                checkRayLine.enabled = true;
                if (checkRay())
                {
                    canShoot = false;

                    StartCoroutine(shootingRay()); //iniciar corrutina de laser (isShooting = true por un tiempo) y luego restaura los valores.
                }
            }
            else
            {
                canShoot = false;
                StartCoroutine(shootingRay());
            }
        }
        else
        {
            checkRayLine.enabled = false;
        }

        if (isShooting)
        {
            shootRay();
        }
    }

    private bool checkRay()
    {
        bool value = false;

        Vector2 endPosition = eyesPoint.position + Vector3.right * detectPlayerRange;
        RaycastHit2D ray = Physics2D.Linecast(eyesPoint.position, endPosition, detectLayerMask);

        checkRayLine.SetPosition(0, eyesPoint.position);
        checkRayLine.SetPosition(1, endPosition);

        if (ray.collider != null)
        {
            if (ray.collider.gameObject.CompareTag("Player")) value = true;
            if (shootEveryone)
            {
                if (ray.collider.gameObject.CompareTag("Enemy") && ray.collider.gameObject != gameObject) value = true;
            } 
        }

        return value;
    } //raycast que detecta si algo entra en su sensor.

    private void shootRay()
    {
        Vector2 endPosition = eyesPoint.position + Vector3.right * detectPlayerRange;
        //RaycastHit2D ray = Physics2D.Linecast(eyesPoint.position, endPosition, detectLayerMask);
        RaycastHit2D ray = Physics2D.Raycast(eyesPoint.position, Vector3.right, detectPlayerRange, detectLayerMask);

        shootRayLine.SetPosition(0, eyesPoint.position);
        shootRayLine.SetPosition(1, endPosition);
        
        if (ray.collider != null)
        {
            if (ray.collider.gameObject.CompareTag("Player")) Events.playerHitEvent.Invoke(new hitEventData(gameObject, ray.collider.gameObject));
            if (ray.collider.gameObject.CompareTag("Enemy")) Events.enemyHitEvent.Invoke(new hitEventData(gameObject, ray.collider.gameObject));
        }
    } //raycast de disparo.

    private IEnumerator shootingRay()
    {
        Destroy(Instantiate(shootingParticles, eyesPoint.position, Quaternion.identity), 3f);
        yield return new WaitForSeconds(1.5f); //tiempo de espera para disparar (carga de laser).
        
        isShooting = true;
        shootRayLine.enabled = true;

        yield return new WaitForSeconds(2f); //tiempo visible del laser.

        isShooting = false;
        shootRayLine.enabled = false; //hacer invisible el laser
        
        isReadyToShoot = false;
        waitingForShot = timeBetweenShot;
        canShoot = true;
    } //corrutina que activa el disparo en un intervalo de tiempo.

    public void changeDetectPlayerRange() { detectPlayerRange = -detectPlayerRange; }
    private void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        changeDetectPlayerRange();
    }
}
