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
using System.IO;

namespace ClockWork.ScriptBuilder
{
	/// <summary>
	/// Provides a quick way to create Script Items
	/// This makes script building code more readable
	/// </summary>
	public class Sb
	{
		#region Script
		/// <summary>
		/// A collection of objects which will each be rendered on a new line
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="lines">collection of objects to render</param>
		/// <returns></returns>
		public static Script Script(ScriptLayout layout, params object[] lines)
		{
			return new Script(layout, lines);

		}
		/// <summary>
		/// A collection of objects which will each be rendered on a new line
		/// </summary>
		/// <param name="lines">collection of objects to render</param>
		/// <returns></returns>
		public static Script Script(params object[] lines)
		{
			return new Script(lines);
		}
		#endregion

		#region ScriptLine
		/// <summary>
		/// A collection of objects which will each be rendered on a single line
		/// They will be concatinated together
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="parts">the objects to be concatinated</param>
		/// <returns></returns>
		public static ScriptLine Line(ScriptLayout layout, params object[] parts)
		{
			return new ScriptLine(layout, parts);
		}
		/// <summary>
		/// A collection of objects which will each be rendered on a single line
		/// They will be concatinated together
		/// </summary>
		/// <param name="parts">the objects to be concatinated</param>
		/// <returns></returns>
		public static ScriptLine Line(params object[] parts)
		{
			return new ScriptLine(parts);
		}
		#endregion

		#region ScriptIf
		/// <summary>
		/// This  item lets you place if-then-else like logic within a script. The logic is only tested at render time.
		/// When rendering this item will check if the test objects results in a true or false
		/// It will then render the object related to that state
		/// It the test object implements IScriptIfCondition the test result is based on its ScriptIfResult property
		/// Otherwise the test object is converted to a boolean in the normal way
		/// A null test object returns false.
		/// </summary>
		/// <param name="test">object to test to see which of the other objects to render</param>
		/// <param name="trueValue">object to render if test returns true</param>
		/// <param name="falseValue">object to render if test returns false</param>
		/// <returns></returns>
		public static ScriptIf ScriptIf(object test, object trueValue, object falseValue)
		{
			return new ScriptIf(test, trueValue, falseValue);
		}
		/// <summary>
		/// This  item lets you place if-then like logic within a script. The logic is only tested at render time.
		/// When rendering this item will check if the test objects results in a true
		/// It will then render the trueValue object, oherwise it renders nothing
		/// It the test object implements IScriptIfCondition the test result is based on its ScriptIfResult property
		/// Otherwise the test object is converted to a boolean in the normal way
		/// A null test object result sin nothing being renderred.
		/// </summary>
		/// <param name="test">object to test. if it results in a true then render the trueValue object</param>
		/// <param name="trueValue">object to render if the test returns true</param>
		/// <returns></returns>
		public static ScriptIf ScriptIf(object test, object trueValue)
		{
			return new ScriptIf(test, trueValue);
		}
		#endregion

		#region ScriptIndent
		/// <summary>
		/// A Script that automatically indents its content
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="lines">the lines of the script</param>
		/// <returns></returns>
		public static ScriptIndent Indent(ScriptLayout layout, params object[] lines)
		{
			return new ScriptIndent(layout, lines);
		}
		/// <summary>
		/// A Script that automatically indents its content
		/// </summary>
		/// <param name="lines">the lines of the script</param>
		/// <returns></returns>
		public static ScriptIndent Indent(params object[] lines)
		{
			return new ScriptIndent(lines);
		}
		#endregion

		#region Wrapper
		/// <summary>
		/// An item that lets you wrap objects before and/or after another item
		/// Useful to create effects like surrounding brackets
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="before">object to render before the item</param>
		/// <param name="item">the item to render</param>
		/// <param name="after">object to render after the item</param>
		/// <returns></returns>
		public static ScriptWrapper Wrapper(ScriptLayout layout, object before, IScriptItem item, object after)
		{
			return new ScriptWrapper(layout, before, item, after);
		}
		/// <summary>
		/// An item that lets you wrap objects before and/or after another item
		/// Useful to create effects like surrounding brackets
		/// </summary>
		/// <param name="before">object to render before the item</param>
		/// <param name="item">the item to render</param>
		/// <param name="after">object to render after the item</param>
		/// <returns></returns>
		public static ScriptWrapper Wrapper(object before, IScriptItem item, object after)
		{
			return new ScriptWrapper(before, item, after);
		}
		#endregion

		#region Layout Control
		/// <summary>
		/// Helper to try and set a layout for an item
		/// First checks if the sent object is an IScriptItem
		/// </summary>
		/// <param name="item">object to check</param>
		/// <param name="layout">preferred layout</param>
		public static void TrySetLayout(object item, ScriptLayout layout)
		{
			ScriptItem.ObjectTrySetLayout(item, layout);
		}



		#endregion

        #region Compressible
        /// <summary>
        /// Lets you vary the text rendered based on the compressvalue in the writer
        /// </summary>
        /// <param name="full"></param>
        /// <param name="compressed"></param>
        /// <returns></returns>
        public static ScriptCompressible Compressible(object full, object compressed)
        {
            return new ScriptCompressible(full, compressed);
        }
        #endregion

		#region Rendering
		/// <summary>
		/// Helper to test if an object will render as more than an empty string
		/// </summary>
		/// <param name="item">object to check</param>
		/// <returns></returns>
		/// 
		public static bool HasRenderContent(object item)
		{
			return ScriptItem.ObjectHasRenderContent(item);
		}

        /// <summary>
        /// Provide a string representation of the object
        /// using ScriptItem rendering if available
        /// </summary>
        /// <param name="o">the object to render</param>
        /// <returns></returns>
        public static string Render(object o)
        {
            return Render(o, 0);
        }

        /// <summary>
        /// Provide a string representation of the object
        /// using ScriptItem rendering if available
        /// </summary>
        /// <param name="o">the object to render</param>
        /// <param name="indentations">starting indentation level</param>
        /// <returns></returns>
        public static string Render(object o, int indentations)
        {
            if (o == null)
                return String.Empty;

            StringWriter tw = new StringWriter();
            ScriptWriter sw = new ScriptWriter(tw);
            sw.CurrentIndentLevel = indentations;
            sw.Write(o);

            return tw.ToString();
        }
        #endregion
	}

}
