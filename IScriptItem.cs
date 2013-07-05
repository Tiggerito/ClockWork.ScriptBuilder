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
using System.Web;
using System.IO;

namespace ClockWork.ScriptBuilder
{
	/// <summary>
	/// Defines an item that knows how to render itself
	/// Also supports defining a layout style and providing 
	/// </summary>
	public interface IScriptItem
	{
		#region Rendering
		/// <summary>
		/// provide a string representation of the item using a default writer
		/// </summary>
		/// <returns></returns>
//		string Render();

		string Render(IFormatProvider formatProvider);

		/// <summary>
		/// Write the string rendering of an item to a ScriptWriter
		/// </summary>
		/// <param name="writer"></param>
		void Render(IScriptWriter writer);

		/// <summary>
		/// Write the string rendering of an item to a ScriptWriter with the specidifed indentation
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="indents"></param>
		void Render(IScriptWriter writer, int indents);

		/// <summary>
		/// Return true if the item will render to a string that is not empty
		/// </summary>
        bool HasRenderContent {get;}
		#endregion

		#region Layout Control
		/// <summary>
		/// The way this item should be rendered
		/// </summary>
		ScriptLayout Layout { get; }

		/// <summary>
		/// Indicate a layout. Should set the layout if it has not already been decided
		/// </summary>
		/// <param name="layout"></param>
		void TrySetLayout(ScriptLayout layout);
		#endregion
	}
}
