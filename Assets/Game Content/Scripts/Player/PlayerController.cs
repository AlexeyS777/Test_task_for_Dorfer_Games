using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _sickle;
    [SerializeField] private Bag _bag;
    [SerializeField] private float _moveSpeed = 8f;
    
    public float Speed { get => _moveSpeed; }    

    private Rigidbody _body;
    private Transform _myPos;
    private Animator _anim;

    public StateMachine _state;
    public Move _moveState;
    public Standing _stayState;
    public Harvest _harvestState;


    private void Start()
    {
        _state = new StateMachine();
        _anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        _myPos = GetComponent<Transform>();
        _bag.Inicializing();

        _stayState = new Standing(this, _state);
        _moveState = new Move(this, _state);
        _harvestState = new Harvest(this, _state);

        _state.Initialize(_stayState);
    }

    private void Update()
    {
        _state.CurrentState.HandleInput();
        _state.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _state.CurrentState.PhysicsUpdate();
    }    

    public void Move(float speed, float angle)
    {
        transform.eulerAngles = Vector3.up * angle;
        _body.velocity = transform.forward * speed;
        _anim.SetFloat("Speed", speed);
    }

    public string Scaning()
    {
        Vector3 point = _myPos.position;
        point.y = 0.5f;
        Ray ray = new Ray(point, _myPos.forward);

        RaycastHit hit;

        if (Physics.SphereCast(ray,0.3f, out hit, 0.5f))
        {
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.name + " / " + hit.collider.transform.parent.name);
                return hit.collider.tag;                
            }
        }

        return null;
    }

    public void AnimationTrigger(int param)
    {
        _anim.SetTrigger(param);
    }

    public void Harvest()
    {
        _anim.SetTrigger("Harvest");
        _sickle.SetActive(true);
    }

    public void EndHarvest()
    {
        _sickle.SetActive(false);
        _state.ChangeState(_stayState);        
    }
}
