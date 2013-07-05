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
using System.Text;

// TODO: look into serialisation of .Net objects
// check out JavaScriptConverter and related classes from the Framework
// maybe have a ScriptItem that wraps objects and converts them to objects

namespace ClockWork.ScriptBuilder.JavaScript
{
	/// <summary>
	/// Renders object syntax:
	/// {property, property, property}
	/// </summary>
	public class JsObject : ScriptSetWrapper
	{
		#region Constructors

		/// <summary>
		/// create an empty object
		/// {}
		/// </summary>
		public JsObject() 
		{
			Properties = new JsPropertyList();
		}
		/// <summary>
		/// create an object with properties
		/// {name: value, name: value, name: value}
		/// </summary>
		/// <param name="properties"></param>
        public JsObject(IEnumerable<object> properties)
		{
			Properties = new JsPropertyList(properties);
		}

		/// <summary>
		/// create an object with properties
		/// {name: value, name: value, name: value}
		/// </summary>
		/// <param name="items"></param>
		public JsObject(params object[] items)
		{
			Properties = new JsPropertyList(items);
		}

		/// <summary>
		/// create an object with properties
		/// {name: value, name: value, name: value}
		/// </summary>
		/// <param name="list"></param>
		public JsObject(JsPropertyList list)
		{
			Properties = list;
		}

		/// <summary>
		/// create an object with properties
		/// {name: value, name: value, name: value}
		/// </summary>
		/// <param name="layout"></param>
		public JsObject(ScriptLayout layout)
			: base(layout)
		{
			Properties = new JsPropertyList();
		}

		/// <summary>
		/// create an object with properties
		/// {name: value, name: value, name: value}
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="items"></param>
		public JsObject(ScriptLayout layout, IEnumerable<object> items)
			: base(layout)
		{
			Properties = new JsPropertyList(items);
		}

		/// <summary>
		/// create an object with properties
		/// {name: value, name: value, name: value}
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="items"></param>
		public JsObject(ScriptLayout layout, params object[] items)
			: base(layout)
		{
			Properties = new JsPropertyList(items);
		}

		/// <summary>
		/// create an object with properties
		/// {name: value, name: value, name: value}
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="list"></param>
		public JsObject(ScriptLayout layout, JsPropertyList list)
			: base(layout)
		{
			Properties = list;
		}
		#endregion

		#region Initialisation

		/// <summary>
		/// Wraps the items in curlies
		/// </summary>
		protected override void OnInitialise()
        {
            base.OnInitialise();

            this.SetWrapper("{", "}");
			this.InternalIndents = 1;
		}
		#endregion

		#region Data

		/// <summary>
		/// the properties in the object
		/// </summary>
		public JsPropertyList Properties
		{
			get { return Set as JsPropertyList; }
			set
			{
				Set = value;
			}
		}
		#endregion
	}
}