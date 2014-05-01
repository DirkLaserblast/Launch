Shader "Custom/Sun" {
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	
		CGINCLUDE
	
	#include "UnityCG.cginc"
	
	//Receive parameters
	uniform sampler2D _MainTex;
	uniform sampler2D _CameraDepthTexture;

	uniform float _G;
	uniform float _SunIntesity;
	uniform float4 _StartDistance;
	uniform float4 _LightDir;
	uniform float4 _ExtColor;
	uniform float4 _MainTex_TexelSize;
	
	// for fast world space reconstruction
	
	uniform float4x4 _FrustumCornersWS;
	uniform float4 _CameraWS;
	 
	struct v2f {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
		float2 uv_depth : TEXCOORD1;
		float4 interpolatedRay : TEXCOORD2;
	};
	
	v2f vert( appdata_img v )
	{
		v2f o;
		half index = v.vertex.z;
		v.vertex.z = 0.1;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.texcoord.xy;
		o.uv_depth = v.texcoord.xy;
		
		#if UNITY_UV_STARTS_AT_TOP
		if (_MainTex_TexelSize.y < 0)
			o.uv.y = 1-o.uv.y;
		#endif				
		
		o.interpolatedRay = _FrustumCornersWS[(int)index];
		o.interpolatedRay.w = index;
		
		return o;
	}
	
	float hgPhase(float cosTheta, float g)
	{
	        return (1.0 / (4.0 * 3.14159265358979)) * ((1.0 - pow(g, 2.0)) / pow(1.0 - 2.0 * g * cosTheta + pow(g, 2.0), 1.5));
	}
	
	float4 Fog(v2f IN) : COLOR
	{
		float dpth = Linear01Depth(UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture,IN.uv_depth)));
		float4 rayDir = dpth * IN.interpolatedRay;
		float4 rayOri = _CameraWS + rayDir;
		float3 extColor = _ExtColor.rgb;
		
		float3 mainScene = tex2D(_MainTex, IN.uv).rgb;
		
		float3 sunDir = normalize(-_LightDir.xyz);
		
		float cosTheta = dot(normalize(rayOri - _CameraWS), sunDir);
		float mPhase = hgPhase(cosTheta, _G);
		float3 finalColor = ((mPhase * extColor) * _SunIntesity) + mainScene;
		 
		 return float4(finalColor, 1.0f);
		
	}

	ENDCG 
	
Subshader {

	ZTest Off
	Cull Off
	ZWrite Off
	Fog { Mode off }
	
//Pass 0 Fog
 Pass 
 {
 Name "Fog"

      CGPROGRAM
      #pragma target 3.0
      #pragma vertex vert
      #pragma fragment Fog
      ENDCG
  }
}

Fallback off
	
}