using System;

namespace UniEx
{
	public class ActionWrapper
	{
		private Action _action;

		public void AddEvent(Action e) 
		{
			_action += e;
		}

		public void RemoveEvent(Action e) 
		{
			_action -= e;
		}

		public void Invoke() 
		{
			_action?.Invoke();
		}
	}

	public class ActionWrapper<T>
	{
		private Action<T> _action;
		
		public void AddEvent(Action<T> e)
		{
			_action += e;
		}

		public void RemoveEvent(Action<T> e)
		{
			_action -= e;
		}

		public void Invoke(T value)
		{
			_action?.Invoke(value);
		}
	}

	public class ActionWrapper<T1, T2>
	{
		private Action<T1, T2> _action;
		
		public void AddEvent(Action<T1, T2> e)
		{
			_action += e;
		}

		public void RemoveEvent(Action<T1, T2> e)
		{
			_action -= e;
		}

		public void Invoke(T1 value1, T2 value2)
		{
			_action?.Invoke(value1, value2);
		}
	}
	
	public class ActionWrapper<T1, T2, T3>
	{
		private Action<T1, T2, T3> _action;
		
		public void AddEvent(Action<T1, T2, T3> e)
		{
			_action += e;
		}

		public void RemoveEvent(Action<T1, T2, T3> e)
		{
			_action -= e;
		}

		public void Invoke(T1 value1, T2 value2, T3 value3)
		{
			_action?.Invoke(value1, value2, value3);
		}
	}
	
	public class ActionWrapper<T1, T2, T3, T4>
	{
		private Action<T1, T2, T3, T4> _action;
		
		public void AddEvent(Action<T1, T2, T3, T4> e)
		{
			_action += e;
		}

		public void RemoveEvent(Action<T1, T2, T3, T4> e)
		{
			_action -= e;
		}

		public void Invoke(T1 value1, T2 value2, T3 value3, T4 value4)
		{
			_action?.Invoke(value1, value2, value3, value4);
		}
	}
	
	public class ActionWrapper<T1, T2, T3, T4, T5>
	{
		private Action<T1, T2, T3, T4, T5> _action;
		
		public void AddEvent(Action<T1, T2, T3, T4, T5> e)
		{
			_action += e;
		}

		public void RemoveEvent(Action<T1, T2, T3, T4, T5> e)
		{
			_action -= e;
		}

		public void Invoke(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
		{
			_action?.Invoke(value1, value2, value3, value4, value5);
		}
	}
	
	public class ActionWrapper<T1, T2, T3, T4, T5, T6>
	{
		private Action<T1, T2, T3, T4, T5, T6> _action;
		
		public void AddEvent(Action<T1, T2, T3, T4, T5, T6> e)
		{
			_action += e;
		}

		public void RemoveEvent(Action<T1, T2, T3, T4, T5, T6> e)
		{
			_action -= e;
		}

		public void Invoke(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
		{
			_action?.Invoke(value1, value2, value3, value4, value5, value6);
		}
	}
	
	public class ActionWrapper<T1, T2, T3, T4, T5, T6, T7>
	{
		private Action<T1, T2, T3, T4, T5, T6, T7> _action;
		
		public void AddEvent(Action<T1, T2, T3, T4, T5, T6, T7> e)
		{
			_action += e;
		}

		public void RemoveEvent(Action<T1, T2, T3, T4, T5, T6, T7> e)
		{
			_action -= e;
		}

		public void Invoke(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
		{
			_action?.Invoke(value1, value2, value3, value4, value5, value6, value7);
		}
	}
}
