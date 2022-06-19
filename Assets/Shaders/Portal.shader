Shader "Unlit/Portal"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {}
        _PortalAlbedo("Portal Albedo", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 screenPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            sampler2D _PortalAlbedo;
            float4 _MainTex_ST;
            float4 _PortalAlbedo_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 screenSpaceUV = i.screenPos.xy / i.screenPos.w;

                //screenSpaceUV.x = 1 - screenSpaceUV.x;
                // sample the texture
                //fixed4 col = tex2D(_MainTex, screenSpaceUV);

                float2 c = (i.uv - float2(0.5,0.5))*2;

                float waveDist = sqrt(c.x * c.x + c.y * c.y);
                float waveFactor = sin(c.x * 30 - _Time.w) / 150;

                waveFactor *= (1 - (waveDist+0.15));

                fixed4 col = tex2D(_MainTex, screenSpaceUV);

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
