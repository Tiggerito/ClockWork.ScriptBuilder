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

using System.IO;

namespace ClockWork.ScriptBuilder
{

	/// <summary>
	/// This  item lets you place if-then-else like logic within a script. The logic is only tested at render time.
	/// When rendering this item will check if the test objects results in a true or false
	/// It will then render the object related to that state
	/// It the test object implements IScriptIfCondition the test result is based on its ScriptIfResult property
	/// Otherwise the test object is converted to a boolean in the normal way
	/// A null test object returns false.
	/// </summary>
    public class ScriptIf : ScriptItem
	{
		#region Constructors

		/// <summary>
		/// If the test object is an IScriptIfCondition then it tests using its ScriptIfResult method
		/// Otherwise we attempt to convert the object to a boolean using ConvertToBoolean()
		/// </summary>
		/// <param name="test">object to test against. must be convertable to a boolean or a IScriptIfCondition</param>
		/// <param name="trueValue">The object to render if the test return true</param>
		/// <param name="falseValue">The object to render if the test return false</param>
		public ScriptIf(object test, object trueValue, object falseValue)
            :base()
        {
			Test = test;
			FalseValue = falseValue;
			TrueValue = trueValue;
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
		public ScriptIf(object test, object trueValue)
			: base()
		{
			Test = test;
			TrueValue = trueValue;

		}
		#endregion

		#region Data
		private object _Test;
		/// <summary>
		/// The object to apply the test on.
		/// If its a ScriptItem the test is based on that items IfTest() method
		/// Otherwise we attempt to convert the object to a boolean using ConvertToBoolean()
		/// </summary>
		public object Test
		{
			get { return _Test; }
			set { _Test = value; }
		}
		private object _TrueValue;

		/// <summary>
		/// The object to render if the test return true
		/// </summary>
		public object TrueValue
		{
			get { return _TrueValue; }
			set { _TrueValue = value; }
		}

		private object _FalseValue;
		/// <summary>
		/// The object to render if the test return false
		/// </summary>
		public object FalseValue
		{
			get { return _FalseValue; }
			set { _FalseValue = value; }
		}

		#endregion

		#region Performing Test

		/// <summary>
		/// Runs the test and returns the result
		/// </summary>
		public bool TestResult
		{
			get
			{
				return ScriptIf.ObjectScriptIfResult(Test);
			}
		}

		/// <summary>
		/// Helper to perform the test on any object
		/// </summary>
		/// <param name="item">object to test</param>
		/// <returns></returns>
		public static bool ObjectScriptIfResult(object item)
		{

			if (item == null)
				return false;

			else if (item is IScriptIfCondition)
			{
				return ((IScriptIfCondition)item).ScriptIfResult;
			}
			else
			{
				try
				{
					return Convert.ToBoolean(item);
				}
				catch
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Returns the object to be used based on the test result
		/// </summary>
		public object Winner
		{
			get
			{
				return this.TestResult ? TrueValue : FalseValue;
			}
		}
		

		/// <summary>
		/// A ScriptIf will return true if the winner has content
		/// </summary>
		/// <returns></returns>
		public override bool ScriptIfResult
		{
			get
			{
				return HasRenderContent;
			}
		}
		#endregion

		#region Rendering
		/// <summary>
		/// Renders the winner of the test
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
        {
			base.OnRender(e);

			IScriptWriter writer = e.Writer;


			object winner = this.Winner;

			writer.Write(winner);

		}
		/// <summary>
		/// Based on the state of the winner
		/// </summary>
		/// <returns></returns>
		public override bool HasRenderContent
		{
			get
			{
				return Sb.HasRenderContent(this.Winner);
			}
		}
		#endregion
    }
}