using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour
{
    #region Properties
    [Header("COMPONENTS")]
    public Transform meshParent;
    public MeshRenderer mesh;
    [HideInInspector] public Player target;

    [Header("STATS")]
    public EnemyData data;
    [SerializeField] private int currentHealth;

    #endregion

    #region Methods
    public virtual void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * data.speed);
        meshParent.forward = Vector3.RotateTowards(meshParent.forward, target.transform.position - transform.position, 10 * Time.deltaTime, 0);
    }

    public virtual void Attack() //example suicide attack
    {
        target.Damage(data.damage);
        Kill();
    }

    public virtual void Damage(int _value)
    {
        if (currentHealth <= 0) return;
        currentHealth -= _value;
        if (meshParent == null) return;
        meshParent.transform.DOScale(1.1f, .1f).OnComplete( () =>
        {
            meshParent.transform.DOScale(1f, .1f).OnComplete(() =>
            {
                if (currentHealth <= 0)
                {
                    Kill();
                }
            });
        });
    }

    public virtual void Kill()
    {
        if (meshParent == null) return;
        meshParent.transform.DOScale(0f, .1f).OnComplete(() => {
            mesh.transform.DOKill();
            Instantiate(GPCtrl.Instance.GeneralData.deathParticles).transform.position = transform.position;
            Destroy(gameObject);
        });
    }
    #endregion

    #region Unity API
    private void OnTriggerEnter(Collider collision)
    {
        Player _player = collision.GetComponent<Player>();
        if (_player != null)
        {
            Attack();
        }
    }

    public virtual void Start()
    {
        target = GPCtrl.Instance.player;
        currentHealth = data.maxHealth;
    }

    private void Update()
    {
        if (GPCtrl.Instance.pause) return;
        Move();
    }
    #endregion
}
