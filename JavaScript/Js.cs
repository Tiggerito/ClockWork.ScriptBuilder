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

namespace ClockWork.ScriptBuilder.JavaScript
{
	// TODO:
	// for loops
	// comments
	// do while
	// with
	// regex
	// switch
	// try catch finally

	/// <summary>
	/// Provides a quick way to create Js Items
	/// This makes script building code more readable
	/// </summary>
	public class Js
	{
		#region Quote
		/// <summary>
		/// Wrap single quotes round an item
		/// Short hand for Quote()
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static JsQuote Q(object text)
		{
			return new JsQuote(text, false);
		}

		/// <summary>
		/// Wrap single quotes round an item
		/// Short hand is Q()
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
        public static JsQuote Quote(object text)
		{
			return new JsQuote(text, false);
		}

		/// <summary>
		/// Wrap double quotes round an item
		/// Short hand for QuoteDouble()
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static JsQuote QQ(object text)
		{
			return new JsQuote(text, true);
		}
		/// <summary>
		/// Wrap double quotes round an item
		/// Short hand is QQ()
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static JsQuote QuoteDouble(object text)
		{
			return new JsQuote(text, true);
		}
		#endregion

		#region New
		/// <summary>
		/// Renders new syntax:
		/// new className(arg, arg, arg)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="className"></param>
		/// <param name="args"></param>
		/// <returns></returns>
        public static JsNew New(ScriptLayout layout, object className, params object[] args)
		{
			return new JsNew(layout, className, args);
		}
		/// <summary>
		/// Renders new syntax:
		/// new className(arg, arg, arg)
		/// </summary>
		/// <param name="className"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static JsNew New(object className, params object[] args)
		{
			return new JsNew(className, args);
		}
		#endregion

		#region Object
		/// <summary>
		/// Renders object syntax:
		/// {property, property, property}
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="properties"></param>
		/// <returns></returns>
		public static JsObject Object(ScriptLayout layout, params object[] properties)
		{
			return new JsObject(layout, properties);
		}
		/// <summary>
		/// Renders object syntax:
		/// {property, property, property}
		/// </summary>
		/// <param name="properties"></param>
		/// <returns></returns>
		public static JsObject Object(params object[] properties)
		{
			return new JsObject(properties);
		}
		#endregion

		#region Object Property
		/// <summary>
		/// Renders object property syntax:
		/// name: value
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <returns></returns>
        public static JsProperty Property(ScriptLayout layout, object name, object value)
		{
			return new JsProperty(layout, name, value);
		}
		/// <summary>
		/// Renders object property syntax:
		/// name: value
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static JsProperty Property(object name, object value)
		{
			return new JsProperty(name, value);
		}

		#endregion

		#region Array
		/// <summary>
		/// Renders array syntax:
		/// [item, item, item]
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="list"></param>
		/// <returns></returns>
		public static JsArray Array(ScriptLayout layout, params object[] list)
		{
			return new JsArray(layout, list);
		}
		/// <summary>
		/// Renders array syntax:
		/// [item, item, item]
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static JsArray Array(params object[] list)
		{
			return new JsArray(list);
		}
		/// <summary>
		/// Renders array syntax:
		/// [item, item, item]
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="list"></param>
		/// <returns></returns>
		public static JsArray Array(ScriptLayout layout, JsList list)
		{
			return new JsArray(layout, list);
		}
		/// <summary>
		/// Renders array syntax:
		/// [item, item, item]
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static JsArray Array(JsList list)
		{
			return new JsArray(list);
		}
		#endregion

		#region Block
		/// <summary>
		/// Renders block syntax:
		/// { 
		///		line 
		///		line 
		///		line 
		/// }
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="lines"></param>
		/// <returns></returns>
		public static JsBlock Block(ScriptLayout layout, params object[] lines)
		{
			return new JsBlock(layout, lines);
		}
		/// <summary>
		/// Renders block syntax:
		/// { 
		///		line 
		///		line 
		///		line 
		/// }
		/// </summary>
		/// <param name="lines"></param>
		/// <returns></returns>
		public static JsBlock Block(params object[] lines)
		{
			return new JsBlock(lines);
		}
		#endregion

		#region Function
		/// <summary>
		/// Renders function syntax:
		/// function name(param, param) { scriptBlock } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="name"></param>
		/// <param name="parameters"></param>
		/// <param name="scriptBlock"></param>
		/// <returns></returns>
        public static JsFunction Function(ScriptLayout layout, object name, JsParameters parameters, JsBlock scriptBlock)
		{
			return new JsFunction(layout, name, parameters, scriptBlock);
		}
		/// <summary>
		/// Renders function syntax:
		/// function name(param, param) { scriptBlock } 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="parameters"></param>
		/// <param name="scriptBlock"></param>
		/// <returns></returns>
        public static JsFunction Function(object name, JsParameters parameters, JsBlock scriptBlock)
		{
			return new JsFunction(name, parameters, scriptBlock);
		}
		/// <summary>
		/// Renders function syntax:
		/// function name() { scriptBlock } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="name"></param>
		/// <param name="scriptBlock"></param>
		/// <returns></returns>
        public static JsFunction Function(ScriptLayout layout, object name, JsBlock scriptBlock)
		{
			return new JsFunction(layout, name, scriptBlock);
		}
		/// <summary>
		/// Renders function syntax:
		/// function name() { scriptBlock } 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="scriptBlock"></param>
		/// <returns></returns>
        public static JsFunction Function(object name, JsBlock scriptBlock)
		{
			return new JsFunction(name, scriptBlock);
		}
		/// <summary>
		/// Renders function syntax:
		/// function(param, param) { scriptBlock } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="parameters"></param>
		/// <param name="scriptBlock"></param>
		/// <returns></returns>
		public static JsFunction Function(ScriptLayout layout, JsParameters parameters, JsBlock scriptBlock)
		{
			return new JsFunction(layout, parameters, scriptBlock);
		}
		/// <summary>
		/// Renders function syntax:
		/// function(param, param) { scriptBlock } 
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="scriptBlock"></param>
		/// <returns></returns>
		public static JsFunction Function(JsParameters parameters, JsBlock scriptBlock)
		{
			return new JsFunction(parameters, scriptBlock);
		}
		/// <summary>
		/// Renders function syntax:
		/// function() { scriptBlock } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="scriptBlock"></param>
		/// <returns></returns>
		public static JsFunction Function(ScriptLayout layout, JsBlock scriptBlock)
		{
			return new JsFunction(layout, scriptBlock);
		}
		/// <summary>
		/// Renders function syntax:
		/// function() { scriptBlock } 
		/// </summary>
		/// <param name="scriptBlock"></param>
		/// <returns></returns>
		public static JsFunction Function(JsBlock scriptBlock)
		{
			return new JsFunction(scriptBlock);
		}

		/// <summary>
		/// Renders function syntax:
		/// function name(param, param) { lines } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="name"></param>
		/// <param name="parameters"></param>
		/// <param name="lines"></param>
		/// <returns></returns>
        public static JsFunction Function(ScriptLayout layout, object name, JsParameters parameters, params object[] lines)
		{
			return new JsFunction(layout, name, parameters, lines);

		}

		/// <summary>
		/// Renders function syntax:
		/// function name(param, param) { lines } 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="parameters"></param>
		/// <param name="lines"></param>
		/// <returns></returns>
        public static JsFunction Function(object name, JsParameters parameters, params object[] lines)
		{
			return new JsFunction(name, parameters, lines);
		}

		/// <summary>
		/// Renders function syntax:
		/// function name() { lines } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="name"></param>
		/// <param name="lines"></param>
		/// <returns></returns>
        public static JsFunction Function(ScriptLayout layout, object name, params object[] lines)
		{
			return new JsFunction(layout, name, lines);
		}

		/// <summary>
		/// Renders function syntax:
		/// function name() { lines } 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="lines"></param>
		/// <returns></returns>
        public static JsFunction Function(object name, params object[] lines)
		{
			return new JsFunction(name, lines);
		}

		/// <summary>
		/// Renders function syntax:
		/// function(param, param) { lines } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="parameters"></param>
		/// <param name="lines"></param>
		/// <returns></returns>
		public static JsFunction Function(ScriptLayout layout, JsParameters parameters, params object[] lines)
		{
			return new JsFunction(layout, parameters, lines);
		}

		/// <summary>
		/// Renders function syntax:
		/// function(param, param) { lines } 
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="lines"></param>
		/// <returns></returns>
		public static JsFunction Function(JsParameters parameters, params object[] lines)
		{
			return new JsFunction(parameters, lines);
		}

		/// <summary>
		/// Renders function syntax:
		/// function() { lines } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="lines"></param>
		/// <returns></returns>
		public static JsFunction Function(ScriptLayout layout, params object[] lines)
		{
			return new JsFunction(layout, lines);
		}

		/// <summary>
		/// Renders function syntax:
		/// function() { lines } 
		/// </summary>
		/// <param name="lines"></param>
		/// <returns></returns>
		public static JsFunction Function(params object[] lines)
		{
			return new JsFunction(lines);
		}

		#endregion

		#region Call

		/// <summary>
		/// Renders function call syntax:
		/// functionName(arg, arg) 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="functionName"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static JsCall Call(ScriptLayout layout, object functionName, params object[] args)
		{
			return new JsCall(layout, functionName, args);
		}
		/// <summary>
		/// Renders function call syntax:
		/// functionName(arg, arg) 
		/// </summary>
		/// <param name="functionName"></param>
		/// <param name="args"></param>
		/// <returns></returns>
        public static JsCall Call(object functionName, params object[] args)
		{
			return new JsCall(functionName, args);
		}

		/// <summary>
		/// Renders function call syntax:
		/// functionName(arg, arg) 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="functionName"></param>
		/// <param name="arguments"></param>
		/// <returns></returns>
        public static JsCall Call(ScriptLayout layout, object functionName, JsArguments arguments)
		{
			return new JsCall(layout, functionName, arguments);
		}

		/// <summary>
		/// Renders function call syntax:
		/// functionName(arg, arg) 
		/// </summary>
		/// <param name="functionName"></param>
		/// <param name="arguments"></param>
		/// <returns></returns>
        public static JsCall Call(object functionName, JsArguments arguments)
		{
			return new JsCall(functionName, arguments);
		}

		#endregion

		#region List
		/// <summary>
		/// Renders list syntax:
		/// item, item, item 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="items"></param>
		/// <returns></returns>
		public static JsList List(ScriptLayout layout, params object[] items)
		{
			return new JsList(layout, items);
		}
		/// <summary>
		/// Renders list syntax:
		/// item, item, item 
		/// </summary>
		/// <param name="items"></param>
		/// <returns></returns>
		public static JsList List(params object[] items)
		{
			return new JsList(items);
		}
		#endregion

		#region Statement

		/// <summary>
		/// Renders statement syntax (concat items and add a semicolon):
		/// items; 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="items"></param>
		/// <returns></returns>
		public static JsStatement Statement(ScriptLayout layout, params object[] items)
		{
			return new JsStatement(layout, items);
		}
		/// <summary>
		/// Renders statement syntax (concat items and add a semicolon):
		/// items;
		/// </summary>
		/// <param name="items"></param>
		/// <returns></returns>
		public static JsStatement Statement(params object[] items)
		{
			return new JsStatement(items);
		}
		#endregion

		#region Parameters
		/// <summary>
		/// Renders parameter syntax:
		/// (param, param, param)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static JsParameters Parameters(ScriptLayout layout, params object[] parameters)
		{
			return new JsParameters(layout, parameters);
		}
		/// <summary>
		/// Renders parameter syntax:
		/// (param, param, param)
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static JsParameters Parameters(params object[] parameters)
		{
			return new JsParameters(parameters);
		}
		/// <summary>
		/// Renders parameter syntax:
		/// (param, param, param)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static JsParameters Parameters(ScriptLayout layout, JsList parameters)
		{
			return new JsParameters(layout, parameters);
		}
		/// <summary>
		/// Renders parameter syntax:
		/// (param, param, param)
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static JsParameters Parameters(JsList parameters)
		{
			return new JsParameters(parameters);
		}
		#endregion

		#region Arguments
		/// <summary>
		/// Renders argument syntax:
		/// (arg, arg, arg)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static JsArguments Arguments(ScriptLayout layout, params object[] args)
		{
			return new JsArguments(layout, args);
		}
		/// <summary>
		/// Renders argument syntax:
		/// (arg, arg, arg)
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static JsArguments Arguments(params object[] args)
		{
			return new JsArguments(args);
		}
		/// <summary>
		/// Renders argument syntax:
		/// (arg, arg, arg)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static JsArguments Arguments(ScriptLayout layout, JsList args)
		{
			return new JsArguments(layout, args);
		}
		/// <summary>
		/// Renders argument syntax:
		/// (arg, arg, arg)
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static JsArguments Arguments(JsList args)
		{
			return new JsArguments(args);
		}
		#endregion

		#region If
		/// <summary>
		/// Renders if syntax:
		/// if (condition) { trueItem } else { falseItem }
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="condition"></param>
		/// <param name="trueItem"></param>
		/// <param name="falseItem"></param>
		/// <returns></returns>
		public static JsIf If(ScriptLayout layout, object condition, object trueItem, object falseItem)
		{
			return new JsIf(layout, condition, trueItem, falseItem);
		}
		/// <summary>
		/// Renders if syntax:
		/// if (condition) { trueItem } else { falseItem }
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="trueItem"></param>
		/// <param name="falseItem"></param>
		/// <returns></returns>
		public static JsIf If(object condition, object trueItem, object falseItem)
		{
			return new JsIf(condition, trueItem, falseItem);
		}

		/// <summary>
		/// Renders if syntax:
		/// if (condition) { trueItem }
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="condition"></param>
		/// <param name="trueItem"></param>
		/// <returns></returns>
		public static JsIf If(ScriptLayout layout, object condition, object trueItem)
		{
			return new JsIf(layout, condition, trueItem);
		}

		/// <summary>
		/// Renders if syntax:
		/// if (condition) { trueItem } 
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="trueItem"></param>
		/// <returns></returns>
		public static JsIf If(object condition, object trueItem)
		{
			return new JsIf(condition, trueItem);
		}

		#endregion



        #region Rendering
        /// <summary>
        /// Provide a string representation of the object
        /// using a javascript format provider
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string Render(object o)
        {
            return Render(o, 0);
        }

        /// <summary>
        /// Provide a string representation of the object
        /// using a javascript format provider
        /// </summary>
        /// <param name="o"></param>
        /// <param name="indentations">number of indentations to start with</param>
        /// <returns></returns>
        public static string Render(object o, int indentations)
        {
            if (o == null)
                return String.Empty;

            StringWriter tw = new StringWriter();
            ScriptWriter sw = new ScriptWriter(tw, JsFormatProvider.Instance);
            sw.CurrentIndentLevel = indentations;
            sw.Write(o);

            return tw.ToString();
        }
        #endregion
	}

}
