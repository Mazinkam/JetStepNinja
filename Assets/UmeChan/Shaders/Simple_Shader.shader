Shader "UmeChan/Basic/Simple_Shader"
{
	SubShader
	{
		Tags {"RenderType" = "Opaque"}
		
		CGPROGRAM
		#pragma surface surf Lambert
		struct Input
		{
			float4 color : COLOR; // 1.0, 1.0 , 1.0, 1.0 RGBA
		};
		
		void surf(Input IN, inout SurfaceOutput o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
}