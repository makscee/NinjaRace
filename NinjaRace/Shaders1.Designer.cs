﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NinjaRace {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Shaders {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Shaders() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NinjaRace.Shaders", typeof(Shaders).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uniform vec4 color;
        ///uniform float size;
        ///
        ///varying vec3 modelPos;
        ///
        ///void main()
        ///{
        ///	float m = 1;
        ///	m = min(m, 1.0 - modelPos.y);
        ///	m = min(m, 1.0 - modelPos.x);
        ///	m = min(m, modelPos.y);
        ///	m = min(m, modelPos.x);
        ///	gl_FragColor = vec4(color.x, color.y, color.z, 1.0 - m * (4 + size));
        ///}.
        /// </summary>
        internal static string BonusTile {
            get {
                return ResourceManager.GetString("BonusTile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uniform vec4 color;
        ///uniform float size;
        ///uniform float fade;
        ///
        ///varying vec3 modelPos;
        ///
        ///void main()
        ///{
        ///	float m = 1;
        ///	m = min(m, 1.0 - modelPos.y);
        ///	m = min(m, modelPos.y &gt;= fade ? 1.0 - modelPos.x : distance(modelPos.xy, vec2(1, fade)));
        ///	if(fade == 0)
        ///		m = min(m, modelPos.y);
        ///	m = min(m, modelPos.y &gt;= fade ? modelPos.x : distance(modelPos.xy, vec2(0, fade)));
        ///	gl_FragColor = vec4(color.x, color.y, color.z, 1.0 - m * (4 + size));
        ///}.
        /// </summary>
        internal static string CrackedTile {
            get {
                return ResourceManager.GetString("CrackedTile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uniform vec4 color;
        ///uniform float size;
        ///
        ///varying vec3 modelPos;
        ///
        ///void main()
        ///{
        ///	float m = 1;
        ///	m = min(m, 1.0 - modelPos.y);
        ///	m = min(m, 1.0 - modelPos.x);
        ///	m = min(m, modelPos.y);
        ///	m = min(m, modelPos.x);
        ///	gl_FragColor = vec4(color.x, color.y, color.z, 1.0 - m * (4 + size));
        ///}.
        /// </summary>
        internal static string FinishTile {
            get {
                return ResourceManager.GetString("FinishTile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uniform sampler2D texture;
        ///uniform vec2 size;
        ///uniform float doX;
        /// 
        ///varying vec3 modelPos;
        /// 
        ///void main() {
        ///	const int n = 10;
        /// 
        ///	vec3 res1 = vec3(0, 0, 0);
        ///	float res2 = 0.0;
        ///	float w1 = 0.0, w2 = 0.0;
        /// 
        ///	vec4 res = vec4(0, 0, 0, 0);
        ///	float w = 0.0;
        /// 
        ///	float gb = 0.0;
        /// 
        ///	for(int i = -n; i &lt;= n; i++) {
        ///		vec2 pos = modelPos.xy + vec2(1.0 / size.x * float(i) * doX, 1.0 / size.y * float(i) * (1.0 - doX));
        ///		pos = clamp(pos, 0.0, 1.0);
        ///		float k = exp(-abs(float(i)) / float(n));
        ///		vec4 colo [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Glow {
            get {
                return ResourceManager.GetString("Glow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uniform sampler2D texture;
        ///uniform vec2 size;
        ///uniform vec4 color;
        ///
        ///varying vec3 modelPos;
        ///
        ///void main()
        ///{
        ///	if(distance(modelPos.xy, vec2(0.5)) &gt; 0.5)
        ///		discard;
        ///	gl_FragColor = vec4(color.x, color.y, color.z, pow(1.0 - distance(modelPos.xy, vec2(0.5)) * 2.0, 3.0));
        ///}.
        /// </summary>
        internal static string GlowingParticle {
            get {
                return ResourceManager.GetString("GlowingParticle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uniform vec4 color;
        ///uniform float size;
        ///uniform float pulse;
        ///
        ///varying vec3 modelPos;
        ///
        ///void main()
        ///{
        ///	float m = 1;
        ///	m = min(m, 1.0 - modelPos.y);
        ///	m = min(m, modelPos.y);
        ///	m = min(m, 1.0 - modelPos.x);
        ///	m = min(m, modelPos.x);
        ///	m = min(m, abs((modelPos.x &gt; 0.5 ? (1 - modelPos.x) : modelPos.x) * 2 - modelPos.y + pulse));
        ///
        ///	gl_FragColor = vec4(color.x, color.y, color.z, 1.0 - m * (4 + size));
        ///}.
        /// </summary>
        internal static string JumpTile {
            get {
                return ResourceManager.GetString("JumpTile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uniform vec4 color;
        ///uniform float size;
        ///uniform float pulse;
        ///
        ///varying vec3 modelPos;
        ///
        ///void main()
        ///{
        ///	float m = 1.0;
        ///	float r = distance(modelPos.xy, vec2(cos(pulse) / 2.3 + 0.5, sin(pulse) / 2.3 + 0.5)) * 3;
        ///	r = min(r, distance(modelPos.xy, vec2(cos(pulse + 3.14) / 2.3 + 0.5, sin(pulse + 3.14) / 2.3 + 0.5)) * 3);
        ///	r = r &gt; 0.5 ? 0.5 : r;
        ///	float x = modelPos.x, y = modelPos.y;
        ///	m = min(m, abs(pow(x - 0.5, 2) + pow(y - 0.5, 2) - 0.15));
        ///
        ///	gl_FragColor = vec4(color.x + 0.5 - r, color.y, color.z, [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Saw {
            get {
                return ResourceManager.GetString("Saw", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uniform vec4 color;
        ///uniform float size;
        ///
        ///varying vec3 modelPos;
        ///
        ///void main()
        ///{
        ///	float m = 1.0;
        ///	m = min(m, modelPos.y);
        ///	m = min(m, abs((modelPos.x &gt; 0.5 ? (1 - modelPos.x) : modelPos.x) * 2 - modelPos.y));
        ///
        ///	gl_FragColor = vec4(color.x, color.y, color.z, 1.0 - m * (4 + size));
        ///}.
        /// </summary>
        internal static string Spikes {
            get {
                return ResourceManager.GetString("Spikes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uniform vec4 color;
        ///uniform float size;
        ///uniform int sides;
        ///
        ///varying vec3 modelPos;
        ///
        ///void main()
        ///{
        ///	float m = 1;
        ///	if(sides &amp; 1)
        ///		m = min(m, 1.0 - modelPos.y);
        ///	if(sides &amp; 2)
        ///		m = min(m, 1.0 - modelPos.x);
        ///	if(sides &amp; 4)
        ///		m = min(m, modelPos.y);
        ///	if(sides &amp; 8)
        ///		m = min(m, modelPos.x);
        ///	gl_FragColor = vec4(color.x, color.y, color.z, 1.0 - m * (4 + size));
        ///}.
        /// </summary>
        internal static string TileBorder {
            get {
                return ResourceManager.GetString("TileBorder", resourceCulture);
            }
        }
    }
}
