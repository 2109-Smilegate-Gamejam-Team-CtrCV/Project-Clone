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
    [HideInInspector] public int mental;
    public int maxMental = 30;
    public int consumeMental = 1;

    [Header("채굴(노랑)")]
#if UNITY_EDITOR
    [SerializeField] Color miningGizmoColor = Color.yellow;
#endif
    public int miningPower = 3;
    public float miningSpeed = 1.3f;
    public float miningRange = 2f;

    // 자원은 매니저에서 갖는게?
    public int mineral = 0; 
    public int organic = 0;

    [Header("건설(초록)")]
#if UNITY_EDITOR
    [SerializeField] Color buildingGizmoColor = Color.green;
#endif
    public int buildingPower = 3;
    public float buildingSpeed = 1.3f;
    public float buildingRange = 1f;

    DateTime nextMiningTime;
    DateTime nextBuildTime;
    DateTime nextConsumeTime;

    EMindState eMindState = EMindState.Stability;

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0))
        {
            ClickObject();
        }
    }

    public override void Init()
    {
        base.Init();

        mental = maxMental;

        nextBuildTime = DateTime.Now;
        nextMiningTime = DateTime.Now;
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

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    public void ClickObject()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(worldPos.x, worldPos.y);

        Ray2D ray = new Ray2D(mousePos, Vector2.zero);
        var hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null)
        {
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
                    nextMiningTime.SetNextTime(miningSpeed);
                    mining.GainResource();
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
            if (transform.position2D().IsInRange(building.transform.position2D(), miningRange))
            {
                if (nextMiningTime.IsEnoughTime())
                {
                    Debug.LogFormat("build target name : " + target.name);
                    nextMiningTime.SetNextTime(miningSpeed);
                    building.GainResource();
                }
            }
        }
    }

    bool ReduceMental()
    {
        // todo : 1초마다 멘탈을 깍는다.
        if (nextConsumeTime.IsEnoughTime())
        {
            mental = Mathf.Clamp(mental - consumeMental, 0, maxMental);
            if (mental <= 0)
            {
                Dead();
            }
        }

        return mental <= 0;
    }

    public override void Dead()
    {
        // todo : clone을 죽게 하고 새로 스폰한다.
    }

    public void SetMindState(EMindState newState)
    {
        eMindState = newState;
    }

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