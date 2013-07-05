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
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using ClockWork.ScriptBuilder.JavaScript;

namespace ClockWork.ScriptBuilder
{

    /// <summary>
	/// A ScriptItem that consists of a set/list/collection of things
    /// </summary>
	public  class ScriptSet : ScriptItem, IEnumerable<object>, IList<object>, ICollection<object>
	{
		#region Constructors
		/// <summary>
		/// Create an empty item
		/// </summary>
		public ScriptSet()

        {
#if DEBUG
			Assert();
#endif
		}

		/// <summary>
		/// Create an empty item using a custom layout
		/// </summary>
		/// <param name="layout">override the default layout</param>
		public ScriptSet(ScriptLayout layout)
			: base(layout)
		{
#if DEBUG
			Assert();
#endif
		}

		/// <summary>
		/// Create a script from a collection of objects
		/// </summary>
		/// <param name="items">collection of objects</param>
		public ScriptSet(IEnumerable<object> items)
        {
			this.Items.AddRange(items);

#if DEBUG
			Assert();
#endif
        }

		/// <summary>
		/// Create an item using a custom layout from a collection of objects
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="items">collection of objects</param>
		public ScriptSet(ScriptLayout layout, IEnumerable<object> items)
			: base(layout)
		{
			this.Items.AddRange(items);
#if DEBUG
			Assert();
#endif
		}

		/// <summary>
		/// Create an item from parameters, each representing a object in the item
		/// </summary>
		/// <param name="items">set of paramters</param>
		public ScriptSet(params object[] items)
        {
			this.Items.AddRange(items);

#if DEBUG
			Assert();
#endif
		}

		/// <summary>
		/// Create an item using a custom layout from parameters
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="items">set of paramters</param>
		public ScriptSet(ScriptLayout layout, params object[] items)
			: base(layout)
		{
			this.Items.AddRange(items);

#if DEBUG
			Assert();
#endif
		}

		#endregion

		#region Data
		private List<object> _Items;
		/// <summary>
		/// The collection of objects in the set
		/// </summary>
		protected List<object> Items
		{
			get
			{
				if (_Items == null)
					_Items = new List<object>();

				return _Items;
			}
		}
		#endregion

		#region Rendering
		private string _Seperator = String.Empty;
		/// <summary>
		/// The string to render between each item
		/// </summary>
		public virtual string Seperator
		{
			get { return _Seperator; }
			set { _Seperator = value; }
		}

		/// <summary>
		/// Gets the items to be included in the sets rendering
		/// </summary>
		/// <returns></returns>
		private IEnumerable<object> GetRenderList()
		{
			List<object> toRender = new List<object>();

			foreach (object o in this)
			{
				AddToRenderList(toRender, o);
			}

			return toRender;
		}

		/// <summary>
		/// By default, adds all items from this collection so they get renderred
		/// Also supports the conversion of object[] to the actual items.
		/// </summary>
		/// <param name="dest"></param>
		/// <param name="o"></param>
		protected virtual void AddToRenderList(List<object> dest, object o)
		{
			if (o is object[])
			{
				foreach (object p in (object[])o)
				{
					AddToRenderList(dest, p);
				}
			}
			else
				dest.Add(o);
		}


		/// <summary>
		/// Used to write the string that seperates each item
		/// By default, renders the Seperator property
		/// </summary>
		/// <param name="writer"></param>
		protected virtual void WriteSeperator(IScriptWriter writer)
		{
			writer.Write(this.Seperator);
		}

		/// <summary>
		/// renders each item in the set, seperated by the Seperator
		/// </summary>
		/// <param name="e">includes the script writer to render content to</param>
		protected override void OnRender(RenderingEventArgs e)
		{
			base.OnRender(e);

			IScriptWriter writer = e.Writer;


			if (this.Layout == ScriptLayout.Block && this.HasRenderContent)
				writer.WriteNewLineAndIndent();


			bool first = true;

			foreach (object line in GetRenderList())
			{
				if (line != null)
				{
					if (line is IScriptItem)
					{
						IScriptItem scriptItem = (IScriptItem)line;
						if (scriptItem.HasRenderContent)
						{
							if (first)
								first = false;
							else
							{
								WriteSeperator(writer);

								// start a new line, if the script item is multiline let it do things for us. this lets it alter indentation first
								if (scriptItem.Layout == ScriptLayout.Inline && 
									(this.Layout == ScriptLayout.Block || this.Layout == ScriptLayout.InlineBlock))
									writer.WriteNewLineAndIndent();
							}
						}
					}
					else
					{
						if (first)
							first = false;
						else
						{
							WriteSeperator(writer);

							if (this.Layout == ScriptLayout.Block || this.Layout == ScriptLayout.InlineBlock)
								writer.WriteNewLineAndIndent();
						}

						
					}

					writer.Write(line);
				}
			}


		}

		/// <summary>
		/// true if contains no items (IfTest) and has no wrapper
		/// </summary>
		/// <returns></returns>
		public override bool HasRenderContent
		{
			get
			{
				return HasRenderContentInSet;
			}
		}

		#endregion

		#region IScriptIfCondition
		/// <summary>
		/// Fail test if empty or all contained ScriptItems fail the iftest
		/// </summary>
		/// <returns></returns>
		public override bool ScriptIfResult
		{
			get
			{
				return HasRenderContentInSet;
			}
		}

		/// <summary>
		/// True if any of the items in the set contain any render content
		/// </summary>
		public bool HasRenderContentInSet
		{
			get
			{
				foreach (object o in this)
				{
					if (Sb.HasRenderContent(o))
						return true;

				}
				return false; // didn't find anything
			}
		}
		#endregion

		#region Parameterised Range Adding
		/// <summary>
		/// Lets you insert multiple items
		/// </summary>
		/// <param name="items"></param>
		public void InsertRange(params object[] items)
		{
			this.Items.InsertRange(0, items);

#if DEBUG
			Assert();
#endif
		}

		/// <summary>
		/// Lets you add multiple items
		/// </summary>
		/// <param name="items"></param>
		public void AddRange(params object[] items)
		{
			this.Items.AddRange(items);

#if DEBUG
			Assert();
#endif
		}

		#endregion

		#region Registration Support
		private Dictionary<string, object> _Registry = null;

		/// <summary>
		/// Only add it if its not already been registered
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool RegisterItem(string name, object item)
		{
			if (_Registry == null)
				_Registry = new Dictionary<string, object>();
			else
			{
				if (_Registry.ContainsKey(name))
					return false;
			}

			_Registry.Add(name, item);

			this.Add(item);

			return true;

		}

		#endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.Items).GetEnumerator();
        }

        #endregion

		#region IEnumerable<object> Members

		IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            return this.Items.GetEnumerator();
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
            return this.Items.IndexOf(item);
        }
		/// <summary>
		/// Inserts an item at the specified index
		/// </summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		public void Insert(int index, object item)
        {
            this.Items.Insert(index, item);

#if DEBUG
			Assert();
#endif
        }

		/// <summary>
		/// Removes the first occurrence of a specific object 
		/// </summary>
		/// <param name="index"></param>
        public void RemoveAt(int index)
        {
            this.Items.RemoveAt(index);
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
                return this.Items[index];
            }
            set
            {
                this.Items[index] = value;


#if DEBUG
				Assert();
#endif
            }
        }

        #endregion

		#region ICollection<object> Members

		/// <summary>
		/// Removes all items 
		/// </summary>
		public void Clear()
        {
            this.Items.Clear();
        }

		/// <summary>
		/// Determines whether the set contains a specific item.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(object item)
        {
            return this.Items.Contains(item);
        }

		/// <summary>
		/// Copies the elements of the set to an Array, starting at a particular Array index.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(object[] array, int arrayIndex)
        {
            this.Items.CopyTo(array, arrayIndex);
        }

		/// <summary>
		/// how many items are in the set
		/// </summary>
        public int Count
        {
            get { return this.Items.Count; }
        }

		/// <summary>
		/// False
		/// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

		/// <summary>
		/// Removes the first occurrence of a specific object 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Remove(object item)
        {
            return this.Items.Remove(item);
        }

		/// <summary>
		/// Adds an item to the end of the set
		/// </summary>
		/// <param name="item"></param>
		public void Add(object item)
        {
            this.Items.Add(item);

#if DEBUG
			Assert();
#endif
        }

        #endregion

		#region Debug
#if DEBUG
		/// <summary>
		/// Check that the item does not contain itself
		/// </summary>
		protected void Assert()
		{
			foreach (IScriptItem i in AllItems)
			{
				if (object.ReferenceEquals(this, i))
					throw new Exception("ScriptSet contains itself");
			}
		}
		/// <summary>
		/// Recursively gather all ScriptItems contained within this one
		/// </summary>
		public List<IScriptItem> AllItems
		{
			get
			{
				List<IScriptItem> list = new List<IScriptItem>();

				foreach (object o in this)
				{
					if (o is IScriptItem)
					{
						list.Add((IScriptItem)o);

						if (o is ScriptSet)
						{
							list.AddRange(((ScriptSet)o).AllItems);
						}
					}
				}

				return list;
			}
		}

#endif
		#endregion

		#region ToString
		/// <summary>
		/// Add collection size
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return base.ToString() + " ("+ this.Count+")";
		}
		#endregion
	}
}
