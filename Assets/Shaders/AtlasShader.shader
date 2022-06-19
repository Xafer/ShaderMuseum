Shader "Custom/AtlasShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NormalTex ("NormalMap", 2D) = "white" {}
        _DetailTex ("Detail Map", 2D) = "white" {}
        [HideInInspector]_MainTex2 ("Dummy", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_Segments("Atlas segments(one axis)", int) = 4
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

        sampler2D _MainTex;
        sampler2D _NormalTex;
        sampler2D _DetailTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv2_MainTex2;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        int _Segments;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float2 localUV = IN.uv2_MainTex2;
			
			localUV = float2(localUV.x % 1, (localUV.y % 1)) / _Segments;

			localUV += floor(IN.uv_MainTex.xy * _Segments)/_Segments;
			
            float4 c = tex2Dgrad(_MainTex, localUV,0,0);
            float4 details = tex2Dgrad(_DetailTex, localUV, 0, 0);
			
            clip(c.a - 0.1f);

            o.Albedo = c.rgb;

            o.Emission = details.r * c.rgb;

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;

            o.Normal = UnpackNormal(tex2Dgrad(_NormalTex, localUV, 0, 0));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
