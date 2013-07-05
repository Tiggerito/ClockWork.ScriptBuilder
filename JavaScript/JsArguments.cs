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

namespace ClockWork.ScriptBuilder.JavaScript
{

	/// <summary>
	/// argument syntax:
	/// (arg, arg, arg)
	/// </summary>
	public class JsArguments : ScriptSetWrapper
	{
		#region Constructors

		/// <summary>
		/// Create an empty set of arguments
		/// ()
		/// </summary>
		public JsArguments() 
		{
			List = new JsList(ScriptLayout.Inline);
		}
		/// <summary>
		/// Create a set of arguments
		/// (arg, arg, arg)
		/// </summary>
		/// <param name="args"></param>
        public JsArguments(IEnumerable<object> args)
		{
			List = new JsList(ScriptLayout.Inline, args);
		}
		/// <summary>
		/// Create a set of arguments
		/// (arg, arg, arg)
		/// </summary>
		/// <param name="items"></param>
		public JsArguments(params object[] items)
		{
			List = new JsList(ScriptLayout.Inline, items);
		}

		/// <summary>
		/// Create a set of arguments
		/// (arg, arg, arg)
		/// </summary>
		/// <param name="list"></param>
		public JsArguments(JsList list)
		{
			List = list;
		}

		/// <summary>
		/// Create a set of arguments
		/// (arg, arg, arg)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="list"></param>
		public JsArguments(ScriptLayout layout, JsList list)
			: base(layout)
		{
			List = list;
		}

		/// <summary>
		/// Create a set of arguments
		/// (arg, arg, arg)
		/// </summary>
		/// <param name="layout"></param>
		public JsArguments(ScriptLayout layout)
			: base(layout)
		{
			List = new JsList(ScriptLayout.Inline);
		}
		/// <summary>
		/// Create a set of arguments
		/// (arg, arg, arg)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="items"></param>
		public JsArguments(ScriptLayout layout, IEnumerable<object> items)
			: base(layout)
		{
			List = new JsList(ScriptLayout.Inline, items);
		}

		/// <summary>
		/// Create a set of arguments
		/// (arg, arg, arg)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="items"></param>
		public JsArguments(ScriptLayout layout, params object[] items)
			: base(layout)
		{
			List = new JsList(ScriptLayout.Inline, items);
		}

		#endregion

		#region Initialisation

		/// <summary>
		/// wrap the arguments in brackets
		/// </summary>
		protected override void OnInitialise()
        {
            base.OnInitialise();

            this.SetWrapper("(", ")");
			this.InternalIndents = 1;
		}
		#endregion

		#region Data
		/// <summary>
		/// The underlying list of arguments
		/// </summary>
		public JsList List
		{
			get { return Set as JsList; }
			set { Set = value; }
		}
		#endregion
	}
}
