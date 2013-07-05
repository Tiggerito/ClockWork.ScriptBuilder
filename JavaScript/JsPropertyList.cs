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

namespace ClockWork.ScriptBuilder.JavaScript
{
	/// <summary>
	/// A collection designed to contains the JsProperty's of a JsObject
	/// Can contain JsObjects or arrays of items 
	/// </summary>
	public class JsPropertyList : JsList
	{
		#region Constructors

		/// <summary>
		/// Create an empty item
		/// </summary>
        public JsPropertyList() : base() { }
		/// <summary>
		/// Create an empty item using a custom layout
		/// </summary>
		/// <param name="layout">override the default layout</param>
		public JsPropertyList(ScriptLayout layout) : base(layout) { }

		/// <summary>
		/// Create a script from a collection of objects
		/// </summary>
		/// <param name="lines">collection of objects</param>
        public JsPropertyList(IEnumerable<object> lines) : base(lines) { }
		/// <summary>
		/// Create an item using a custom layout from a collection of objects
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="lines">collection of objects</param>
		public JsPropertyList(ScriptLayout layout, IEnumerable<object> lines) : base(layout, lines) { }

		/// <summary>
		/// Create an item from parameters, each representing a object in the item
		/// </summary>
		/// <param name="lines">set of paramters</param>
        public JsPropertyList(params object[] lines) : base(lines) { }
		/// <summary>
		/// Create an item using a custom layout from parameters
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="lines">set of paramters</param>
		public JsPropertyList(ScriptLayout layout, params object[] lines) : base(layout, lines) { }

		#endregion

		#region Helpers

		/// <summary>
		/// Find the first property of that name in the list
		/// Will recurse through inner JsObjects directly placed in the list
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public JsProperty FindProperty(string name)
		{
			return FindProperty(this, name);
		}

		/// <summary>
		/// Recursive search for a property
		/// </summary>
		/// <param name="o"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		private JsProperty FindProperty(object o, string name)
		{

			if (o is JsProperty)
			{
                if (Sb.Render(((JsProperty)o).Name) == name) // probably would struggle with compression or formatting variations
                    return (JsProperty)o;				
			}
			if (o is JsPropertyList)
			{
				foreach (object p in (JsPropertyList)o)
				{
					JsProperty pp = FindProperty(p, name);
					if (pp != null)
						return pp;
				}
			}
			if (o is JsObject)
			{
				return ((JsObject)o).Properties.FindProperty(name);
			}
			if (o is object[])
			{
				foreach (object p in (object[])o)
				{
					JsProperty pp = FindProperty(p, name);
					if (pp != null)
						return pp;
				}
			}
			return null;
		}

		/// <summary>
		/// Return the value of a property
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public object FindPropertyValue(string name)
		{
			JsProperty p = FindProperty(name);
			if (p != null)
				return p.Value;
			return null;
		}

		/// <summary>
		/// Add a property by its name and value
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void Add(string name, object value)
		{
			this.Add(Js.Property(name, value));
		}
		#endregion

		#region Rendering
		/// <summary>
		/// flatten out object in objects so they become one
		/// so we can combine objects/properties into resulting object 
		/// </summary>
		/// <returns></returns>
		protected override void AddToRenderList(List<object> dest, object o)
		{
			if (o is JsObject)
			{
				foreach (object p in ((JsObject)o).Properties)
				{
					AddToRenderList(dest, p);
				}
			}
			else
			{
				if (o is JsPropertyList)
				{
					foreach (object p in ((JsPropertyList)o).Items)
					{
						AddToRenderList(dest, p);
					}
				}
				else
					base.AddToRenderList(dest, o);
			}
		}
		#endregion
	}
}
