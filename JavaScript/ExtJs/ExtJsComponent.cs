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


namespace ClockWork.ScriptBuilder.JavaScript.ExtJs
{
	/// <summary>
	/// Renders a component structure
	/// 
	/// Ref: http://extjs.com/forum/showthread.php?t=28085
	/// </summary>
	/// 
    public class ExtJsComponent : ScriptItem
	{
		#region Constructors
		
		/// <summary>
		/// Create a component called componentName based on the baseComponent component
		/// defined by componentObject
		/// </summary>
		/// <param name="componentName"></param>
		/// <param name="baseComponent"></param>
		/// <param name="componentObject"></param>
        public ExtJsComponent(object componentName, object baseComponent, JsObject componentObject)
        {
			Object = componentObject;
			ComponentName = componentName;
			BaseComponent = baseComponent;
		}
		#endregion

		#region Data

        private object _ComponentName;
		/// <summary>
		/// the name (including namespace) for the new class
		/// </summary>
        public object ComponentName
		{
			get { return _ComponentName; }
			set { _ComponentName = value; }
		}

        private object _BaseComponent;
		/// <summary>
		/// the class this inherits from
		/// </summary>
        public object BaseComponent
		{
			get { return _BaseComponent; }
			set { _BaseComponent = value; }
		}

        private JsObject _Object;
		/// <summary>
		/// Declare class properties and functions 
		/// These can be altered when creating instances of the class by parameters in the constructor
		/// You can also use them inside any class functions by using the "this" keyword.
		/// 
		/// Components have the following functions that can be overwritten
		///		initComponent
		///		onRender
		///		initEvents
		///		afterRender
		///		beforeDestroy
		///		onDestroy
		/// 
		/// make sure you call the base function in your overriding function e.g.
		/// ExtJs.BaseApply(this.ClassName, "initComponent")
		/// </summary>
        public JsObject Object
        {
            get
            {
                if (_Object == null)
					_Object = Js.Object(ScriptLayout.Block); 
                return _Object;
            }
            set { _Object = value; }
        }



        private string _RegistryName = null;
		/// <summary>
		/// If supplied, the  script will register the class as an xtype
		/// </summary>
        public string RegistryName
        {
            get { return _RegistryName; }
            set { _RegistryName = value; }
		}
		#endregion

		#region Rendering
		/// <summary>
		/// renders the component
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
		{
			base.OnRender(e);

            Script script = Sb.Script();

            // work out the component name
            StringWriter tw = new StringWriter();
            ScriptWriter sw = new ScriptWriter(tw, e.Writer); // use same format provider

            sw.Write(this.ComponentName);

            string componentName = tw.ToString();


            string nameSpace = ExtJs.GetNamespace(componentName);

			// namespace script
			if (!String.IsNullOrEmpty(nameSpace))
                script.Add(Js.Statement(Js.Call("Ext.ns", Js.Q(nameSpace)))); // register namespace

			// component script
            script.Add(
                Js.Statement(ComponentName, " = ",
                    Js.Call(ScriptLayout.InlineBlock,"Ext.extend",
                        Js.List(
                            this.BaseComponent,
							Js.Object(ScriptLayout.InlineBlock,
								this.Object
							)
                        )
                    )
                )
			);

			// register script
            if (!String.IsNullOrEmpty(this.RegistryName))
                script.Add(Js.Statement(Js.Call("Ext.reg", Js.Q(this.RegistryName), this.ComponentName))); // register xtype

            e.Writer.Write(script);

		}
		/// <summary>
		/// An example on creating a component
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		protected ExtJsComponent ComponentExample(IScriptWriter writer)
		{
			string componentName = "test";
			string baseComponent = "Ext.Window";

			return
				ExtJs.Component(
					componentName, baseComponent, // define the component name and the one it derives from

					Js.Object(
						// define properties of the component
						Js.Property("panel", "{}"), 

						// initComponent function. is the most commonly overriden one 
						Js.Property("initComponent", 
							Js.Function(ScriptLayout.InlineBlock,
								Js.Block(

									// initialise properties in the initComponent function
									Js.Statement("this.panel = ", Js.New("Ext.Panel")), 

									// apply changes to the components settings
									ExtJs.Apply(
										Js.Object(
											Js.Property("title",Js.Q("Test"))
										)
									),

									// always call the base function
									ExtJs.BaseApply(this.ComponentName, "initComponent") 
								)
							)
						)
					)
				);
		}
		#endregion
    }
}