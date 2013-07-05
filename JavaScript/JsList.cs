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
	/// Renders a set so that each item is seperated by a comma
	/// item, item, item
	/// </summary>
    public class JsList : ScriptSet
	{
		#region Constructors

		/// <summary>
		/// Create an empty item
		/// </summary>
        public JsList() : base() { }
		/// <summary>
		/// Create an empty item using a custom layout
		/// </summary>
		/// <param name="layout">override the default layout</param>
		public JsList(ScriptLayout layout) : base(layout) { }

		/// <summary>
		/// Create a script from a collection of objects
		/// </summary>
		/// <param name="lines">collection of objects</param>
        public JsList(IEnumerable<object> lines) : base(lines) { }
		/// <summary>
		/// Create an item using a custom layout from a collection of objects
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="lines">collection of objects</param>
		public JsList(ScriptLayout layout, IEnumerable<object> lines) : base(layout, lines) { }

		/// <summary>
		/// Create an item from parameters, each representing a object in the item
		/// </summary>
		/// <param name="lines">set of paramters</param>
        public JsList(params object[] lines) : base(lines) { }
		/// <summary>
		/// Create an item using a custom layout from parameters
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="lines">set of paramters</param>
		public JsList(ScriptLayout layout, params object[] lines) : base(layout, lines) { }

		#endregion

		#region Initialisation

		/// <summary>
		/// Set the seperator to a comma
		/// </summary>
		protected override void OnInitialise()
		{
			base.OnInitialise();

			this.Seperator = ", ";
		}
		#endregion
	}
}