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
	/// A writer used by ScriptItems
	/// Supports indentation and formatting
	/// </summary>
	public interface IScriptWriter
	{
		#region Indentation
		/// <summary>
		/// Start a new level of indentation
		/// </summary>
		void BeginIndent();

		/// <summary>
		/// reduce the level of indentation
		/// </summary>
		void EndIndent();

		/// <summary>
		/// increase the level of indentations
		/// </summary>
		/// <param name="levels">how many levels to increase by</param>
		void BeginIndent(int levels);
		/// <summary>
		/// decrease the level of indentations
		/// </summary>
		/// <param name="levels">how many levels to decrease by</param>
		void EndIndent(int levels);

		/// <summary>
		/// The current level of indents in use
		/// </summary>
		Int32 CurrentIndentLevel { get;}
		#endregion

		#region Writers
		/// <summary>
		/// The standard wat to move to the next line and pre indent so content is in the correct place
		/// </summary>
		void WriteNewLineAndIndent();

		/// <summary>
		/// Write the given object.
		/// Content should be formatted using the format method
		/// </summary>
		/// <param name="o"></param>
		void Write(object o);
		#endregion

		#region Formatting

        /// <summary>
        /// The format provider to use when formatting objects
        /// </summary>
        IFormatProvider FormatProvider { get;}

		/// <summary>
		/// Convert an object to string form writing
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		string Format(object o);

        /// <summary>
        /// writer requests that compressed content is provided when writing
        /// </summary>
        bool Compress { get;}
		#endregion

		#region Stream Handing
		/// <summary>
		/// Clears all buffers for the current writer and causes any buffered data to be written to the underlying device.
		/// </summary>
		void Flush();

		/// <summary>
		/// Closes the current writer and releases any system resources associated with the writer. 
		/// </summary>
		void Close();
		#endregion

	}
}
