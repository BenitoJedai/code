using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibAppJet.JavaScript.AppJet
{
	[Script(HasNoPrototype = true, ExternalTarget = "StorableCollection")]
	public class StorableCollection
	{
		/// <summary>
		/// Adds a StorableObject to this collection.
		/// </summary>
		/// <param name="e"></param>
		public object add(object e)
		{
			return default(object);

		}

		/// <summary>
		/// Removes StorableObjects from this collection.
		/// </summary>
		/// <param name="filter"></param>
		public void remove(object filter)
		{

		}

		/// <summary>
		/// Returns the number of elements in a collection. Can also be applied to filtered and sorted views of a collection. (This number may be approximate if your collection is very large or if many requests are modifying it simultaneously.)
		/// </summary>
		/// <returns></returns>
		public int size()
		{
			return default(int);
		}

		/// <summary>
		/// Returns a view that skips the first n elements of this collection or view.
		/// </summary>
		/// <param name="n"></param>
		/// <returns></returns>
		public StorableCollection skip(int n)
		{
			return default(StorableCollection);
		}

		/// <summary>
		/// Returns a view consisting of the first n elements of this collection or view.
		/// </summary>
		/// <param name="n"></param>
		/// <returns></returns>
		public StorableCollection limit(int n)
		{
			return default(StorableCollection);
		}

		/// <summary>
		/// Gets the first object in this StorableCollection.
		/// </summary>
		/// <returns></returns>
		public object first()
		{
			return default(object);
		}

	}
}
