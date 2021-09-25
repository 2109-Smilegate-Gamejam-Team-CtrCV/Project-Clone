using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EMindState
{
    Stability,
    Instability,
}

public class Clone : Actor, IPlayable
{
    public float mental;
    public float maxMental = 30;
    public float consumeMental = 1;
    public float totalCousumeMental { get { return eMindState == EMindState.Stability ? consumeMental : consumeMental * 0.5f; } }

    [Header("ä��")]
#if UNITY_EDITOR
    [SerializeField] Color miningGizmoColor = Color.yellow;
#endif
    public int miningPower = 3;
    public float miningSpeed = 1.3f;
    public float miningRange = 2f;
    public float miningRate = 1f; // �ڿ� ȹ�� ����

    // �ڿ��� �Ŵ������� ���°�?
    public int mineral = 0; 
    public int organic = 0;

    [Header("�Ǽ�")]
#if UNITY_EDITOR
    [SerializeField] Color buildingGizmoColor = Color.blue;
#endif
    public int buildingPower = 3;
    public float buildingSpeed = 1.3f;
    public float buildingRange = 1f;

    DateTime nextMiningTime;
    DateTime nextBuildTime;
    DateTime nextConsumeTime;

    EMindState eMindState = EMindState.Stability;
    bool isDead = false;

    protected override void Update()
    {
        if (isDead)
            return;

        ReduceMental();

        if (Input.GetMouseButton(0))
        {
            ClickObject();
        }

        base.Update();
        //Accelerate();
    }

    public override void Init()
    {
        base.Init();

        EnemyTags = new string[] { "Enemy" };

        mental = maxMental;
        isDead = false;

        nextMiningTime = DateTime.Now;
        nextBuildTime = DateTime.Now;
        nextConsumeTime = DateTime.Now;
    }

    public override void Move()
    {
        Vector2 moveDir = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDir += Vector2.up;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDir += Vector2.down;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir += Vector2.left;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDir += Vector2.right;
        }

        moveDir = moveDir.normalized;
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        SetMoveAnimation(moveDir);
    }

    void Accelerate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontal, vertical) * moveSpeed;
    }

    public void ClickObject()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(worldPos.x, worldPos.y);

        Ray2D ray = new Ray2D(mousePos, Vector2.zero);
        var hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null)
        {
            // todo : ȣ������ ������Ʈ�� �ƿ����� �ָ� ������?

            //Debug.LogFormat("click target name : " + hit.collider.name);

            if (hit.collider.CompareTag("Mining"))
            {
                Mine(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("Building"))
            {
                Build(hit.collider.gameObject);
            }
        }
    }

    public void Mine(GameObject target)
    {
        // todo : ä�� �����Ÿ� ���� ä�� ����� ��Ŭ���ϸ� ä�� ����
        var mining = target.GetComponent<MiningObject>();
        if (mining)
        {
            if (transform.position2D().IsInRange(mining.transform.position2D(), miningRange))
            {
                if (nextMiningTime.IsEnoughTime())
                {
                    Debug.LogFormat("mine target name : " + target.name);
                    nextMiningTime = DateTime.Now.AddSeconds(miningSpeed);
                    mining.GetDamage(miningPower);
                }
            }
        }
    }

    public void Build(GameObject target)
    {
        // todo : �Ǽ� �����Ÿ� ���� �Ǽ� ����� ��Ŭ���ϸ� �Ǽ� ����
        var building = target.GetComponent<MiningObject>();
        if (building)
        {
            if (transform.position2D().IsInRange(building.transform.position2D(), buildingRange))
            {
                if (nextBuildTime.IsEnoughTime())
                {
                    Debug.LogFormat("build target name : " + target.name);
                    nextBuildTime = DateTime.Now.AddSeconds(buildingSpeed);
                    building.GetDamage(miningPower);
                }
            }
        }
    }

    public void GainResource(EResource eResource, int amount)
    {
        amount = (int)(amount * miningRate);

        if (eResource == EResource.Mineral)
            GameManager.Instance.gamePresenter.gameModel.AddMineral(amount);
        else
            GameManager.Instance.gamePresenter.gameModel.AddOrganism(amount);
    }

    bool ReduceMental()
    {
        // todo : 1�ʸ��� ��Ż�� ��´�.
        if (nextConsumeTime.IsEnoughTime())
        {
            float nextTime = eMindState == EMindState.Stability ? 1f : 0.5f;
            nextConsumeTime = DateTime.Now.AddSeconds(nextTime);

            mental = Mathf.Clamp(mental - totalCousumeMental, 0, maxMental);
            GameManager.Instance.gamePresenter.gameMainView.MetalGauge = mental / maxMental;
            //GameManager.Instance.gamePresenter.gameModel.AddMental(-totalCousumeMental);

            if (mental <= 0)
            {
                Dead();
            }
        }

        return mental <= 0;
    }

    public override bool GetDamage(int power)
    {
        bool isDead = base.GetDamage(power);
        GameManager.Instance.gamePresenter.gameMainView.SetHeart(HP);

        return isDead;
    }

    public override void Dead()
    {
        // todo : clone�� �װ� �ϰ� ���� �����Ѵ�.
        // ���ӸŴ������� ó��
        Debug.Log("DEAD!!");
        isDead = true;
    }

    public void SetMindState(EMindState newState)
    {
        eMindState = newState;
    }

#region Animation
    public void SetMoveAnimation(Vector2 moveDir)
    {
        animator.SetBool("Walk", moveDir != Vector2.zero);

        if (moveDir.x != 0)
            render.flipX = moveDir.x > 0;
    }

#endregion

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = miningGizmoColor;
        Gizmos.DrawWireSphere(transform.position, miningRange);

        Gizmos.color = buildingGizmoColor;
        Gizmos.DrawWireSphere(transform.position, buildingRange);
    }
#endif
}

public interface IPlayable
{
    public void Mine(GameObject target);
    public void Build(GameObject target);
}