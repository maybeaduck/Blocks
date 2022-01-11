 // Toony Colors Pro+Mobile 2
// (c) 2014-2019 Jean Moreno

Shader "LFH/SliderRamp6Sides"
{
	Properties
	{
	[TCP2HeaderHelp(BASE, Base Properties)]
		//TOONY COLORS
		_Color ("Color", Color) = (1,1,1,1)
		_HColor ("Highlight Color", Color) = (0.785,0.785,0.785,1.0)
		_SColor ("Shadow Color", Color) = (0.195,0.195,0.195,1.0)

		//DIFFUSE
		_MainTex ("Main Texture", 2D) = "white" {}
		_up ("Up", 2D) = "white" {}
		_down ("Down", 2D) = "white" {}
		_left ("Left", 2D) = "white" {}
		_right ("Right", 2D) = "white" {}
		_front ("Front", 2D) = "white" {}
		_back ("Back", 2D) = "white" {}
		
		_upMask ("UpMask", 2D) = "white" {}
		_downMask ("DownMask", 2D) = "white" {}
		_leftMask ("LeftMask", 2D) = "white" {}
		_rightMask ("RightMask", 2D) = "white" {}
		_frontMask ("FrontMask", 2D) = "white" {}
		_backMask ("BackMask", 2D) = "white" {}
	[TCP2Separator]

		//TOONY COLORS RAMP
		[TCP2Header(RAMP SETTINGS)]

		_RampThreshold ("Ramp Threshold", Range(0,1)) = 0.5
		_RampSmooth ("Ramp Smoothing", Range(0.001,1)) = 0.1
	[TCP2Separator]


		//Avoid compile error if the properties are ending with a drawer
		[HideInInspector] __dummy__ ("unused", Float) = 0
	}

	SubShader
	{

		Tags { "RenderType"="Opaque" }

		CGPROGRAM

		#pragma surface surf ToonyColorsCustom  exclude_path:deferred exclude_path:prepass
		#pragma target 3.0

		//================================================================
		// VARIABLES

		fixed4 _Color;
		sampler2D _MainTex;
		sampler2D _up;
		sampler2D _down;
		sampler2D _left;
		sampler2D _right;
		sampler2D _front;
		sampler2D _back;
		sampler2D _upMask;
		sampler2D _downMask;
		sampler2D _leftMask;
		sampler2D _rightMask;
		sampler2D _frontMask;
		sampler2D _backMask;

		#define UV_MAINTEX uv_MainTex
		// #define UV_UP _up
		// #define UV_DOWN _down
		// #define UV_LEFT _left
		// #define UV_RIGHT _right
		// #define UV_FRONT _front
		

		struct Input
		{
			half2 uv_MainTex;
			half2 uv_up;
			half2 uv_down;
			half2 uv_left;
			half2 uv_right;
			half2 uv_front;
			half2 uv_back;
			float2  _upMask;
			float2  _downMask;
			float2  _leftMask;
			float2  _rightMask;
			float2  _frontMask;
			float2  _backMask;
		};

		//================================================================
		// CUSTOM LIGHTING

		//Lighting-related variables
		fixed4 _HColor;
		fixed4 _SColor;
		half _RampThreshold;
		half _RampSmooth;

		// Instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)
		
		//Custom SurfaceOutput
		struct SurfaceOutputCustom
		{
			half atten;
			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
			half Specular;
			fixed Gloss;
			fixed Alpha;
		};
		
		inline half4 LightingToonyColorsCustom (inout SurfaceOutputCustom s, half3 viewDir, UnityGI gi)
		{
		#define IN_NORMAL s.Normal
	
			half3 lightDir = gi.light.dir;
		#if defined(UNITY_PASS_FORWARDBASE)
			half3 lightColor = _LightColor0.rgb;
			half atten = s.atten;
		#else
			half3 lightColor = gi.light.color.rgb;
			half atten = 1;
		#endif

			IN_NORMAL = normalize(IN_NORMAL);
			fixed ndl = max(0, dot(IN_NORMAL, lightDir));
			#define NDL ndl

			#define		RAMP_THRESHOLD	_RampThreshold
			#define		RAMP_SMOOTH		_RampSmooth

			fixed3 ramp = smoothstep(RAMP_THRESHOLD - RAMP_SMOOTH*0.5, RAMP_THRESHOLD + RAMP_SMOOTH*0.5, NDL);
		#if !(POINT) && !(SPOT)
			ramp *= atten;
		#endif
		// Note: we consider that a directional light with a cookie is supposed to be the main one (even though Unity renders it as an additional light).
		// Thus when using a main directional light AND another directional light with a cookie, then the shadow color might be applied twice.
		// You can remove the DIRECTIONAL_COOKIE check below the prevent that.
		#if !defined(UNITY_PASS_FORWARDBASE) && !defined(DIRECTIONAL_COOKIE)
			_SColor = fixed4(0,0,0,1);
		#endif
			_SColor = lerp(_HColor, _SColor, _SColor.a);	//Shadows intensity through alpha
			ramp = lerp(_SColor.rgb, _HColor.rgb, ramp);
			fixed4 c;
			c.rgb = s.Albedo * lightColor.rgb * ramp;
			c.a = s.Alpha;

		#ifdef UNITY_LIGHT_FUNCTION_APPLY_INDIRECT
			c.rgb += s.Albedo * gi.indirect.diffuse;
		#endif

			return c;
		}

		void LightingToonyColorsCustom_GI(inout SurfaceOutputCustom s, UnityGIInput data, inout UnityGI gi)
		{
			gi = UnityGlobalIllumination(data, 1.0, IN_NORMAL);

			s.atten = data.atten;	//transfer attenuation to lighting function
			gi.light.color = _LightColor0.rgb;	//remove attenuation
		}

		//================================================================
		// SURFACE FUNCTION
		fixed4 paintTexture(Input IN,fixed4 up,fixed4 down,fixed4 left,fixed4 right,fixed4 front,fixed4 back)
		{
			
			fixed4 upTex = up;
			fixed4 tex = lerp(upTex,up,tex2D(_upMask,IN._upMask).a);
			tex = lerp(tex,down,tex2D(_downMask,IN._downMask).a);
			tex = lerp(tex,left,tex2D(_leftMask,IN._leftMask).a);
			tex = lerp(tex,right,tex2D(_rightMask,IN._rightMask).a);
			tex = lerp(tex,front,tex2D(_frontMask,IN._frontMask).a);
			tex = lerp(tex,back,tex2D(_backMask,IN._backMask).a);
			return tex;
		}
		void surf(Input IN, inout SurfaceOutputCustom o)
		{
			
			 // mainTex = tex2D(_MainTex, IN.UV_MAINTEX);
			fixed4 up = tex2D(_up,IN.uv_up);
			fixed4 down = tex2D(_down,IN.uv_down);
			fixed4 left = tex2D(_left,IN.uv_left);
			fixed4 right = tex2D(_right,IN.uv_right);
			fixed4 front = tex2D(_front,IN.uv_front);
			fixed4 back = tex2D(_back,IN.uv_back);
			fixed4 mainTex = paintTexture(IN,up,down,left,right,front,back);
			o.Albedo = mainTex.rgb * _Color.rgb;
			o.Alpha = mainTex.a * _Color.a;
		}

		ENDCG
	}

	Fallback "Diffuse"
	CustomEditor "TCP2_MaterialInspector_SG"
}
