/*
 * Copyright (c) 2008, Anthony James McCreath
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     1 Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     2 Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     3 Neither the name of the project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY Anthony James McCreath "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL Anthony James McCreath BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWork.ScriptBuilder
{
	/// <summary>
	/// A wrapper designed to wrap a ScriptSet
	/// Provides direct access to the ScriptSet
	/// </summary>
	public class ScriptSetWrapper : ScriptWrapper, IEnumerable<object>, IList<object>, ICollection<object>
	{
		#region Constructors
		/// <summary>
		/// Create an empty wrapper
		/// </summary>
		public ScriptSetWrapper()
		{
		}

		/// <summary>
		/// Create an empty wrapper with a custom layout
		/// </summary>
		/// <param name="layout"></param>
		public ScriptSetWrapper(ScriptLayout layout)
			:base(layout)
		{
		}

		/// <summary>
		/// Create a wrapper with a custom layout
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="before">object to render before the item</param>
		/// <param name="set">the item to render</param>
		/// <param name="after">object to render after the item</param>
		/// <returns></returns>
		public ScriptSetWrapper(ScriptLayout layout, object before, ScriptSet set, object after)
			: base(layout)
		{
			this.SetWrapper(before, after);
			this.Set = set;
		}

		/// <summary>
		/// Create a wrapper
		/// </summary>
		/// <param name="before">object to render before the item</param>
		/// <param name="set">the item to render</param>
		/// <param name="after">object to render after the item</param>
		/// <returns></returns>
		public ScriptSetWrapper(object before, ScriptSet set, object after)
			: base()
		{
			this.SetWrapper(before, after);
			this.Set = set;
		}

		/// <summary>
		/// Create a wrapper round a Script using a custom layout
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="before">object to render before the item</param>
		/// <param name="after">object to render after the item</param>
		/// <returns></returns>
		public ScriptSetWrapper(ScriptLayout layout, object before, object after)
			: base(layout)
		{
			this.SetWrapper(before, after);
			this.Set = Sb.Script();
		}

		/// <summary>
		/// Create a wrapper round a Script
		/// </summary>
		/// <param name="before">object to render before the item</param>
		/// <param name="after">object to render after the item</param>
		/// <returns></returns>
		public ScriptSetWrapper(object before, object after)
			: base()
		{
			this.SetWrapper(before, after);
			this.Set = Sb.Script();
		}
		#endregion

		#region Data
		/// <summary>
		/// The ScriptSet used as the content for this wrapper
		/// </summary>
		public ScriptSet Set
		{
			get { return (ScriptSet)Content; }
			set { Content = value; }
		}
		#endregion

		#region Parameterised Range Adding
		/// <summary>
		/// Insert an array of items into the set
		/// </summary>
		/// <param name="items"></param>
		public void InsertRange(params object[] items)
		{
			this.Set.InsertRange(0, items);

		}

		/// <summary>
		/// Add an array of items to the end of the set
		/// </summary>
		/// <param name="items"></param>
		public void AddRange(params object[] items)
		{
			this.Set.AddRange(items);

		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return ((System.Collections.IEnumerable)Set).GetEnumerator();
		}

		#endregion

		#region IEnumerable<object> Members

		IEnumerator<object> IEnumerable<object>.GetEnumerator()
		{
			return ((IEnumerable<object>)Set).GetEnumerator();
		}

		#endregion

		#region IList<object> Members
		/// <summary>
		/// Determines the index of a specific item
		/// </summary>
		/// <param name="item">The object to locate</param>
		/// <returns>The index of item if found in the list; otherwise, -1. </returns>
		public int IndexOf(object item)
		{
			return Set.IndexOf(item);
		}

		/// <summary>
		/// Inserts an item at the specified index
		/// </summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		public void Insert(int index, object item)
		{
			Set.Insert(index, item);
		}

		/// <summary>
		/// Removes the first occurrence of a specific object 
		/// </summary>
		/// <param name="index"></param>
		public void RemoveAt(int index)
		{
			Set.RemoveAt(index);
		}

		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public object this[int index]
		{
			get
			{
				return Set[index];
			}
			set
			{
				Set[index] = value;
			}
		}

		#endregion

		#region ICollection<object> Members

		/// <summary>
		/// Adds an item to the end of the set
		/// </summary>
		/// <param name="item"></param>
		public void Add(object item)
		{
			Set.Add(item);
		}

		/// <summary>
		/// Removes all items 
		/// </summary>
		public void Clear()
		{
			Set.Clear();
		}

		/// <summary>
		/// Determines whether the set contains a specific item.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(object item)
		{
			return Set.Contains(item);
		}

		/// <summary>
		/// Copies the elements of the set to an Array, starting at a particular Array index.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(object[] array, int arrayIndex)
		{
			Set.CopyTo(array,arrayIndex);
		}

		/// <summary>
		/// how many items are in the set
		/// </summary>
		public int Count
		{
			get { return Set.Count; }
		}

		/// <summary>
		/// False
		/// </summary>
		public bool IsReadOnly
		{
			get { return Set.IsReadOnly; }
		}

		/// <summary>
		/// Removes the first occurrence of a specific object 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Remove(object item)
		{
			return Set.Remove(item);
		}

		#endregion
	}
}
