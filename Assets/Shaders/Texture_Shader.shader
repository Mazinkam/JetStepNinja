Shader "UmeChan/Basic/Texture_Shader"
{

Properties
{
	_MainTex("Texture", 2D) = "white"{}
	_BumpMap("Bumpmap", 2D) = "bump"{}
}
	SubShader
	{
		Tags {"RenderType" = "Opaque"}
		CGPROGRAM
		#pragma surface surf Lambert
		struct Input
		{
			float2 uv_MainTex; //(1.0, 1.0) U, V
			float2 uv_BumpMap;
			//float4 color : COLOR; //(1.0, 1.0, 1.0, 1.0) R, G, B, A
		};
		sampler2D _MainTex;
		sampler2D _BumpMap;
		
		void surf(Input IN, inout SurfaceOutput o)
		{
			//o.Albedo = 1;
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		}
		ENDCG
	}
	Fallback "Diffuse"
}