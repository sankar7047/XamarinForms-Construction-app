using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MartinPulgarConstruction.Models
{
	public class BaseNotify : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public bool SetPropertyChanged<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = "", bool forceRefresh = false)
		{
			return PropertyChanged.SetProperty(this, ref currentValue, newValue, propertyName, forceRefresh);
		}

		public void SetPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

namespace System.ComponentModel
{
	public static class BaseNotify
	{
		public static bool SetProperty<T>(this PropertyChangedEventHandler handler, object sender, ref T currentValue, T newValue, [CallerMemberName] string propertyName = "", bool forceRefresh = false)
		{
			if (!forceRefresh && EqualityComparer<T>.Default.Equals(currentValue, newValue))
				return false;

			currentValue = newValue;

			if (handler == null)
				return true;

			handler.Invoke(sender, new PropertyChangedEventArgs(propertyName));
			return true;
		}
	}
}