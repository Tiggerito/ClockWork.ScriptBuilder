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
	/// Renders parameter syntax:
	/// (param, param, param)
	/// </summary>
	public class JsParameters : ScriptSetWrapper
	{
		#region Constructors

		/// <summary>
		/// Create an empty parameter list
		/// ()
		/// </summary>
		public JsParameters() 
		{
			List = new JsList();
		}

		/// <summary>
		/// Create a parameter list
		/// (param, param, param)
		/// </summary>
		/// <param name="items"></param>
        public JsParameters(IEnumerable<object> items)
		{
			List = new JsList(items);
		}

		/// <summary>
		/// Create a parameter list
		/// (param, param, param)
		/// </summary>
		/// <param name="items"></param>
		public JsParameters(params object[] items)
		{
			List = new JsList(items);
		}


		/// <summary>
		/// Create a parameter list
		/// (param, param, param)
		/// </summary>
		/// <param name="list"></param>
		public JsParameters(JsList list)
		{
			List = list;
		}
		/// <summary>
		/// Create a parameter list
		/// (param, param, param)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="list"></param>
		public JsParameters(ScriptLayout layout, JsList list)
			: base(layout)
		{
			List = list;
		}

		/// <summary>
		/// Create an empty parameter list
		/// ()
		/// </summary>
		/// <param name="layout"></param>
		public JsParameters(ScriptLayout layout)
			: base(layout)
		{
			List = new JsList();
		}

		/// <summary>
		/// Create a parameter list
		/// (param, param, param)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="items"></param>
		public JsParameters(ScriptLayout layout, IEnumerable<object> items)
			: base(layout)
		{
			List = new JsList(items);
		}

		/// <summary>
		/// Create a parameter list
		/// (param, param, param)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="items"></param>
		public JsParameters(ScriptLayout layout, params object[] items)
			: base(layout)
		{
			List = new JsList(items);
		}
		#endregion

		#region Initialisation

		/// <summary>
		/// wrap the parameters in brackets
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
		/// the underlying list of parameters
		/// </summary>
		public JsList List
		{
			get { return Set as JsList; }
			set { Set = value; }
		}
		#endregion

		#region Layout Control
		private ScriptLayout _ListLayout = ScriptLayout.None;
		/// <summary>
		/// The layout to apply to the list if not already stated
		/// </summary>
		public ScriptLayout ListLayout
		{
			get { return _ListLayout; }
			set
			{
				if (_ListLayout != value)
				{
					_ListLayout = value;
				}
			}
		}

		/// <summary>
		/// base the layout of the parameter list on our layout
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLayoutChanged(LayoutChangedEventArgs e)
		{
			base.OnLayoutChanged(e);

			ScriptLayout layout = e.Layout;

			switch (layout)
			{
				case ScriptLayout.None:
					break;
				case ScriptLayout.NotAlreadyEstablished:
					break;
				case ScriptLayout.Default:
					break;
				case ScriptLayout.Inline:
					ListLayout =  ScriptLayout.Inline;
					break;
				case ScriptLayout.InlineBlock:
					ListLayout = ScriptLayout.Block;
					break;
				case ScriptLayout.Block:
					ListLayout = ScriptLayout.Block;
					break;
				default:
					break;
			}
		}

		#endregion

		#region Rendering
		/// <summary>
		/// render the parameters
		/// (param, param, param)
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
		{
			base.OnRender(e);

			Sb.TrySetLayout(List, ListLayout);

		}
		#endregion

	}
}
