using UnityEngine;
using System.Collections;

public class SimplePlatformMover : MonoBehaviour
{
	// public variables:
	
	public GameObject m_targetPlatform;
	public GameObject[] m_wayPoints;
	public bool m_loop;
	[Range(0.0f, 10.0f)] // a slider in the editor
	public float m_moveSpeed;
	public float m_waitAtWayPointTime;

	// private variables:
	int m_currentWayPointIndex;
	float m_moveTime;
	bool m_isMoving;
	Transform m_transform;


	// Use this for initialization
	void Start ()
	{
		// Start to move immediately
		m_isMoving = true;
		m_currentWayPointIndex = 0;
		m_transform = m_targetPlatform.transform;
		m_moveTime = 0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time >= m_moveTime)
		{
			Move();
		}
	}

	void Move()
	{
		if (m_isMoving && m_wayPoints.Length > 0)
		{
			// Calculate the next position to move to in the next tick
			m_transform.position = Vector3.MoveTowards(m_transform.position, m_wayPoints[m_currentWayPointIndex].transform.position, m_moveSpeed * Time.deltaTime);

			// If reach the next way point, change to move a another way point
			if (Vector3.Distance(m_transform.position, m_wayPoints[m_currentWayPointIndex].transform.position) <= 0f)
			{
				m_currentWayPointIndex++;
				m_moveTime = Time.time + m_waitAtWayPointTime;
			}

			// If walk through all the wait points
			if (m_currentWayPointIndex >= m_wayPoints.Length)
			{
				if (m_loop)
				{
					m_currentWayPointIndex = 0;
				}
				else
				{
					m_isMoving = false;
				}
			}
		}
	}
}

