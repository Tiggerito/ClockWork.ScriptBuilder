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


namespace ClockWork.ScriptBuilder.JavaScript.ExtJs
{
	/// <summary>
	/// Script Item designed to help construct the class pattern often used in ExtJs
	/// aka pre-configured classes
	/// </summary>
    public class ExtJsClass : ScriptItem
	{
		#region Constructors
		/// <summary>
		/// Create a Class pattern
		/// Established the namespace (Ext.ns)
		/// Extends the base class (Ext.extend)
		/// </summary>
		/// <param name="className">the name (including namespace) for the new class</param>
		/// <param name="baseClass">the class this inherits from</param>
		/// <param name="parameters">The parameters for the constructor function</param>
		/// <param name="constructor">the constructor code</param>
        public ExtJsClass(object className, object baseClass, JsParameters parameters, JsBlock constructor)
			: base()
        {
            ClassName = className;
            BaseClass = baseClass;
			Parameters = parameters;
			Constructor = constructor;
		}
		#endregion

		#region Data
        private object _ClassName;
		/// <summary>
		/// the name (including namespace) for the new class
		/// </summary>
        public object ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }

        private object _BaseClass;
		/// <summary>
		/// the class this inherits from
		/// </summary>
        public object BaseClass
        {
            get { return _BaseClass; }
            set { _BaseClass = value; }
        }

		private JsParameters _Parameters;
		/// <summary>
		/// paramters for the constuctor
		/// </summary>
		public JsParameters Parameters
		{
			get 
			{
				if (_Parameters == null)
					_Parameters = Js.Parameters();
				return _Parameters; 
			}
			set { _Parameters = value; }
		}


		private JsBlock _Constructor;
		/// <summary>
		/// Constructor code
		/// </summary>
		public JsBlock Constructor
		{
			get 
			{ 
				if (_Constructor==null)
					_Constructor = Js.Block();
				return _Constructor; 
			}
			set { _Constructor = value; }
		}


		#endregion



		#region Rendering
		/// <summary>
		/// render the class
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
		{
			base.OnRender(e);

            // work out the class name
            StringWriter tw = new StringWriter();
            ScriptWriter sw = new ScriptWriter(tw, e.Writer); // use same format provider

            sw.Write(this.ClassName);

            string className = tw.ToString();

            Script script = Sb.Script();

            string nameSpace = ExtJs.GetNamespace(className);

			Script constructor = Sb.Script();

			if (!String.IsNullOrEmpty(nameSpace))
                script.Add(Js.Statement(Js.Call("Ext.ns", Js.Q(nameSpace)))); // register namespace

            script.AddRange(
                Js.Statement(ClassName, " = ",
					Js.Function(ScriptLayout.InlineBlock,
						Parameters,
						Constructor
					)
				),
				Js.Statement(Js.Call("Ext.extend",ClassName,BaseClass)) // make it inherit from base class
			
            );

            e.Writer.Write(script);
		}
		#endregion
	}
}