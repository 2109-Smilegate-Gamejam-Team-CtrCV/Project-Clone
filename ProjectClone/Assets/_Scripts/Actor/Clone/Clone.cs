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

    [Header("채굴")]
#if UNITY_EDITOR
    [SerializeField] Color miningGizmoColor = Color.yellow;
#endif
    public int miningPower = 3;
    public float miningSpeed = 1.3f;
    public float miningRange = 2f;
    public float miningRate = 1f; // 자원 획득 배율

    // 자원은 매니저에서 갖는게?
    public int mineral = 0; 
    public int organic = 0;

    [Header("건설")]
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
            // todo : 호버링된 오브젝트에 아웃라인 주면 좋을듯?

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
        // todo : 채굴 사정거리 내의 채굴 대상을 좌클릭하면 채굴 진행
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
        // todo : 건설 사정거리 내의 건설 대상을 좌클릭하면 건설 진행
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
        // todo : 1초마다 멘탈을 깍는다.
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
        // todo : clone을 죽게 하고 새로 스폰한다.
        // 게임매니저에서 처리
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