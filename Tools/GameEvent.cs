using System.Collections.Generic;
using UnityEngine;

namespace _Main._Core.Scripts.Tools
{
	[CreateAssetMenu]
	public class GameEvent : ScriptableObject
	{
        private List<GameEventListener> m_listeners = new List<GameEventListener>();

		public void Raise()
		{
			for (int i = m_listeners.Count - 1; i >= 0; i--)
				m_listeners[i].OnEventRaised();
		}

		public void RegisterListener(GameEventListener listener)
		{
			m_listeners.Add(listener);
		}

		public void UnregisterListener(GameEventListener listener)
		{
			m_listeners.Remove(listener);
		}
	}
}
