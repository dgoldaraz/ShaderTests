Shader "MyShaders/NormalMap" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_NormalTex ("Normal Map", 2D) = "bump" {}
		_NormalMapIntensity ("Normal Map Intensity", Range(1, 10)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float2 uv_NormalTex;
		};

		fixed4 _Color;
		sampler2D _NormalTex;
		fixed _NormalMapIntensity;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {

			float3 NormalMap = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex));
			NormalMap.x *= _NormalMapIntensity;
			NormalMap.y *= _NormalMapIntensity;
			// Albedo comes from a texture tinted by color
			fixed4 c = _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Normal = NormalMap.rgb;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
