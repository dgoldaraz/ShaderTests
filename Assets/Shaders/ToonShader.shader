Shader "MyShaders/Toon" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_RampTex("Ramp", 2D) = "white" {}
		_CelShadingLevels("CellShadingLevel", float) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// SimpleLambert model
		#pragma surface surf Toon

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _RampTex;
		fixed4 _Color;
		float _CelShadingLevels;

		struct Input {
			float2 uv_MainTex;
		};

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
		}

		half4 LightingToon(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			float NdotL = dot(s.Normal, lightDir);
			//NdotL = tex2D(_RampTex, fixed2(NdotL, 0.5));
			// This way avoid the use of a RampTex
			half cel = floor(NdotL * _CelShadingLevels) / (_CelShadingLevels - 0.5); //snap
			fixed4 c;
			//c.rgb = s.Albedo * _LightColor0.rgb * NdotL * atten;
			c.rgb = s.Albedo * _LightColor0.rgb * cel * atten;
			c.a = s.Alpha;
			return c;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
