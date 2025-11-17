using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float maxDrag = 4f;
    public float power = 8f;
    public Rigidbody2D rb;
    public LineRenderer lr;
    public Trajectory trajectory;
    Vector3 dragStartPos;
    bool dragging = false;
    public ParticleSystem impactEffect;

    private bool firstHitDone = false;
    public TrailRenderer trail;
    public Character[] characterSwing;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (dragging == true)
        {
            Vector3 draggingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 finalDraggingPos = 2 * dragStartPos - draggingPos;


            lr.positionCount = 2;
            lr.SetPosition(1, finalDraggingPos);


            Vector3 force = dragStartPos - draggingPos;
            Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

            trajectory.Show(transform.position, clampedForce);
        }

        if(dragging == false && Input.GetMouseButtonDown(0)) 
        {
            dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragStartPos.z = 0;

            dragging = true;

            lr.positionCount = 1;
            lr.SetPosition(0, dragStartPos);

            trajectory.Hide();

        }
        if (Input.GetMouseButtonUp(0))
        {
            lr.positionCount = 0;
            dragging = false;

            Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 force = dragStartPos - dragReleasePos;
            Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

            if(rb.linearVelocity.magnitude >=0 && rb.linearVelocity.magnitude <= 0.5f)
            {
                rb.AddForce(clampedForce, ForceMode2D.Impulse);
             if(!firstHitDone)
                {
                    trail.emitting = true;
                    firstHitDone = true;

                }
                foreach (Character c in characterSwing)
                {
                    c.Swing();
                }

            }

            //trajectory.Hide();

        }
        if(rb.linearVelocity.magnitude <0.1f && firstHitDone)
        {
            trail.emitting = false;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && rb.linearVelocity.magnitude > 3f)
        {
            impactEffect.transform.position = transform.position;
            impactEffect.Play();
        }

    }

}
