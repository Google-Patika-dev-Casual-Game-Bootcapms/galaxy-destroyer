Shader "Custom/vertex_Color"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
	_RedTex("Red Channel", 2D) = "white" {}
	_GreenTex("Green Channel", 2D) = "black" {}
	_BlueTex("Blue Channel", 2D) = "black" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

		sampler2D _RedTex;
		sampler2D _GreenTex;
		sampler2D _BlueTex;

        struct Input
        {
			float4 color : COLOR;
			float2 uv_RedTex;
			float2 uv_GreenTex;
			float2 uv_BlueTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			fixed4 tex1 = tex2D(_RedTex, IN.uv_RedTex);
			fixed4 tex2 = tex2D(_GreenTex, IN.uv_GreenTex);
			fixed4 tex3 = tex2D(_BlueTex, IN.uv_BlueTex);
            o.Albedo = (IN.color.r*tex1 + IN.color.y*tex2 + IN.color.z*tex3) * _Color;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
